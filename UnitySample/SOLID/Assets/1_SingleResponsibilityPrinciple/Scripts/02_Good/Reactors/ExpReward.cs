using UnityEngine;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「経験値の付与」だけ。倒したときの通知に反応して経験値を出す。
    /// </summary>
    [RequireComponent(typeof(CharacterHealth))]
    public class ExpReward : MonoBehaviour
    {
        [SerializeField] int exp = 21;

        CharacterHealth health;

        void Awake() => health = GetComponent<CharacterHealth>();
        void OnEnable() => health.OnFainted += Grant;
        void OnDisable() => health.OnFainted -= Grant;

        void Grant() => Debug.Log($"[ExpReward] {health.DisplayName} を倒した！ 経験値 +{exp} 獲得（経験値担当）");
    }
}
