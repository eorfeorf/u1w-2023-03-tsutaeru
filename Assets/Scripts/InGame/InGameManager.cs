using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using InGame;
using InGame.Component;
using UniRx;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField]
    private PlayView _playView;

    [SerializeField]
    private UpperView _upperView;

    [SerializeField]
    private AudioSource _audioSourceHeartBeat;

    public IReadOnlyReactiveProperty<GameDefine.ResultRank> OnEnd => _onEnd;
    private readonly ReactiveProperty<GameDefine.ResultRank> _onEnd = new();

    private MusicalScoreLoader _musicalScoreLoader = new();
    private MusicalScoreEntity _musicalScoreEntity;
    private List<Note> _notes = new();
    private List<ControlPoint> _controlPoints = new();
    private ProgressTimer _progressTimer = new();

    private bool _isEnd = false;
    private int _okCount = 0;
    private bool _isInitialized = false;
    private int _musicalScoreIndex = -1;


    public void Initialize(int index, MusicalScore musicalScore)
    {
        // 選ばれた楽曲の譜面を読み込む.
        // if (!_isInitialized && index != _musicalScoreIndex)
        if(index != _musicalScoreIndex)
        {
            _musicalScoreEntity = _musicalScoreLoader.Load(musicalScore);
            _isInitialized = true;
            _musicalScoreIndex = index;
        }
        
        Reset();

        // ゲーム開始.
        _progressTimer.Start();
        Debug.Log("[InGame] Game start.");
    }

    private void Reset()
    {
        // ビュー削除.
        _playView.DestroyNoteViews();
        // ノーツ削除.
        _notes.Clear();
        // コントロールポイント削除.
        _controlPoints.Clear();

        // ノーツ生成.
        foreach (var note in _musicalScoreEntity.Notes)
        {
            _notes.Add(new Note(note));
        }
        // コントロールポイント削除.
        foreach (var controlPoint in _musicalScoreEntity.ControlPoints)
        {
            _controlPoints.Add(new ControlPoint(controlPoint));
        }
        
        // ビュー生成.
        _playView.CreateNoteViews(_notes);

        _isEnd = false;
        _okCount = 0;
    }

    private async void Update()
    {
        // 時間更新.
        _progressTimer.Update(Time.time);

        // 入力適用.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyInput();
        }

        // リセット.
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        // ノーツ通過.
        UpdateNotePass(_progressTimer.ProgressTime);

        // 描画更新.
        _playView.UpdateNoteViews(_progressTimer.ProgressTime);

        // ゲームが終わったか？
        if (!_isEnd && IsEnd())
        {
            Ending().Forget();
        }
    }

    /// <summary>
    /// 入力適用.
    /// </summary>
    private void ApplyInput()
    {
        // ノーツ適用.
        var note = _notes.FirstOrDefault(x => x.IsActive);
        if (note == null)
        {
            return;
        }

        // 許容時間内.
        var checkType = NoteTiming.Check(note.Time - _progressTimer.ProgressTime);
        switch (checkType)
        {
            case NoteTiming.CheckType.Success:
                note.IsActive = false;
                ++_okCount;
                _playView.SetNoteActive(note.Uid, false);
                _playView.ApplyRank(GameDefine.Rank.Ok);
                _upperView.CrateHeart();
                _audioSourceHeartBeat.Play();
                break;
            case NoteTiming.CheckType.FastFail:
                note.IsActive = false;
                _playView.SetNoteActive(note.Uid, false);
                _playView.ApplyRank(GameDefine.Rank.Ng);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ノーツ通過.
    /// </summary>
    private void UpdateNotePass(float progressTime)
    {
        var note = _notes.FirstOrDefault(x => x.IsActive);
        if (note == null)
        {
            return;
        }

        var isPass = NoteTiming.IsPassNote(progressTime, note.Time);
        if (isPass)
        {
            note.IsActive = false;
            _playView.SetNoteActive(note.Uid, false);
            _playView.ApplyRank(GameDefine.Rank.Ng);
        }
    }

    /// <summary>
    /// ゲームが終わったか？
    /// </summary>
    /// <returns></returns>
    private bool IsEnd()
    {
        var note = _notes.FirstOrDefault(x => x.IsActive);
        if (note == null)
        {
            Debug.Log("[InGame] Game end.");
            _isEnd = true;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 終了後演出.
    /// </summary>
    private async UniTaskVoid Ending()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(GameDefine.INGAME_END_WAIT_TIME_SEC));

        var rate = _okCount / (float) _notes.Count;
        var resultRank = rate >= 0.9f ? GameDefine.ResultRank.Success : GameDefine.ResultRank.Fail;
        Debug.Log($"[InGame] ResultRank:{resultRank} rate:{rate}");
        _onEnd.SetValueAndForceNotify(resultRank);
    }
}