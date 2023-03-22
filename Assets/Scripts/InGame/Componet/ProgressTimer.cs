using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTimer
{
    public float ProgressTime => _progressTime;
    
    private float _progressTime;
    private float _startTime;
    
    public void Start()
    {
        Reset();
        _startTime = Time.time;
    }
    
    public void Update(float nowTime)
    {
        _progressTime = nowTime - _startTime;
        //Debug.Log($"[ProgressTime] {_progressTime}");
    }

    private void Reset()
    {
        _progressTime = 0f;
        _startTime = 0f;
    }
}