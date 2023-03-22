using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField]
    private PlayView _playView;

    [SerializeField]
    private UpperView _upperView;

    [SerializeField]
    private AudioSource _audioSourceHeartBeat;

    public IReadOnlyReactiveProperty<GameDefine.ResultRank> OnEnd => _onEnd;
    private readonly ReactiveProperty<GameDefine.ResultRank> _onEnd = new();

    private readonly List<Note> _notes = new();
    private ProgressTimer _progressTimer = new();

    private bool _isEnd = false;
    private int okCount = 0;

    private int noteNum = 10;

    private void Reset()
    {
        // データ削除.
        _playView.DestroyNoteViews();
        // ビュー削除.
        _notes.Clear();


        // データ生成.
        for (int i = 0; i < noteNum; ++i)
        {
            var note = new Note();
            note.Time = GameDefine.START_ADD_TIME_SEC + i * (GameDefine.SEC60 / GameDefine.BPM);
            note.IsActive = true;
            note.Uid = i;
            _notes.Add(note);
        }

        // ビュー生成.
        _playView.CreateNoteViews(_notes);

        _isEnd = false;
        okCount = 0;
    }

    private void OnEnable()
    {
        Reset();

        // ゲーム開始.
        _progressTimer.Start();
        Debug.Log("[InGame] Game start.");
    }

    private void Update()
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
                ++okCount;
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
    private async UniTask Ending()
    {
        await Task.Delay(GameDefine.INGAME_END_WAIT_TIME_MS);

        var rate = okCount / (float) noteNum;
        var resultRank = rate >= 0.9f ? GameDefine.ResultRank.Success : GameDefine.ResultRank.Fail;
        Debug.Log($"[InGame] ResultRank:{resultRank} rate:{rate}");
        _onEnd.SetValueAndForceNotify(resultRank);
    }
}