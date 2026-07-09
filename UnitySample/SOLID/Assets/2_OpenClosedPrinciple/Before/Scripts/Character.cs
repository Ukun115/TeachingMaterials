using UnityEngine;

namespace SOLID.OpenClosed.Before
{
    /// <summary>
    /// 1体のキャラ。ステータスを持ち、相手を攻撃する。
    /// 攻撃の中では、属性の判定を ElementalAffinity にたよっている。
    /// キャラ自体は素直な作りだが、属性を増やすと ElementalAffinity 側が悲鳴を上げる。
    /// </summary>
    public class Character : MonoBehaviour
    {
        public string characterName = "勇者";
        public int maxHp = 100;               // 最大HP
        public int hp = 100;                  // 今のHP
        public int attack = 20;               // 攻撃力
        public int defense = 5;               // 防御力
        public Element element = Element.Fire; // 属性
        public int expReward = 30;            // 倒されたとき、相手にあげる経験値

        /// <summary>まだ生きているか（HPが1以上なら生存）。</summary>
        public bool IsAlive => hp > 0;

        /// <summary>相手を1回攻撃する。</summary>
        public void Attack(Character target)
        {
            if (!IsAlive || target == null || !target.IsAlive)
            {
                return;
            }

            // ① 基礎ダメージ ＝ 攻撃力 － 相手の防御力（最低でも1は入る）。
            int baseDamage = Mathf.Max(1, attack - target.defense);

            // ② 属性の相性で倍率をかける（判定は ElementalAffinity におまかせ）。
            float multiplier = ElementalAffinity.GetMultiplier(element, target.element);
            int damage = Mathf.Max(1, Mathf.RoundToInt(baseDamage * multiplier));

            // ③ 相手のHPをけずる。
            target.hp = Mathf.Max(0, target.hp - damage);

            // ④ 演出（相性のひとことと、属性ごとのフレーバー）。
            string relation = "";
            if (multiplier > 1f)
            {
                relation = "＜効果はバツグンだ！＞";
            }
            else if (multiplier < 1f)
            {
                relation = "＜効果はいまひとつ…＞";
            }

            string myName = ElementalAffinity.GetDisplayName(element);
            string flavor = ElementalAffinity.GetOnHitFlavor(element);
            Debug.Log($"【攻撃】{characterName}（{myName}）の攻撃！ {target.characterName} に {damage} ダメージ {relation} {flavor}（残りHP {target.hp}/{target.maxHp}）");

            // ⑤ 倒したら撃破ログ。
            if (!target.IsAlive)
            {
                Debug.Log($"【撃破】{target.characterName} は倒れた！");
            }
        }
    }
}
