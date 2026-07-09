using UnityEngine;

namespace SOLID.OpenClosed.After
{
    /// <summary>
    /// 1体のキャラ。ステータスを持ち、相手を攻撃する。
    /// 属性は「合い言葉」で持ち、実体（IElement）は名簿から取り出す。
    /// 属性が何種類に増えても、このキャラのコードは書き換えなくてよい。
    /// </summary>
    public class Character : MonoBehaviour
    {
        public string characterName = "勇者";
        public int maxHp = 100;   // 最大HP
        public int hp = 100;      // 今のHP
        public int attack = 20;   // 攻撃力
        public int defense = 5;   // 防御力

        [Tooltip("属性の合い言葉（Fire / Water / Wind / Thunder）。名簿 ElementRegistry から実体を取り出す。")]
        public string elementId = "Fire"; // 属性

        public int expReward = 30; // 倒されたとき、相手にあげる経験値

        // 合い言葉から取り出した属性の実体。最初に使うときに一度だけ引き当てる。
        private IElement _element;
        public IElement Element => _element ??= ElementRegistry.Get(elementId);

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
            float multiplier = ElementalAffinity.GetMultiplier(Element, target.Element);
            int damage = Mathf.Max(1, Mathf.RoundToInt(baseDamage * multiplier));

            // ③ 相手のHPをけずる。
            target.hp = Mathf.Max(0, target.hp - damage);

            // ④ 演出。属性ごとのひとことは、属性クラス自身が持っている（switch は不要）。
            string relation = "";
            if (multiplier > 1f)
            {
                relation = "＜効果はバツグンだ！＞";
            }
            else if (multiplier < 1f)
            {
                relation = "＜効果はいまひとつ…＞";
            }

            Debug.Log($"【攻撃】{characterName}（{Element.DisplayName}）の攻撃！ {target.characterName} に {damage} ダメージ {relation} {Element.OnHitFlavor}（残りHP {target.hp}/{target.maxHp}）");

            // ⑤ 倒したら撃破ログ。
            if (!target.IsAlive)
            {
                Debug.Log($"【撃破】{target.characterName} は倒れた！");
            }
        }
    }
}
