using UnityEngine;

namespace SOLID.SingleResponsibility.Before
{
    /// <summary>
    /// 神クラスCharacterを動かすためだけのシーン用ドライバー。
    /// 再生すると勇者と敵が交互に攻撃し、決着までをConsoleに出力する。
    /// </summary>
    public class BattleRunner : MonoBehaviour
    {
        public Character hero;
        public Character enemy;

        private void Start()
        {
            if (hero == null || enemy == null)
            {
                Debug.LogWarning("BattleRunner に hero / enemy が設定されていません。");
                return;
            }

            Debug.Log($"【バトル開始】{hero.characterName}（{hero.element}） vs {enemy.characterName}（{enemy.element}）");

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
