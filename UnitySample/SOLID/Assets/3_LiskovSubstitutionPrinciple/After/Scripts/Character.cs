using UnityEngine;

namespace SOLID.Liskov.After
{
    /// <summary>
    /// 敵味方に共通する、キャラの基底クラス。（Before と中身は同じ）
    /// TakeDamage には「1以上のダメージを渡せば、HPは必ず1以上減る」という約束がある。
    /// バトルの進行役は、この約束を信じて『攻撃し続ければ、いつかは倒せる』と考えて動く。
    /// この約束を守れる相手なら、どんな子クラスでも進行役に“置き換えて”渡してよい。
    /// </summary>
    public class Character : MonoBehaviour
    {
        public string characterName = "勇者";
        public int maxHp = 100;   // 最大HP
        public int hp = 100;      // 今のHP
        public int attack = 20;   // 攻撃力
        public int defense = 5;   // 防御力

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
            int damage = Mathf.Max(1, attack - target.defense);

            // ② 相手にダメージを渡す。実際にいくつ減ったかは、減らす前後のHPの差で見る。
            int hpBefore = target.hp;
            target.TakeDamage(damage);
            int dealt = hpBefore - target.hp;

            Debug.Log($"【攻撃】{characterName} の攻撃！ {target.characterName} に {dealt} ダメージ（残りHP {target.hp}/{target.maxHp}）");

            // ③ 倒したら撃破ログ。
            if (!target.IsAlive)
            {
                Debug.Log($"【撃破】{target.characterName} は倒れた！");
            }
        }

        /// <summary>
        /// ダメージを受ける。
        /// 【約束】1以上のダメージを渡されたら、HPは必ず1以上減る（そして0未満にはならない）。
        /// 子クラスがこの約束を破ると、Character として使っている進行役が破綻する。
        /// </summary>
        public virtual void TakeDamage(int amount)
        {
            hp = Mathf.Max(0, hp - amount);
        }
    }
}
