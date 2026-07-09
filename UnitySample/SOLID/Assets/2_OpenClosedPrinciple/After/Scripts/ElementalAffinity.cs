namespace SOLID.OpenClosed.After
{
    /// <summary>
    /// 属性の相性「だけ」を判定する。
    /// 中身は、属性が何種類あってもこのまま。属性を足しても、この計算は1文字も書き換えない。
    /// ＝ 修正に対して閉じている（開放閉鎖の原則）。
    /// </summary>
    public static class ElementalAffinity
    {
        /// <summary>攻撃属性と相手属性から、ダメージ倍率を返す。</summary>
        public static float GetMultiplier(IElement attackElement, IElement targetElement)
        {
            // 自分が相手に有利なら2倍。
            if (attackElement.IsStrongAgainst(targetElement))
            {
                return 2.0f;
            }

            // 逆に相手が自分に有利なら、こちらは不利で0.5倍。
            if (targetElement.IsStrongAgainst(attackElement))
            {
                return 0.5f;
            }

            // どちらでもなければ等倍。
            return 1.0f;
        }
    }
}
