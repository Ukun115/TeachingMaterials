using UnityEngine;

namespace Solid.Srp
{
    /// <summary>
    /// 【悪い例】単一責任の原則（SRP）違反。
    /// 1つのクラスに「ステータス・ダメージ計算・属性相性・効果音演出・経験値」を
    /// すべて詰め込んだ "神キャラクター"。変更する理由が 5 つもある。
    ///
    /// このスクリプトは対比用の参考コード。シーンには載せていません。
    /// 良い設計は CharacterHealth / BattleFeedback / ExperienceReceiver / FaintLogger を参照。
    /// </summary>
    public class GodCharacter : MonoBehaviour
    {
        public int hp = 30;
        public int attack = 10;
        public int defense = 5;
        public int level = 3;
        public int exp;
        public string type = "Water";
        public AudioClip se;

        public void TakeMove(int power, string moveType, GodCharacter attacker)
        {
            // 責任2：属性相性
            float eff = 1f;
            if (moveType == "Thunder" && type == "Water") eff = 2f;

            // 責任1：ダメージ計算
            int damage = (int)(attacker.attack * power / (float)defense * eff);
            hp -= damage;
            Debug.Log($"[GodCharacter] {damage} のダメージ！ 残りHP={hp}");

            // 責任3：効果音演出
            if (se != null) AudioSource.PlayClipAtPoint(se, transform.position);
            else Debug.Log("[GodCharacter] 🔊 効果音を鳴らす（演出）");

            if (hp <= 0)
            {
                Debug.Log("[GodCharacter] 倒れた！");
                attacker.GainExp(level * 7); // 責任4：経験値付与
            }
        }

        // 責任5：レベルアップ処理
        public void GainExp(int amount)
        {
            exp += amount;
            Debug.Log($"[GodCharacter] 経験値 +{amount}（合計 {exp}）");
        }
    }
}
