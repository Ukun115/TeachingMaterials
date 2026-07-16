using UnityEngine;

namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// 魔法使い。強力な攻撃魔法が持ち味で、物理はか弱い。（能力値は Before と同じ）
    /// できることは「攻撃」と「魔法」なので、IAttacker と IMagicUser の2つだけを実装する。
    /// 回復はできないので IHealer は実装しない＝Heal を書かされない。
    /// </summary>
    public class Mage : PartyMember, IAttacker, IMagicUser
    {
        public int physicalPower = 4;
        public int magicPower = 30;

        public void Attack(Enemy target)
        {
            int damage = Mathf.Max(1, physicalPower - target.defense);
            target.TakeDamage(damage);
            Debug.Log($"【物理】{Name} のか弱い杖の一撃！ {target.Name} に {damage} ダメージ（残りHP {target.hp}/{target.maxHp}）");
        }

        public void CastMagic(Enemy target)
        {
            target.TakeDamage(magicPower);
            Debug.Log($"【魔法】{Name} のファイアボール！ {target.Name} に {magicPower} ダメージ（残りHP {target.hp}/{target.maxHp}）");
        }
    }
}
