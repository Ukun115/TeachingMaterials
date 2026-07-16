namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// 「仲間を回復できる」という役割だけを表す小さなインターフェース。
    /// 回復役だけが実装する。攻撃しかできない者はこれを実装しない＝回復を強要されない。
    /// </summary>
    public interface IHealer
    {
        void Heal(PartyMember target);
    }
}
