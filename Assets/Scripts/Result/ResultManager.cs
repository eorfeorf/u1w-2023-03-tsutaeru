using System;
using UniRx;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private ResultView resulView;

    public IReadOnlyReactiveProperty<Unit> OnRetryButton => _onRetryButton; 
    private readonly ReactiveProperty<Unit> _onRetryButton = new();
    
    public IReadOnlyReactiveProperty<Unit> OnTitleButton => _onTitleButton; 
    private readonly ReactiveProperty<Unit> _onTitleButton = new();
    
    private void Start()
    {
        resulView.OnRetryButton.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            _onRetryButton.SetValueAndForceNotify(Unit.Default);
        }).AddTo(this);
        resulView.OnTitleButton.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            _onTitleButton.SetValueAndForceNotify(Unit.Default);
        }).AddTo(this);
    }
}
