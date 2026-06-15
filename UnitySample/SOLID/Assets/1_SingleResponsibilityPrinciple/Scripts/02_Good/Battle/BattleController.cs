using System;
using UnityEngine;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「プレイヤーのコマンドを受け取り、誰のHPに作用するか決める」だけ。
    /// 各ボタンの OnClick から呼ばれる。演出・経験値・表示の事情は知らない。
    /// プレイヤーが1回行動したら OnPlayerActed を知らせ、敵ターンは別係に任せる。
    /// </summary>
    public class BattleController : MonoBehaviour
    {
        [SerializeField] CharacterHealth enemy; // まもの（攻撃対象）
        [SerializeField] CharacterHealth hero;  // 勇者（回復対象）

        [SerializeField] int normalDamage = 8;
        [SerializeField] int strongDamage = 16;
        [SerializeField] int healAmount = 10;

        /// <summary>プレイヤーが1回行動した（敵ターンのトリガ）</summary>
        public event Action OnPlayerActed;

        bool Finished => enemy.IsFainted || hero.IsFainted;

        // --- ボタンの OnClick から呼ぶコマンド ---

        public void Attack()
        {
            if (BlockedByEnd()) return;
            Debug.Log("▶ コマンド：こうげき");
            enemy.TakeDamage(normalDamage);
            EndPlayerTurn();
        }

        public void StrongAttack()
        {
            if (BlockedByEnd()) return;
            Debug.Log("▶ コマンド：つよい一撃");
            enemy.TakeDamage(strongDamage);
            EndPlayerTurn();
        }

        public void Heal()
        {
            if (BlockedByEnd()) return;
            Debug.Log("▶ コマンド：かいふく");
            hero.Heal(healAmount);
            EndPlayerTurn();
        }

        bool BlockedByEnd()
        {
            if (Finished) Debug.Log("（決着がついています）");
            return Finished;
        }

        void EndPlayerTurn()
        {
            if (Finished) return; // 自分の行動で決着したら敵ターンなし
            OnPlayerActed?.Invoke();
        }
    }
}
