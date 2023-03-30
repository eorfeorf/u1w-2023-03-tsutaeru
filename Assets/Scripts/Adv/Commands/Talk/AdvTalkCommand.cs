using System;
using Adv.Commands;
using UniRx;
using UnityEngine;

[Serializable]
public class AdvTalkCommand : AdvCommand
{
    // [SerializeField]
    // private AdvTalkCommandData commandData;

    /// <summary>
    /// 立ち位置.
    /// </summary>
    [SerializeField]
    public int PositionIndex;

    /// <summary>
    /// 名前インデックス.
    /// </summary>
    [SerializeField]
    public int NameIndex;
    
    /// <summary>
    /// 文章.
    /// </summary>
    [SerializeField]
    public string Text;
    
    public override void Execute()
    {
        
    }
}