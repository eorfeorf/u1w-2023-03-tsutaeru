using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TalkSettingsData", menuName = "Adv/TalkSettings", order = 1)]
public class AdvTalkSetting : ScriptableObject
{
    public int Uid;
    public Sprite TalkWindow;
}
