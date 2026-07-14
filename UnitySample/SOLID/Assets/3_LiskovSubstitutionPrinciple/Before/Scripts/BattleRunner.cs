using UnityEngine;

namespace SOLID.Liskov.Before
{
    /// <summary>
    /// バトルを進めるだけのシーン用ドライバー。
    /// 相手が勇者だろうがゴーストだろうが、すべて Character として同じように扱う。
    /// 「攻撃し続ければ、いつかは倒せる」＝ Character の約束を信じて組んである。
    /// ところが約束を破る子クラス（GhostCharacter）を渡されると、この進行役は破綻する。
    /// </summary>
    public class BattleRunner : MonoBehaviour
    {
        [SerializeField] private Character hero;
        [SerializeField] private Character enemy;

        // 保険の打ち切りターン。これが無いと、決着がつかないとき無限ループになる。
        [SerializeField] private int maxTurns = 30;

        private void Start()
        {
            if (hero == null || enemy == null)
            {
                Debug.LogWarning("BattleRunner に hero / enemy が設定されていません。");
                return;
            }

            Debug.Log($"【バトル開始】{hero.characterName} vs {enemy.characterName}");

            int turn = 1;
            // どちらかが倒れるまで、勇者→敵の順で殴り合う（打ち切りターンまで）。
            while (hero.IsAlive && enemy.IsAlive && turn <= maxTurns)
            {
                Debug.Log($"── ターン {turn} ──");

                hero.Attack(enemy);
                if (!enemy.IsAlive)
                {
                    break;
                }

                enemy.Attack(hero);
                turn++;
            }

            // 両者とも生き残ったまま打ち切りに達した＝決着がつかなかった。
            if (hero.IsAlive && enemy.IsAlive)
            {
                Debug.LogWarning($"【異常】{maxTurns} ターン戦っても決着がつかない…！ 攻撃が通っていないのでは？");
                return;
            }

            string winner = hero.IsAlive ? hero.characterName : enemy.characterName;
            Debug.Log($"【決着】{winner} の勝利！");
        }
    }
}
