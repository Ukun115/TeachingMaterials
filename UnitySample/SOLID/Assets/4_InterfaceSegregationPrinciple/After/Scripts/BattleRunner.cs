using System.Collections.Generic;
using UnityEngine;

namespace SOLID.InterfaceSegregation.After
{
    /// <summary>
    /// バトルを進めるだけのシーン用ドライバー。（Before と同じ隊列・同じ敵）
    /// 太った IBattler をやめ、役割を IAttacker / IMagicUser / IHealer の3つに分けた。
    ///
    /// 号令をかけるときは「その役割ができる者だけ」に声をかける。
    ///   ・攻撃 … IAttacker を持つ者だけ
    ///   ・魔法 … IMagicUser を持つ者だけ
    ///   ・回復 … IHealer を持つ者だけ
    /// できない者に無理をさせないので、例外も“空の穴”も起きない。
    /// 魔法使いはちゃんと魔法を撃ち、僧侶はちゃんと回復し、パーティは連携してボスを倒せる。
    /// </summary>
    public class BattleRunner : MonoBehaviour
    {
        [SerializeField] private Warrior warrior;
        [SerializeField] private Mage mage;
        [SerializeField] private Priest priest;
        [SerializeField] private Enemy boss;

        // 保険の打ち切りターン。連携が効いていれば、ここに達する前に決着がつく。
        [SerializeField] private int maxTurns = 20;

        private List<PartyMember> _party;

        private void Start()
        {
            if (warrior == null || mage == null || priest == null || boss == null)
            {
                Debug.LogWarning("BattleRunner に登場人物が設定されていません。");
                return;
            }

            _party = new List<PartyMember> { warrior, mage, priest };

            Debug.Log($"【バトル開始】パーティ vs {boss.Name}（HP {boss.hp}）");

            int turn = 1;
            while (boss.IsAlive && IsPartyAlive() && turn <= maxTurns)
            {
                Debug.Log($"── ターン {turn} ──");

                // ① 物理フェーズ：攻撃できる者（IAttacker）だけが殴る。
                foreach (var member in _party)
                {
                    if (member.IsAlive && member is IAttacker attacker)
                    {
                        attacker.Attack(boss);
                    }
                }
                if (!boss.IsAlive)
                {
                    break;
                }

                // ② 魔法フェーズ：魔法を使える者（IMagicUser）だけが唱える。
                //    戦士や僧侶はそもそも声をかけられないので、例外は起きない。
                Debug.Log("　▼ 魔法フェーズ：魔法を使える仲間だけが唱える！");
                foreach (var member in _party)
                {
                    if (member.IsAlive && member is IMagicUser magicUser)
                    {
                        magicUser.CastMagic(boss);
                    }
                }

                // ③ 回復フェーズ：回復役（IHealer）だけが、いちばん傷ついた仲間を癒す。
                Debug.Log("　▼ 回復フェーズ：回復役だけが、いちばん傷ついた仲間を癒す！");
                PartyMember weakest = FindWeakest();
                foreach (var member in _party)
                {
                    if (member.IsAlive && member is IHealer healer)
                    {
                        healer.Heal(weakest);
                    }
                }

                // ④ 敵の反撃：範囲攻撃。ただし僧侶が回復してくれるので、パーティは持ちこたえる。
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
                Debug.Log($"【勝利】{boss.Name} を倒した！ 役割を分け合ったパーティの勝利！");
            }
            else if (!IsPartyAlive())
            {
                Debug.LogWarning("【敗北】パーティは全滅した…！");
            }
            else
            {
                Debug.LogWarning($"【異常】{maxTurns} ターン戦っても {boss.Name} を倒せなかった。");
            }
        }
    }
}
