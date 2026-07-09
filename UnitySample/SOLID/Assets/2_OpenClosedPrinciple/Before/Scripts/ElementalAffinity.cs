namespace SOLID.OpenClosed.Before
{
    /// <summary>
    /// 属性まわりの判定を、まとめて if / switch で背負いこんだクラス。
    /// 一見きれいに分かれているようで、属性を1つ増やすと
    /// このクラスの中の3つのメソッドを「全部」直さないと動かない。
    /// ＝ 拡張のたびに既存コードを書き換える必要がある＝開放閉鎖の原則に反した状態。
    /// </summary>
    public static class ElementalAffinity
    {
        /// <summary>
        /// 攻撃属性と相手属性から、ダメージ倍率を返す。
        /// 属性が増えるほど、この if の山がふくれ上がっていく。
        /// しかも既存の行を直すので、うっかり別の相性まで壊しやすい。
        /// </summary>
        public static float GetMultiplier(Element attackElement, Element targetElement)
        {
            // 火→風、風→水、水→火 なら有利（2倍）。
            if ((attackElement == Element.Fire && targetElement == Element.Wind) ||
                (attackElement == Element.Wind && targetElement == Element.Water) ||
                (attackElement == Element.Water && targetElement == Element.Fire))
            {
                return 2.0f;
            }

            // ↓↓↓ 雷を追加したとき、ここに新しい相性を書き足した（有利側）。
            if (attackElement == Element.Thunder && targetElement == Element.Water)
            {
                return 2.0f;
            }

            // 逆向きなら不利（0.5倍）。
            if ((attackElement == Element.Wind && targetElement == Element.Fire) ||
                (attackElement == Element.Water && targetElement == Element.Wind) ||
                (attackElement == Element.Fire && targetElement == Element.Water))
            {
                return 0.5f;
            }

            // ↓↓↓ 雷を追加したとき、ここにも相性を書き足した（不利側）。書き忘れると相性が片手落ちになる。
            if (attackElement == Element.Water && targetElement == Element.Thunder)
            {
                return 0.5f;
            }

            // それ以外は等倍。
            return 1.0f;
        }

        /// <summary>属性の日本語名を返す。属性を足すたび、この switch にも case を書き足すことになる。</summary>
        public static string GetDisplayName(Element element)
        {
            switch (element)
            {
                case Element.Fire:
                    return "火";
                case Element.Water:
                    return "水";
                case Element.Wind:
                    return "風";
                case Element.Thunder: // ← 雷の追加でここも1行増えた。
                    return "雷";
                default:
                    return "？";
            }
        }

        /// <summary>攻撃が当たったときの、属性ごとのひとこと。ここも属性ぶんだけ case が並ぶ。</summary>
        public static string GetOnHitFlavor(Element element)
        {
            switch (element)
            {
                case Element.Fire:
                    return "メラメラと燃え上がる！";
                case Element.Water:
                    return "水しぶきが激しく打ちつける！";
                case Element.Wind:
                    return "鋭い風が切り裂く！";
                case Element.Thunder: // ← 雷の追加でここも1行増えた。3か所目。
                    return "バリバリと雷がしびれさせる！";
                default:
                    return "";
            }
        }
    }
}
