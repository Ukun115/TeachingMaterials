namespace SOLID.Liskov.Before
{
    /// <summary>
    /// ゴースト。物理攻撃がすり抜けて効かない敵…のつもりで作った。
    /// そのために TakeDamage を「何もしない」で上書きしてしまった。
    ///
    /// これは基底 Character の約束（1以上のダメージなら、HPは必ず1以上減る）を破っている。
    /// つまり Character として置き換えて使えない子クラス＝リスコフの置換原則の違反。
    /// 進行役 BattleRunner は相手を Character として扱うので、この約束破りに気づけず、
    /// 「攻撃しても0ダメージ」がえんえん続き、いつまでも決着がつかなくなる。
    /// </summary>
    public class GhostCharacter : Character
    {
        /// <summary>
        /// 物理はすり抜ける…として、HPをまったく減らさない。
        /// ＝ 親の「必ず1以上減る」という約束を、子が勝手に取り消してしまった。
        /// </summary>
        public override void TakeDamage(int amount)
        {
            // あえて base.TakeDamage を呼ばず、HPを1も減らさない。
            UnityEngine.Debug.Log($"　→ {characterName} には攻撃がすり抜けた！（HPは減らない）");
        }
    }
}
