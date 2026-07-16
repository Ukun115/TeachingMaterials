namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// 「攻撃魔法を使える」という役割だけを表す小さなインターフェース。
    /// 魔法を使える者だけが実装する。戦士や僧侶はこれを実装しない＝魔法を強要されない。
    /// </summary>
    public interface IMagicUser
    {
        void CastMagic(Enemy target);
    }
}
