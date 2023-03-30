using UnityEngine;

public static class NoteTiming
{
    public enum CheckType
    {
        None, // 何もなし.
        Success, // 許容時間内.
        FastFail, // 早押し.
    }
    
    /// <summary>
    /// ノーツが許容時間内か.
    /// </summary>
    /// <param name="subTime"></param>
    /// <returns></returns>
    public static CheckType Check(float subTime)
    {
        // 許容時間チェック.
        var validTimeHalf = GameDefine.NOTE_VALID_TIME_HALF;
        if (-validTimeHalf <= subTime && subTime <= validTimeHalf)
        {
            Debug.Log("[NoteTiming] success");
            return CheckType.Success;
        }
        
        // 早押しチェック.
        var invalidFastTimeHalf = GameDefine.NOTE_FAST_INVALID_TIME_HALF;
        if (subTime <= invalidFastTimeHalf)
        {
            Debug.Log("[NoteTiming] fast fail");
            return CheckType.FastFail;
        }

        //Debug.Log("[NoteTiming] none");
        return CheckType.None;
    }

    /// <summary>
    /// ノーツが通り過ぎたか.
    /// </summary>
    /// <returns>true:通り過ぎた　false:通り過ぎてない</returns>
    public static bool IsPassNote(float progressTime, float noteTime)
    {
        var isPass = progressTime > (noteTime + GameDefine.NOTE_VALID_TIME_HALF);
        if (isPass)
        {
            Debug.Log("[NoteTiming] passed");
            return true;
        }
        
        return false;
    }
}
