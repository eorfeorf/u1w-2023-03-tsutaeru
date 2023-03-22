using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TextMeshProUGUI startButtonText;
    [SerializeField]
    private TextMeshProUGUI introText;

    public IObservable<Unit> OnStartButtonPushed => startButton.onClick.AsObservable();

    private void Start()
    {
        introText.text = GameDefine.TITLE_MESSAGE;
        startButtonText.text = GameDefine.TITLE_BUTTON_TEXT;
    }
}