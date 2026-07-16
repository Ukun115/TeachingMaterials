using UnityEngine;

namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// 戦士。頼れる物理前衛。（能力値は Before と同じ）
    /// 実装するのは IAttacker だけ。魔法も回復も“できないものは実装しない”。
    /// だから CastMagic や Heal の“空の穴”がそもそも存在しない。
    /// </summary>
    public class Warrior : PartyMember, IAttacker
    {
        public int physicalPower = 18;

        public void Attack(Enemy target)
        {
            int damage = Mathf.Max(1, physicalPower - target.defense);
            target.TakeDamage(damage);
            Debug.Log($"【物理】{Name} の渾身の斬撃！ {target.Name} に {damage} ダメージ（残りHP {target.hp}/{target.maxHp}）");
        }
    }
}
