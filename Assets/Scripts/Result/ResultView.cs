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

    public IReadOnlyReactiveProperty<Unit> OnRetryButton => _onRetryButton;
    private readonly ReactiveProperty<Unit> _onRetryButton = new();

    public IReadOnlyReactiveProperty<Unit> OnTitleButton => _onTitleButton;
    private readonly ReactiveProperty<Unit> _onTitleButton = new();


    private void Start()
    {
        retryButtonText.text = GameDefine.RESULT_RETRY_BUTTON_TEXT;
        titleButtonText.text = GameDefine.RESULT_TITLE_BUTTON_TEXT;

        retryButton.onClick.AsObservable().Subscribe(_ =>
        {
            _onRetryButton.SetValueAndForceNotify(Unit.Default);
        }).AddTo(this);
        
        titleButton.onClick.AsObservable().Subscribe(_ =>
        {
            _onTitleButton.SetValueAndForceNotify(Unit.Default);
        }).AddTo(this);
    }
}
