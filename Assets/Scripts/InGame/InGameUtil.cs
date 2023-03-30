namespace InGame
{
    public static class InGameUtil
    {
        /// <summary>
        /// 指定したBPMの1拍分の時間を取得.
        /// </summary>
        /// <param name="bpm"></param>
        /// <returns></returns>
        public static float Get1BeatTime(float bpm)
        {
            return GameDefine.SEC60 / bpm;
        }
    }
}