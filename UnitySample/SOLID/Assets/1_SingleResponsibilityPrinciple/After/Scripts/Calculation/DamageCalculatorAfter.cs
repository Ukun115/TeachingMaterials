using UnityEngine;

namespace SOLID.SingleResponsibility.After
{
    /// <summary>1回の攻撃の結果をまとめた、小さなデータの箱。演出担当はこれを見て反応する。</summary>
    public readonly struct DamageInfoAfter
    {
        public readonly string SourceName; // 攻撃した人の名前
        public readonly int Amount;        // 与えたダメージ
        public readonly float Multiplier;  // 属性相性の倍率

        public DamageInfoAfter(string sourceName, int amount, float multiplier)
        {
            SourceName = sourceName;
            Amount = amount;
            Multiplier = multiplier;
        }
    }

    /// <summary>ダメージ計算「だけ」を担当する。計算式を変えたいときに触るのは、ここだけ。</summary>
    public static class DamageCalculatorAfter
    {
        /// <summary>攻撃側と相手のステータスから、与えるダメージを計算する。</summary>
        public static DamageInfoAfter Calculate(CharacterStatusAfter attacker, CharacterStatusAfter target)
        {
            // ① 基礎ダメージ ＝ 攻撃力 － 相手の防御力（最低でも1は入る）。
            int baseDamage = Mathf.Max(1, attacker.Attack - target.Defense);

            // ② 属性の相性で倍率をかける（相性の判定は、専門クラスにおまかせ）。
            float multiplier = ElementalAffinityAfter.GetMultiplier(attacker.Element, target.Element);
            int amount = Mathf.Max(1, Mathf.RoundToInt(baseDamage * multiplier));

            return new DamageInfoAfter(attacker.CharacterName, amount, multiplier);
        }
    }
}
