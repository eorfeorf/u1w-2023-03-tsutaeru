using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperView : MonoBehaviour
{
    [SerializeField]
    private HeartView heartViewPrefab;
    
    [SerializeField]
    private Transform heartParent;
    
    [SerializeField]
    private Transform heartStart;
    
    [SerializeField]
    private Transform heartEnd;

    
    /// <summary>
    /// ハート生成.
    /// </summary>
    public void CrateHeart()
    {
        var heart = Instantiate(heartViewPrefab, Vector3.zero, Quaternion.identity, heartParent);
        heart.Initialize(heartStart.localPosition, heartEnd.localPosition);
    }
}
