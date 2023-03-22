using System;
using UniRx;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    private UpperView _upperView;

    [SerializeField]
    private TitleView _titleView;

    public IObservable<Unit> OnStartButton => _titleView.OnStartButtonPushed;

    private void Start()
    {
    }
}