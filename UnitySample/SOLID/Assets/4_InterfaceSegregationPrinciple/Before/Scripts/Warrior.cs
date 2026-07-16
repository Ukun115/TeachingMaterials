using System;
using UnityEngine;

namespace SOLID.InterfaceSegregation.Before
{
    /// <summary>
    /// 戦士。物理攻撃はパーティ一の頼れる前衛だが、魔法も回復もまったく使えない。
    /// なのに IBattler を実装したせいで、CastMagic と Heal まで書かされてしまった。
    /// 中身に書けることが無いので、やむなく例外を投げる“空の穴”になっている——これが違反の証拠。
    /// </summary>
    public class Warrior : PartyMember, IBattler
    {
        public int physicalPower = 18;

        public void Attack(Enemy target)
        {
            int damage = Mathf.Max(1, physicalPower - target.defense);
            target.TakeDamage(damage);
            Debug.Log($"【物理】{Name} の渾身の斬撃！ {target.Name} に {damage} ダメージ（残りHP {target.hp}/{target.maxHp}）");
        }

        /// <summary>戦士は魔法を使えない。IBattler に持てと言われたので仕方なく実装した“穴”。</summary>
        public void CastMagic(Enemy target)
        {
            throw new NotSupportedException($"{Name} は魔法を使えません");
        }

        /// <summary>戦士は回復もできない。これも書きようがなく、例外を投げるだけの“穴”。</summary>
        public void Heal(PartyMember target)
        {
            throw new NotSupportedException($"{Name} は回復魔法を使えません");
        }
    }
}
