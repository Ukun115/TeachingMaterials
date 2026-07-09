namespace SOLID.OpenClosed.After
{
    /// <summary>
    /// 「1つの属性」がすべきことを決めた約束（インターフェース）。
    /// 新しい属性は、この約束を満たすクラスを1つ作るだけで仲間入りできる。
    /// 相性ロジックやキャラ側のコードには、いっさい手を入れない。
    /// ＝ 拡張には開き（新クラスを足せる）、修正には閉じる（既存を触らない）。
    /// </summary>
    public interface IElement
    {
        /// <summary>属性を見分けるための合い言葉（シーンではこの文字列で属性を指定する）。</summary>
        string Id { get; }

        /// <summary>画面に出す日本語名（火・水・風・雷…）。</summary>
        string DisplayName { get; }

        /// <summary>攻撃が当たったときの、この属性ならではのひとこと。</summary>
        string OnHitFlavor { get; }

        /// <summary>
        /// この属性が、相手の属性に対して有利かどうか。
        /// 「自分が強い相手」だけを各属性が自分で言い切る。
        /// 「誰に弱いか」は書かなくてよい（相手が自分を有利と言えば、自動的にこちらが不利になる）。
        /// </summary>
        bool IsStrongAgainst(IElement target);
    }
}
