using UnityEngine;

namespace SOLID.SingleResponsibility.After
{
    /// <summary>
    /// 経験値とレベル「だけ」を担当する。経験値をため、レベルアップの判定をする。
    /// 実際にステータスを強くするのはStatusにおまかせ。成長のさせ方を変えたいときに触るのは、ここだけ。
    /// </summary>
    [RequireComponent(typeof(CharacterStatusAfter))]
    public class ExperienceSystemAfter : MonoBehaviour
    {
        [SerializeField] private int level = 1;            // 今のレベル
        [SerializeField] private int exp = 0;              // ためた経験値
        [SerializeField] private int expToNextLevel = 100; // 次のレベルに必要な経験値

        private CharacterStatusAfter _status;

        private void Awake()
        {
            _status = GetComponent<CharacterStatusAfter>();
        }

        /// <summary>経験値をもらう。必要な分がたまったら、レベルアップする。</summary>
        public void GainExp(int amount)
        {
            exp += amount;
            Debug.Log($"【経験値】{_status.CharacterName} は {amount} の経験値を得た！（{exp}/{expToNextLevel}）");

            // 一気に何レベルも上がることがあるので、足りているあいだくり返す。
            while (exp >= expToNextLevel)
            {
                exp -= expToNextLevel;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            level++;
            expToNextLevel += 50;

            // 実際にステータスを強くするのは「状態の持ち主」におまかせ。ここは上げ幅を決めるだけ。
            _status.Grow(20, 5, 2);

            Debug.Log($"【レベルアップ】{_status.CharacterName} はレベル {level} に上がった！（HP {_status.MaxHp} / 攻撃 {_status.Attack} / 防御 {_status.Defense}）");
        }
    }
}
