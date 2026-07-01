using UnityEngine;

namespace SOLID.SingleResponsibility.After
{
    /// <summary>
    /// バトルの進行「だけ」を担当する。攻撃のたびに、計算・演出・経験値を各担当へ振り分けるだけ。
    /// 自分では中身のロジックを持たない（＝つなぎ役）。
    /// </summary>
    public class BattleRunnerAfter : MonoBehaviour
    {
        [SerializeField] private CharacterStatusAfter hero;
        [SerializeField] private CharacterStatusAfter enemy;
        [SerializeField] private ExperienceSystemAfter heroExperience;

        private void Start()
        {
            if (hero == null || enemy == null)
            {
                Debug.LogWarning("BattleRunner に hero / enemy が設定されていません。");
                return;
            }

            Debug.Log($"【バトル開始】{hero.CharacterName}（{hero.Element}） vs {enemy.CharacterName}（{enemy.Element}）");

            int turn = 1;
            // どちらかが倒れるまで、勇者→敵の順でくり返し攻撃する。
            while (hero.IsAlive && enemy.IsAlive)
            {
                Debug.Log($"── ターン {turn} ──");

                PerformAttack(hero, enemy, heroExperience);
                if (!enemy.IsAlive)
                {
                    break;
                }

                PerformAttack(enemy, hero, null);
                turn++;
            }

            string winner = hero.IsAlive ? hero.CharacterName : enemy.CharacterName;
            Debug.Log($"【決着】{winner} の勝利！");
        }

        /// <summary>1回の攻撃。計算→ダメージ適用→（倒したら）経験値付与を、それぞれの担当におまかせするだけ。</summary>
        private void PerformAttack(CharacterStatusAfter attacker, CharacterStatusAfter target, ExperienceSystemAfter attackerExperience)
        {
            // ① ダメージを計算してもらう。
            DamageInfoAfter info = DamageCalculatorAfter.Calculate(attacker, target);

            // ② 相手にダメージをわたす（演出は、相手のBattleFeedbackAfterが勝手に反応する）。
            target.TakeDamage(info);

            // ③ 倒していたら、経験値を入れてもらう。
            if (!target.IsAlive && attackerExperience != null)
            {
                attackerExperience.GainExp(target.ExpReward);
            }
        }
    }
}
