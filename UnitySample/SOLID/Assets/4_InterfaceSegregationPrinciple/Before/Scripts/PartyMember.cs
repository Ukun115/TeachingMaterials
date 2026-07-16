using UnityEngine;

namespace SOLID.InterfaceSegregation.Before
{
    /// <summary>
    /// パーティの仲間に共通する土台。名前とHP、そして「ダメージを受ける／回復する」を持つ。
    /// 戦士・魔法使い・僧侶は、みんなこのクラスを継承する。
    /// ここには「攻撃・魔法・回復」といった“できること（役割）”は入れていない。
    /// 役割はインターフェースの側で表す——その分け方がこの実習の主役。
    /// </summary>
    public abstract class PartyMember : MonoBehaviour
    {
        public string memberName = "仲間";
        public int maxHp = 50;   // 最大HP
        public int hp = 50;      // 今のHP

        public string Name => memberName;

        /// <summary>まだ戦えるか（HPが1以上なら生存）。</summary>
        public bool IsAlive => hp > 0;

        /// <summary>ダメージを受ける（0未満のHPにはならない）。</summary>
        public void TakeDamage(int amount)
        {
            if (amount < 0)
            {
                amount = 0;
            }
            hp = Mathf.Max(0, hp - amount);
        }

        /// <summary>HPを回復する（最大HPは超えない）。</summary>
        public void ReceiveHeal(int amount)
        {
            if (amount < 0)
            {
                amount = 0;
            }
            hp = Mathf.Min(maxHp, hp + amount);
        }
    }
}
