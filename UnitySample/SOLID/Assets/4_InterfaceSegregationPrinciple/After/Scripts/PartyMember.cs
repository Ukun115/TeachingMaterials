using UnityEngine;

namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// パーティの仲間に共通する土台。（Before と中身は同じ）
    /// 名前とHP、そして「ダメージを受ける／回復する」だけを持つ。
    /// “できること（役割）”はこのクラスには入れず、小さなインターフェースの側で表す。
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
