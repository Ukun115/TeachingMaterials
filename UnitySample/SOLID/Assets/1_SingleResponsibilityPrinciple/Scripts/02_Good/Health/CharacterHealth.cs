using System;
using UnityEngine;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「HPの管理」だけ。誰が反応するかは一切知らない。
    /// ダメージ・回復・戦闘不能を "イベントで知らせる" だけに徹する。
    /// </summary>
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] string displayName = "キャラ";
        [SerializeField] int maxHp = 30;

        public string DisplayName => displayName;
        public int Max => maxHp;

        /// <summary>現在HP（読み取り専用）</summary>
        public int Current { get; private set; }
        public bool IsFainted => Current <= 0;

        /// <summary>ダメージを受けた（引数 = 受けたダメージ量）</summary>
        public event Action<int> OnDamaged;
        /// <summary>回復した（引数 = 回復量）</summary>
        public event Action<int> OnHealed;
        /// <summary>戦闘不能になった</summary>
        public event Action OnFainted;

        void Awake() => Current = maxHp; // HPの初期化

        public void TakeDamage(int amount)
        {
            if (IsFainted) return;

            Current = Mathf.Max(0, Current - amount);
            OnDamaged?.Invoke(amount);

            if (IsFainted) OnFainted?.Invoke();
        }

        public void Heal(int amount)
        {
            if (IsFainted) return; // 戦闘不能からは回復できない

            Current = Mathf.Min(maxHp, Current + amount);
            OnHealed?.Invoke(amount);
        }
    }
}
