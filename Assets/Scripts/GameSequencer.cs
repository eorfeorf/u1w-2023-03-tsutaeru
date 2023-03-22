using UniRx;
using UnityEngine;

public class GameSequencer : MonoBehaviour
{
    [SerializeField]
    private Title title;

    [SerializeField]
    private InGame inGame;

    [SerializeField]
    private Result result;

    private void Start()
    {
        title.OnStartButton.Subscribe(_ =>
        {
            Debug.Log("[GameSequencer] タイトル終了.");
            title.gameObject.SetActive(false);
            inGame.gameObject.SetActive(true);
        }).AddTo(this);

        inGame.OnEnd.Subscribe(_ =>
        {
            Debug.Log("[GameSequencer] インゲーム終了.");
            inGame.gameObject.SetActive(false);
            result.gameObject.SetActive(true);
        }).AddTo(this);

        result.OnRetryButton.Subscribe(_ =>
        {
            Debug.Log("[GameSequencer] リトライ.");
            result.gameObject.SetActive(false);
            inGame.gameObject.SetActive(true);
        }).AddTo(this);
        result.OnTitleButton.Subscribe(_ =>
        {
            Debug.Log("[GameSequencer] 戻る.");
            result.gameObject.SetActive(false);
            title.gameObject.SetActive(true);
        }).AddTo(this);
    }
}
