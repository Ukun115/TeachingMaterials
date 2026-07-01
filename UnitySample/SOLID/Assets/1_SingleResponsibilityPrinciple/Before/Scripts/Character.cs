using UnityEngine;

namespace SOLID.SingleResponsibility.Before
{
    /// <summary>属性の種類。ダメージの相性判定に使う。</summary>
    public enum Element
    {
        Fire,  // 火
        Water, // 水
        Wind,  // 風
    }

    /// <summary>
    /// 1体のキャラの処理を、たった1つのクラスで全部かかえこんだ「神クラス」。
    /// ステータス／ダメージ計算／属性相性／戦闘演出／経験値と、役割が5つも混ざっている悪い例。
    /// この1ファイルを直すだけで、関係ない機能まで巻きぞえで壊しやすい。
    /// </summary>
    public class Character : MonoBehaviour
    {
        // ===== 役割① ステータス（このキャラの状態そのもの） =====
        public string characterName = "勇者";
        public int maxHp = 100;                 // 最大HP
        public int hp = 100;                    // 今のHP
        public int attack = 20;                 // 攻撃力
        public int defense = 5;                 // 防御力
        public Element element = Element.Fire;  // 属性
        public int expReward = 30;              // 倒されたとき、相手にあげる経験値

        // ===== 役割⑤ 経験値・レベル（本当は別の話なのに、状態のすぐ隣に同居している） =====
        public int level = 1;             // 今のレベル
        public int exp = 0;               // ためた経験値
        public int expToNextLevel = 100;  // 次のレベルに必要な経験値

        /// <summary>まだ生きているか（HPが1以上なら生存）。</summary>
        public bool IsAlive => hp > 0;

        // ===== 役割② ダメージ計算 =====
        /// <summary>
        /// 相手を1回攻撃する。
        /// このメソッド1つの中で「計算→相性→HP更新→演出→経験値」と、5つの役割を芋づる式に触ってしまっている。
        /// </summary>
        public void Attack(Character target)
        {
            // 自分か相手が倒れていたら、何もしない。
            if (!IsAlive || target == null || !target.IsAlive)
            {
                return;
            }

            // ① 基礎ダメージ ＝ 攻撃力 － 相手の防御力（最低でも1は入る）。
            int baseDamage = Mathf.Max(1, attack - target.defense);

            // ② 属性の相性で倍率をかける。
            float multiplier = GetElementMultiplier(element, target.element);
            int damage = Mathf.Max(1, Mathf.RoundToInt(baseDamage * multiplier));

            // ③ 相手のHPを直接けずる（相手の状態にまで手を突っこんでいる）。
            target.hp = Mathf.Max(0, target.hp - damage);

            // ④ 攻撃の演出を出す。
            PlayHitEffect(target, damage, multiplier);

            // ⑤ 倒したら、撃破の演出と経験値わたしまで面倒を見る。
            if (!target.IsAlive)
            {
                PlayDefeatEffect(target);
                GainExp(target.expReward);
            }
        }

        // ===== 役割③ 属性相性 =====
        /// <summary>攻撃属性と相手属性から、ダメージ倍率を返す。相性ルールを増やすたびに、この神クラスを触ることになる。</summary>
        private float GetElementMultiplier(Element attackElement, Element targetElement)
        {
            // 火→風、風→水、水→火 なら有利（2倍）。
            if ((attackElement == Element.Fire && targetElement == Element.Wind) ||
                (attackElement == Element.Wind && targetElement == Element.Water) ||
                (attackElement == Element.Water && targetElement == Element.Fire))
            {
                return 2.0f;
            }

            // その逆なら不利（0.5倍）。
            if ((attackElement == Element.Wind && targetElement == Element.Fire) ||
                (attackElement == Element.Water && targetElement == Element.Wind) ||
                (attackElement == Element.Fire && targetElement == Element.Water))
            {
                return 0.5f;
            }

            // それ以外は等倍。
            return 1.0f;
        }

        // ===== 役割④ 戦闘演出 =====
        /// <summary>攻撃が当たったときの演出。本当はSEやエフェクトを鳴らす場所（ここではログで代用）。</summary>
        private void PlayHitEffect(Character target, int damage, float multiplier)
        {
            // 相性によって、ひとことを変える。
            string relation = "";
            if (multiplier > 1f)
            {
                relation = "＜効果はバツグンだ！＞";
            }
            else if (multiplier < 1f)
            {
                relation = "＜効果はいまひとつ…＞";
            }

            Debug.Log($"【攻撃】{characterName} の攻撃！ {target.characterName} に {damage} ダメージ {relation}（残りHP {target.hp}/{target.maxHp}）");
        }

        /// <summary>相手が倒れたときの演出。派手にしたいだけでも、この神クラス全体を触るはめになる。</summary>
        private void PlayDefeatEffect(Character target)
        {
            Debug.Log($"【撃破】{target.characterName} は倒れた！");
        }

        // ===== 役割⑤ 経験値・レベル =====
        /// <summary>経験値をもらう。必要な分がたまったら、レベルアップする。</summary>
        public void GainExp(int amount)
        {
            exp += amount;
            Debug.Log($"【経験値】{characterName} は {amount} の経験値を得た！（{exp}/{expToNextLevel}）");

            // 一気に何レベルも上がることがあるので、足りているあいだくり返す。
            while (exp >= expToNextLevel)
            {
                exp -= expToNextLevel;
                LevelUp();
            }
        }

        /// <summary>レベルを1つ上げ、ステータスも強くする（役割①にまで踏みこんで書きかえている）。</summary>
        private void LevelUp()
        {
            level++;
            maxHp += 20;
            hp = maxHp; // レベルアップで全回復
            attack += 5;
            defense += 2;
            expToNextLevel += 50;
            Debug.Log($"【レベルアップ】{characterName} はレベル {level} に上がった！（HP {maxHp} / 攻撃 {attack} / 防御 {defense}）");
        }
    }
}
