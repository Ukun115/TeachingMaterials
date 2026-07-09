using UnityEngine;

namespace SOLID.OpenClosed.Before
{
    /// <summary>
    /// バトルを進めるだけのシーン用ドライバー。
    /// 再生すると勇者と敵が交互に攻撃し、決着までを画面ログに流す。
    /// </summary>
    public class BattleRunner : MonoBehaviour
    {
        [SerializeField] private Character hero;
        [SerializeField] private Character enemy;

        private void Start()
        {
            if (hero == null || enemy == null)
            {
                Debug.LogWarning("BattleRunner に hero / enemy が設定されていません。");
                return;
            }

            string heroElement = ElementalAffinity.GetDisplayName(hero.element);
            string enemyElement = ElementalAffinity.GetDisplayName(enemy.element);
            Debug.Log($"【バトル開始】{hero.characterName}（{heroElement}） vs {enemy.characterName}（{enemyElement}）");

            int turn = 1;
            // どちらかが倒れるまで、勇者→敵の順で殴り合う。
            while (hero.IsAlive && enemy.IsAlive)
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

            string winner = hero.IsAlive ? hero.characterName : enemy.characterName;
            Debug.Log($"【決着】{winner} の勝利！");
        }
    }
}
