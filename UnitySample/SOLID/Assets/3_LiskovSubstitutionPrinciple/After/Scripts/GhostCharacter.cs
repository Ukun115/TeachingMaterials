using UnityEngine;

namespace SOLID.Liskov.After
{
    /// <summary>
    /// ゴースト。物理攻撃が半分すり抜ける「打たれ強い敵」。
    /// Before と同じ“打たれ強さ”を狙っているが、表し方を変えた。
    ///
    /// ポイントは、親 Character の約束（1以上のダメージなら、HPは必ず1以上減る）を守ること。
    /// ダメージを軽減はしても、最低1は必ず通し、実際にHPを減らすのは親の TakeDamage に任せる。
    /// こうすれば Character として置き換えて使っても破綻しない＝リスコフの置換原則を満たす。
    /// 進行役 BattleRunner は一切書き換えていないのに、こんどはちゃんと決着がつく。
    /// </summary>
    public class GhostCharacter : Character
    {
        /// <summary>
        /// 物理は半分すり抜ける…が、完全には無効化しない。
        /// 親の約束を守るため、軽減後も最低1は必ず通し、base.TakeDamage で確実にHPを減らす。
        /// </summary>
        public override void TakeDamage(int amount)
        {
            int reduced = Mathf.Max(1, amount / 2);
            Debug.Log($"　→ {characterName} は攻撃を半分すり抜けた！（{amount} → {reduced} に軽減）");
            base.TakeDamage(reduced);
        }
    }
}
