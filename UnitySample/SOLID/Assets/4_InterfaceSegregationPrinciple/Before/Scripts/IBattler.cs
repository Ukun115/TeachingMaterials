namespace SOLID.InterfaceSegregation.Before
{
    /// <summary>
    /// 「パーティの一員なら、攻撃も魔法も回復も、なんでもできるはずだ」と決めつけた欲ばりインターフェース。
    /// 3つの役割をひとつに束ねてしまっている＝“太った”インターフェース。
    ///
    /// ところが実際は、戦士は魔法を使えず、魔法使いは回復できず、僧侶は攻撃魔法を持たない。
    /// この IBattler を実装すると、どのクラスも「自分にはできないメソッド」まで書かされる。
    /// できないメソッドは、中身を空にするか、例外を投げるしかない＝インターフェース分離の原則の違反。
    /// </summary>
    public interface IBattler
    {
        string Name { get; }
        bool IsAlive { get; }

        /// <summary>物理で敵を攻撃する。</summary>
        void Attack(Enemy target);

        /// <summary>魔法で敵を攻撃する。（魔法を使えない者には実装できない）</summary>
        void CastMagic(Enemy target);

        /// <summary>仲間を回復する。（回復役でない者には実装できない）</summary>
        void Heal(PartyMember target);
    }
}
