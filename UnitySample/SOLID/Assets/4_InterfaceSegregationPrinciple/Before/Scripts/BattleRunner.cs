using System;
using System.Collections.Generic;
using UnityEngine;

namespace SOLID.InterfaceSegregation.Before
{
    /// <summary>
    /// バトルを進めるだけのシーン用ドライバー。
    /// 仲間を IBattler（＝攻撃も魔法も回復もできる建前）として一律に扱い、
    /// 毎ターン「全員で攻撃 → 全員で魔法 → 全員で回復」と号令をかける。
    ///
    /// ところが太った IBattler の建前はウソで、戦士は魔法も回復もできない。
    /// 号令をかけた瞬間、先頭の戦士が例外を投げ、そのフェーズごと巻き添えで止まる。
    /// 後ろに並ぶ魔法使いや僧侶の出番まで一緒に消し飛び、魔法も回復もパーティに通らない。
    /// 結果、ボスをいつまでも削りきれず、決着がつかなくなる。
    /// </summary>
    public class BattleRunner : MonoBehaviour
    {
        [SerializeField] private Warrior warrior;
        [SerializeField] private Mage mage;
        [SerializeField] private Priest priest;
        [SerializeField] private Enemy boss;

        // 保険の打ち切りターン。連携が崩れて決着がつかないときのため。
        [SerializeField] private int maxTurns = 20;

        private List<PartyMember> _party;

        private void Start()
        {
            if (warrior == null || mage == null || priest == null || boss == null)
            {
                Debug.LogWarning("BattleRunner に登場人物が設定されていません。");
                return;
            }

            // 隊列は 戦士 → 魔法使い → 僧侶 の順。先頭の戦士が“穴”を持つのがポイント。
            _party = new List<PartyMember> { warrior, mage, priest };

            Debug.Log($"【バトル開始】パーティ vs {boss.Name}（HP {boss.hp}）");

            int turn = 1;
            while (boss.IsAlive && IsPartyAlive() && turn <= maxTurns)
            {
                Debug.Log($"── ターン {turn} ──");

                // ① 物理フェーズ：全員が攻撃する。これは全員できるので問題なく通る。
                foreach (var member in _party)
                {
                    if (member.IsAlive)
                    {
                        ((IBattler)member).Attack(boss);
                    }
                }
                if (!boss.IsAlive)
                {
                    break;
                }

                // ② 魔法フェーズ：IBattler は「全員が魔法を使える」建前なので、全員に号令する。
                //    だが先頭の戦士が CastMagic で例外を投げ、フェーズごと中断してしまう。
                //    → 魔法を撃てるはずの魔法使いまで、巻き添えで出番を失う。
                Debug.Log("　▼ 魔法フェーズ：パーティ全員の魔法で追撃！");
                try
                {
                    foreach (var member in _party)
                    {
                        if (member.IsAlive)
                        {
                            ((IBattler)member).CastMagic(boss);
                        }
                    }
                }
                catch (NotSupportedException e)
                {
                    Debug.LogWarning($"　【魔法フェーズ中断】{e.Message}。連携が崩れ、魔法が一切通らなかった…！");
                }

                // ③ 回復フェーズ：同じく全員に号令。ここでも先頭の戦士で止まり、僧侶が回復に入れない。
                Debug.Log("　▼ 回復フェーズ：いちばん傷ついた仲間を回復！");
                try
                {
                    PartyMember weakest = FindWeakest();
                    foreach (var member in _party)
                    {
                        if (member.IsAlive)
                        {
                            ((IBattler)member).Heal(weakest);
                        }
                    }
                }
                catch (NotSupportedException e)
                {
                    Debug.LogWarning($"　【回復フェーズ中断】{e.Message}。誰も回復できなかった…！");
                }

                // ④ 敵の反撃：回復できていないパーティに、範囲攻撃が突き刺さる。
                boss.AttackAll(_party);

                turn++;
            }

            Conclude(turn - 1);
        }

        private bool IsPartyAlive()
        {
            return warrior.IsAlive || mage.IsAlive || priest.IsAlive;
        }

        /// <summary>生きている仲間のうち、いちばんHPが低い者を返す。</summary>
        private PartyMember FindWeakest()
        {
            PartyMember weakest = null;
            foreach (var member in _party)
            {
                if (!member.IsAlive)
                {
                    continue;
                }
                if (weakest == null || member.hp < weakest.hp)
                {
                    weakest = member;
                }
            }
            return weakest;
        }

        private void Conclude(int lastTurn)
        {
            if (!boss.IsAlive)
            {
                Debug.Log($"【勝利】{boss.Name} を倒した！ パーティの勝利！");
            }
            else if (!IsPartyAlive())
            {
                Debug.LogWarning("【敗北】パーティは全滅した…！ 連携がかみ合わなかった。");
            }
            else
            {
                Debug.LogWarning($"【異常】{maxTurns} ターン戦っても {boss.Name} を倒せない…！ 魔法も回復も、連携できていないのでは？");
            }
        }
    }
}
