using System;
using UnityEngine;

namespace SOLID.InterfaceSegregation.Before
{
    /// <summary>
    /// 魔法使い。強力な攻撃魔法が持ち味だが、物理はか弱く、回復魔法は持たない。
    /// IBattler のせいで Heal を書かされ、ここも例外を投げるだけの“穴”になっている。
    /// </summary>
    public class Mage : PartyMember, IBattler
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

        /// <summary>魔法使いは回復魔法を持たない。IBattler に強いられた“穴”。</summary>
        public void Heal(PartyMember target)
        {
            throw new NotSupportedException($"{Name} は回復魔法を使えません");
        }
    }
}
