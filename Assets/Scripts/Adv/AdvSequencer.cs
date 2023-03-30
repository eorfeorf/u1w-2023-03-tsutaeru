using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AdvSequencer : MonoBehaviour
{
    [SerializeField]
    //private AdvTalkSetting talkSetting;
    private Sprite bg;
    
    // TODO:いろんなコマンドを使えるようにしたい.
    [SerializeField]
    private List<AdvTalkCommand> datas;

    private Dictionary<int, Sprite> talkWindows = new();

    private Transform parent;
    private CompositeDisposable _disposable = new();
    
    private void Start()
    {
        // ウィンドウ初期化.
        // var setting = Instantiate(talkSetting, parent) as AdvTalkSetting;
        // talkWindows[setting.Uid] = setting.TalkWindow;
    }

    private async void Update()
    {
        // while (true)
        // {
        //     for (int i = datas.Count - 1; i >= 0; --i)
        //     {
        //         var data = datas[i];
        //         data.OnSetup.Subscribe(data =>
        //         {
        //             if (data is AdvTalkCommandData talkCommandData)
        //             {
        //                 talkCommandData.
        //             }
        //         }).AddTo(_disposable);
        //         
        //         data.Execute();
        //     }
        // }
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}