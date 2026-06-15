using UnityEngine;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「戦闘不能の通知ログ」だけ。OnFainted に反応するだけ。
    /// 「倒れたときの見せ方」を変えたくなっても、ここだけ直せばよい。
    /// </summary>
    [RequireComponent(typeof(CharacterHealth))]
    public class FaintLogger : MonoBehaviour
    {
        CharacterHealth health;

        void Awake() => health = GetComponent<CharacterHealth>();
        void OnEnable() => health.OnFainted += Log;
        void OnDisable() => health.OnFainted -= Log;

        void Log() => Debug.Log($"[FaintLogger] {health.DisplayName} は倒れた！（戦闘不能の通知担当）");
    }
}
