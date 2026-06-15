using UnityEngine;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「敵の行動」だけ。プレイヤーが行動したら、自分が生きていれば
    /// 勇者を攻撃する。攻撃の"結果"（演出・HP表示・戦闘不能）は知らない
    /// ＝ target.TakeDamage に伝えるだけで、あとは各コンポーネントが反応する。
    /// </summary>
    [RequireComponent(typeof(CharacterHealth))]
    public class EnemyTurn : MonoBehaviour
    {
        [SerializeField] CharacterHealth target;   // 勇者
        [SerializeField] BattleController battle;   // 「プレイヤーが行動した」通知元
        [SerializeField] int power = 6;

        CharacterHealth self;

        void Awake() => self = GetComponent<CharacterHealth>();

        void OnEnable()
        {
            if (battle != null) battle.OnPlayerActed += Act;
        }

        void OnDisable()
        {
            if (battle != null) battle.OnPlayerActed -= Act;
        }

        void Act()
        {
            if (self.IsFainted || target == null || target.IsFainted) return;
            Debug.Log($"[EnemyTurn] {self.DisplayName} のこうげき！");
            target.TakeDamage(power);
        }
    }
}
