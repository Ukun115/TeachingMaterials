namespace SOLID.OpenClosed.Before
{
    /// <summary>
    /// 属性の種類。
    /// 新しい属性を足すたび、この enum に1行加えるだけでなく、
    /// 相性・表示名・ヒット演出…と、あちこちの switch / if 分岐まで直して回るはめになる。
    /// </summary>
    public enum Element
    {
        Fire,    // 火
        Water,   // 水
        Wind,    // 風
        Thunder, // 雷（あとから追加された属性。追加のたびに何か所さわったか数えてみよう）
    }
}
