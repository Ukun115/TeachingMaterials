using UnityEngine;

namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// 僧侶。仲間を癒す回復役で、物理は控えめ。（能力値は Before と同じ）
    /// できることは「攻撃」と「回復」なので、IAttacker と IHealer の2つだけを実装する。
    /// 攻撃魔法はできないので IMagicUser は実装しない＝CastMagic を書かされない。
    /// </summary>
    public class Priest : PartyMember, IAttacker, IHealer
    {
        public int physicalPower = 6;
        public int healPower = 25;

        public void Attack(Enemy target)
        {
            int damage = Mathf.Max(1, physicalPower - target.defense);
            target.TakeDamage(damage);
            Debug.Log($"【物理】{Name} のメイス！ {target.Name} に {damage} ダメージ（残りHP {target.hp}/{target.maxHp}）");
        }

        public void Heal(PartyMember target)
        {
            target.ReceiveHeal(healPower);
            Debug.Log($"【回復】{Name} のヒール！ {target.Name} のHPが {healPower} 回復（残りHP {target.hp}/{target.maxHp}）");
        }
    }
}
