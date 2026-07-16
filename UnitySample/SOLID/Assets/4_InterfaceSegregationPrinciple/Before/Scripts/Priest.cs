using System;
using UnityEngine;

namespace SOLID.InterfaceSegregation.Before
{
    /// <summary>
    /// 僧侶。仲間を癒す回復役で、物理は控えめ。攻撃魔法はまったく持たない。
    /// IBattler のせいで CastMagic を書かされ、ここも例外を投げるだけの“穴”になっている。
    /// </summary>
    public class Priest : PartyMember, IBattler
    {
        public int physicalPower = 6;
        public int healPower = 25;

        public void Attack(Enemy target)
        {
            int damage = Mathf.Max(1, physicalPower - target.defense);
            target.TakeDamage(damage);
            Debug.Log($"【物理】{Name} のメイス！ {target.Name} に {damage} ダメージ（残りHP {target.hp}/{target.maxHp}）");
        }

        /// <summary>僧侶は攻撃魔法を持たない。IBattler に強いられた“穴”。</summary>
        public void CastMagic(Enemy target)
        {
            throw new NotSupportedException($"{Name} は攻撃魔法を使えません");
        }

        public void Heal(PartyMember target)
        {
            target.ReceiveHeal(healPower);
            Debug.Log($"【回復】{Name} のヒール！ {target.Name} のHPが {healPower} 回復（残りHP {target.hp}/{target.maxHp}）");
        }
    }
}
