using System;
using UnityEngine;

namespace SOLID.SingleResponsibility.After
{
    /// <summary>
    /// このキャラの状態（名前・属性・攻撃/防御・HP）「だけ」を持つ担当。
    /// ダメージを受けたら数値を更新して「受けた！」と知らせるだけ。誰がそれに反応するかは知らない。
    /// </summary>
    public class CharacterStatusAfter : MonoBehaviour
    {
        // [SerializeField] を付けると、private でも Unity の Inspector から値を設定できる。
        [SerializeField] private string characterName = "勇者";
        [SerializeField] private ElementAfter element = ElementAfter.Fire; // 属性
        [SerializeField] private int attack = 20;    // 攻撃力
        [SerializeField] private int defense = 5;    // 防御力
        [SerializeField] private int maxHp = 100;    // 最大HP
        [SerializeField] private int hp = 100;       // 今のHP
        [SerializeField] private int expReward = 30; // 倒されたとき、相手にあげる経験値

        // 外からは「読み取り専用」で見せる（他人に勝手に書きかえられないようにする）。
        public string CharacterName => characterName;
        public ElementAfter Element => element;
        public int Attack => attack;
        public int Defense => defense;
        public int MaxHp => maxHp;
        public int Hp => hp;
        public int ExpReward => expReward;

        /// <summary>まだ生きているか（HPが1以上なら生存）。</summary>
        public bool IsAlive => hp > 0;

        // event は「できごとの放送」。放送するだけで、誰が聞いているかはこのクラスは気にしない。
        /// <summary>ダメージを受けたら放送する（受けたダメージ情報をわたす）。</summary>
        public event Action<DamageInfoAfter> Damaged;

        /// <summary>倒れたら放送する。</summary>
        public event Action Died;

        /// <summary>ダメージを受けてHPをけずり、「受けた！」（必要なら「倒れた！」）と放送する。</summary>
        public void TakeDamage(DamageInfoAfter info)
        {
            if (!IsAlive)
            {
                return;
            }

            hp = Mathf.Max(0, hp - info.Amount);

            // 「ダメージを受けた」と放送する。演出担当などが、勝手に聞いて反応してくれる。
            Damaged?.Invoke(info);

            if (hp == 0)
            {
                Died?.Invoke();
            }
        }

        /// <summary>ステータスを強くする。どれだけ上げるかは、呼び出し側（経験値担当）が決める。</summary>
        public void Grow(int hpUp, int attackUp, int defenseUp)
        {
            maxHp += hpUp;
            hp = maxHp; // レベルアップで全回復
            attack += attackUp;
            defense += defenseUp;
        }
    }
}
