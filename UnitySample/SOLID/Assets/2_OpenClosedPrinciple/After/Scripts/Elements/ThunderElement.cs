namespace SOLID.OpenClosed.After
{
    /// <summary>
    /// 雷の属性。あとから追加した新入り。
    /// 追加のために書いたのは、この1ファイルと、登録の1行だけ。
    /// 火・水・風のクラスも、相性ロジックも、キャラのコードも、いっさい触っていない。
    /// これが「拡張には開き、修正には閉じる」＝開放閉鎖の原則の気持ちよさ。
    /// </summary>
    public class ThunderElement : IElement
    {
        public string Id => "Thunder";
        public string DisplayName => "雷";
        public string OnHitFlavor => "バリバリと雷がしびれさせる！";

        /// <summary>
        /// 雷は水に強い。
        /// 「水は雷に弱い」とは、どこにも書いていないことに注目。
        /// 相手（水）を有利と判定しないだけで、自動的に水は雷に不利になる。
        /// </summary>
        public bool IsStrongAgainst(IElement target) => target is WaterElement;
    }
}
