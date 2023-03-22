using TMPro;
using UnityEngine;

public class RankView : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro rankText;

    public void SetRank(GameDefine.Rank rank)
    {
        rankText.text = GameDefine.RankString[rank];
    }
}