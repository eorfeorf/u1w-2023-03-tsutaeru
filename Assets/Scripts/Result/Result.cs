using System;
using UniRx;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField]
    private ResultView resulView;

    public IObservable<Unit> OnRetryButton => resulView.OnRetryButton;
    public IObservable<Unit> OnTitleButton => resulView.OnTitleButton;

    private void Start()
    {
        
    }
}
