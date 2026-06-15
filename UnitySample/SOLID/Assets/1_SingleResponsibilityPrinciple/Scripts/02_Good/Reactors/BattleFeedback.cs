using UnityEngine;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「演出（効果音）」だけ。HPの通知に "反応する" だけ。
    /// HP担当（CharacterHealth）は、この演出の存在を一切知らない。
    /// </summary>
    [RequireComponent(typeof(CharacterHealth))]
    public class BattleFeedback : MonoBehaviour
    {
        [SerializeField] AudioClip hitSe;
        [SerializeField] AudioClip healSe;

        CharacterHealth health;

        void Awake() => health = GetComponent<CharacterHealth>();

        void OnEnable()
        {
            health.OnDamaged += PlayHit;
            health.OnHealed += PlayHeal;
        }

        void OnDisable()
        {
            health.OnDamaged -= PlayHit;
            health.OnHealed -= PlayHeal;
        }

        void PlayHit(int damage)
        {
            if (hitSe != null) AudioSource.PlayClipAtPoint(hitSe, transform.position);
            else Debug.Log($"[BattleFeedback] 🔊 {health.DisplayName}：ヒット効果音（演出担当 / dmg={damage}）");
        }

        void PlayHeal(int amount)
        {
            if (healSe != null) AudioSource.PlayClipAtPoint(healSe, transform.position);
            else Debug.Log($"[BattleFeedback] ✨ {health.DisplayName}：回復効果音（演出担当 / +{amount}）");
        }
    }
}
