using System;
using UniRx;
using UnityEngine;

public class GameSequencer : MonoBehaviour, IDisposable
{
    [SerializeField]
    private TitleManager titleManager;

    [SerializeField]
    private InGameManager inGameManager;

    [SerializeField]
    private ResultManager resultManager;

    [SerializeField]
    private MusicalScore[] _musicalScores;
    
    private int selectMusicIndex = 0;

    private CompositeDisposable _disposable = new();

    private void Start()
    {
        // タイトル.
        titleManager.OnStartButton.SkipLatestValueOnSubscribe().Subscribe(selectMusicIndex =>
        {
            Debug.Log("[GameSequencer] タイトル終了.");
            titleManager.gameObject.SetActive(false);
            
            Debug.Log($"[GameSequencer] selectMusicIndex:{selectMusicIndex}");
            inGameManager.gameObject.SetActive(true);
            inGameManager.Initialize(selectMusicIndex, _musicalScores[selectMusicIndex]);
        }).AddTo(_disposable);

        // インゲーム.
        inGameManager.OnEnd.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            Debug.Log("[GameSequencer] インゲーム終了.");
            inGameManager.gameObject.SetActive(false);
            
            resultManager.gameObject.SetActive(true);
        }).AddTo(_disposable);

        // リザルト.
        resultManager.OnRetryButton.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            Debug.Log("[GameSequencer] リトライ.");
            resultManager.gameObject.SetActive(false);
            
            inGameManager.gameObject.SetActive(true);
            inGameManager.Initialize(selectMusicIndex, _musicalScores[selectMusicIndex]);
        }).AddTo(_disposable);
        resultManager.OnTitleButton.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            Debug.Log("[GameSequencer] 戻る.");
            resultManager.gameObject.SetActive(false);
            
            titleManager.gameObject.SetActive(true);
        }).AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable?.Dispose();
    }
}
