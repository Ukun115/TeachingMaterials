namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// 「物理で攻撃できる」という、たったひとつの役割だけを表す小さなインターフェース。
    /// 前衛でも後衛でも、殴れる者はこれを実装する。
    /// できないことは要求しないので、実装側に“空の穴”が生まれない。
    /// </summary>
    public interface IAttacker
    {
        void Attack(Enemy target);
    }
}
