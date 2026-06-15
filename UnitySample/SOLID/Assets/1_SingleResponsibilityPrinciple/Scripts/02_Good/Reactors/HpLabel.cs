using UnityEngine;
using UnityEngine.UI;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「HPの表示」だけ。CharacterHealth の通知に反応して Text を更新する。
    /// UI もまた "HPの通知に反応するだけの存在"。HP担当はUIを一切知らない。
    /// </summary>
    public class HpLabel : MonoBehaviour
    {
        [SerializeField] CharacterHealth health;
        [SerializeField] Text label;

        void Reset() => label = GetComponent<Text>();

        void OnEnable()
        {
            if (health == null || label == null) return;
            health.OnDamaged += Refresh;
            health.OnHealed += Refresh;
            health.OnFainted += RefreshFaint;
        }

        // 初回描画は全 Awake 完了後の Start で（Current が確定してから）
        void Start()
        {
            if (health != null && label != null) Refresh(0);
        }

        void OnDisable()
        {
            if (health == null) return;
            health.OnDamaged -= Refresh;
            health.OnHealed -= Refresh;
            health.OnFainted -= RefreshFaint;
        }

        void Refresh(int _)
        {
            label.text = $"{health.DisplayName}  HP {health.Current}/{health.Max}";
        }

        void RefreshFaint()
        {
            label.text = $"{health.DisplayName}  HP 0/{health.Max}（戦闘不能）";
        }
    }
}
