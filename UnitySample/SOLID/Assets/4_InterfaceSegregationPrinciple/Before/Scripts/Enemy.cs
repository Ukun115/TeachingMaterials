using System.Collections.Generic;
using UnityEngine;

namespace SOLID.InterfaceSegregation.Before
{
    /// <summary>
    /// パーティが立ち向かうボス。HPが高く、物理だけでは倒しきれない。
    /// 毎ターン、生きている仲間全員に範囲攻撃を放ってくる。
    /// この敵は「攻撃・魔法・回復」の役割とは無関係なので、IBattler は実装しない。
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public string enemyName = "闇の大魔王";
        public int maxHp = 480;
        public int hp = 480;
        public int defense = 3;    // 物理を軽減する防御力
        public int aoeDamage = 2;  // 範囲攻撃1回のダメージ

        public string Name => enemyName;
        public bool IsAlive => hp > 0;

        public void TakeDamage(int amount)
        {
            if (amount < 0)
            {
                amount = 0;
            }
            hp = Mathf.Max(0, hp - amount);
        }

        /// <summary>生きている仲間全員に、範囲攻撃でダメージを与える。</summary>
        public void AttackAll(List<PartyMember> party)
        {
            Debug.Log($"　▼ 敵の反撃：{Name} の闇の波動！ パーティ全体に {aoeDamage} ダメージ");
            foreach (var member in party)
            {
                if (!member.IsAlive)
                {
                    continue;
                }
                member.TakeDamage(aoeDamage);
                if (!member.IsAlive)
                {
                    Debug.Log($"　【戦闘不能】{member.Name} は倒れた…！");
                }
            }
        }
    }
}
