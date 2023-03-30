using System;
using UnityEngine;

public class HeartView : MonoBehaviour
{
    private Vector3 _startLocalPos;
    private Vector3 _endLocalPos;

    private float _createdAt;
    
    public void Initialize(Vector3 startLocalPos, Vector3 endLocalPos)
    {
        _startLocalPos = startLocalPos;
        _endLocalPos = endLocalPos;
        _createdAt = Time.time;
        transform.localPosition = _startLocalPos;
    }

    private void Update()
    {
        var t =  (Time.time - _createdAt) / GameDefine.HEART_MOVE_TIME;
        //Debug.Log($"[HeartView] t={t}");
        if (t > 1f)
        {
            Destroy(gameObject);
            return;
        }

        var pos = _startLocalPos + (_endLocalPos - _startLocalPos) * t;
        // 1周期 * 回数.
        pos.y = Mathf.Sin(t * (Mathf.PI * 2) * GameDefine.HEART_MOVE_FREQ);
        // 振幅幅.
        pos.y *= GameDefine.HEART_MOVE_AMP;
        transform.localPosition = pos;
    }
}
