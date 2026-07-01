namespace SOLID.SingleResponsibility.After
{
    /// <summary>属性の相性「だけ」を判定する。相性ルールを変えたいときに触るのは、ここだけ。</summary>
    public static class ElementalAffinityAfter
    {
        /// <summary>攻撃属性と相手属性から、ダメージ倍率を返す。</summary>
        public static float GetMultiplier(ElementAfter attackElement, ElementAfter targetElement)
        {
            // 火→風、風→水、水→火 なら有利（2倍）。
            if ((attackElement == ElementAfter.Fire && targetElement == ElementAfter.Wind) ||
                (attackElement == ElementAfter.Wind && targetElement == ElementAfter.Water) ||
                (attackElement == ElementAfter.Water && targetElement == ElementAfter.Fire))
            {
                return 2.0f;
            }

            // その逆なら不利（0.5倍）。
            if ((attackElement == ElementAfter.Wind && targetElement == ElementAfter.Fire) ||
                (attackElement == ElementAfter.Water && targetElement == ElementAfter.Wind) ||
                (attackElement == ElementAfter.Fire && targetElement == ElementAfter.Water))
            {
                return 0.5f;
            }

            // それ以外は等倍。
            return 1.0f;
        }
    }
}
