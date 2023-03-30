using System.Collections.Generic;

public static class GameDefine
{
    // 開始までの待ち時間.
    public static float START_ADD_TIME_SEC = 5f;
    
    // ノーツ判定許容時間.
    private static float oneFrame = 1 / 60f; // 60fps 1フレームあたりの時間.
    public static float NOTE_VALID_TIME_HALF = oneFrame * 5f;
    // ノーツ早押し不正時間.
    public static float NOTE_FAST_INVALID_TIME_HALF = NOTE_VALID_TIME_HALF + oneFrame * 10f;

    // ハートが届くまでの時間.
    public static float HEART_MOVE_TIME = 2f;
    // ハートが届くまでのsin周期.
    public static float HEART_MOVE_FREQ = 3f;
    // ハートの振幅係数.
    public static float HEART_MOVE_AMP = 0.15f;

    public static float SEC60 = 60f;
    public static float BPM = 360f;

    // ゲーム終了後待機時間.
    public static float INGAME_END_WAIT_TIME_SEC = 4f;
    
    
    /// <summary>
    /// ノーツの判定種類.
    /// </summary>
    public enum Rank
    {
        Ng,
        Ok,
    }
    
    /// <summary>
    /// ノーツ適用結果. 
    /// </summary>
    public static readonly Dictionary<Rank, string> RankString = new Dictionary<Rank, string>()
    {
        {Rank.Ng, "NG"},
        {Rank.Ok, "OK"}
    };

    // タイトル用文字列.
    public static string TITLE_MESSAGE = "ドキドキを伝えて告白を成功させよう！";
    public static string[] TITLE_BUTTONS_TEXT =
    {
        "気がある子に告白する！",
        "興味なさそうな子に告白する！",
        "冷たい子に告白する！",
    };

    // リザルト用ゲーム成功ランク.
    public enum ResultRank
    {
        Success,
        Fail,
    }
 
    // リザルト用文字列.
    public static string RESULT_RETRY_BUTTON_TEXT = "もう一回チャレンジ！";
    public static string RESULT_TITLE_BUTTON_TEXT = "戻る...";
}
