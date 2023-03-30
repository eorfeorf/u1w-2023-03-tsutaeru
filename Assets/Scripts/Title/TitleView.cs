using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StartButton
{
    public int Index;
    public Button Button;
    public TextMeshProUGUI ButtonText;
}

public class TitleView : MonoBehaviour
{
    [SerializeField]
    private StartButton[] startButtons;
    [SerializeField]
    private TextMeshProUGUI introText;

    public IReadOnlyReactiveProperty<int> OnStartButtonPush => _onStartButtonPush;
    private readonly ReactiveProperty<int> _onStartButtonPush = new();

    private void Start()
    {
        introText.text = GameDefine.TITLE_MESSAGE;
        foreach (var button in startButtons)
        {
            button.ButtonText.text = GameDefine.TITLE_BUTTONS_TEXT[button.Index];
            button.Button.onClick.AsObservable().Subscribe(_ =>
            {
                _onStartButtonPush.SetValueAndForceNotify(button.Index);
            }).AddTo(this);
        }
    }
}