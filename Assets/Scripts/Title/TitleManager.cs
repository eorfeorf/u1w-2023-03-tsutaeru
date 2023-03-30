using System;
using UniRx;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private UpperView _upperView;

    [SerializeField]
    private TitleView _titleView;

    public IReadOnlyReactiveProperty<int> OnStartButton => _onStartButton;
    private readonly ReactiveProperty<int> _onStartButton = new();

    private void Start()
    {
        _titleView.OnStartButtonPush.SkipLatestValueOnSubscribe().Subscribe(index =>
        {
            _onStartButton.SetValueAndForceNotify(index);
        }).AddTo(this);
    }
}