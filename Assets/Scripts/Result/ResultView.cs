using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private TextMeshProUGUI retryButtonText;
    [SerializeField]
    private Button titleButton;
    [SerializeField]
    private TextMeshProUGUI titleButtonText;

    public IObservable<Unit> OnRetryButton => retryButton.onClick.AsObservable();
    public IObservable<Unit> OnTitleButton => titleButton.onClick.AsObservable();

    private void Start()
    {
        retryButtonText.text = GameDefine.RESULT_RETRY_BUTTON_TEXT;
        titleButtonText.text = GameDefine.RESULT_TITLE_BUTTON_TEXT;
    }
}
