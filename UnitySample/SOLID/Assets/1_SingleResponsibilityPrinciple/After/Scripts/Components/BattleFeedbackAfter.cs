using UnityEngine;

namespace SOLID.SingleResponsibility.After
{
    /// <summary>
    /// 戦闘演出「だけ」を担当する。同じGameObjectのステータスの放送を聞いて、反応するだけ。
    /// 演出を派手にしたくなっても、直すのはこのクラスだけで済む。
    /// </summary>
    [RequireComponent(typeof(CharacterStatusAfter))] // 必ずCharacterStatusAfterと一緒に付ける
    public class BattleFeedbackAfter : MonoBehaviour
    {
        private CharacterStatusAfter _status;

        private void Awake()
        {
            _status = GetComponent<CharacterStatusAfter>();
        }

        // 放送を「聞き始める／やめる」。OnEnableで聞き始め、OnDisableでやめるのがお約束。
        private void OnEnable()
        {
            _status.Damaged += OnDamaged;
            _status.Died += OnDied;
        }

        private void OnDisable()
        {
            _status.Damaged -= OnDamaged;
            _status.Died -= OnDied;
        }

        /// <summary>ダメージを受けたと聞いたときの演出。本当はSEやエフェクトを鳴らす場所（ここではログで代用）。</summary>
        private void OnDamaged(DamageInfoAfter info)
        {
            // 相性によって、ひとことを変える。
            string relation = "";
            if (info.Multiplier > 1f)
            {
                relation = "＜効果はバツグンだ！＞";
            }
            else if (info.Multiplier < 1f)
            {
                relation = "＜効果はいまひとつ…＞";
            }

            Debug.Log($"【攻撃】{info.SourceName} の攻撃！ {_status.CharacterName} に {info.Amount} ダメージ {relation}（残りHP {_status.Hp}/{_status.MaxHp}）");
        }

        /// <summary>倒れたと聞いたときの演出。派手にしたいときも、直すのはこのクラスだけで済む。</summary>
        private void OnDied()
        {
            Debug.Log($"【撃破】{_status.CharacterName} は倒れた！");
        }
    }
}
