using UnityEngine;
using UnityEngine.UI;

namespace Solid.Srp
{
    /// <summary>
    /// 責任は「HPバーの表示」だけ。CharacterHealth の通知に反応して
    /// 前景(fill)の横幅と色を更新する。HP担当はこのバーの存在を知らない。
    ///
    /// fill は「左に揃え・縦いっぱい」に伸ばした Image。anchorMax.x を
    /// 残りHPの割合にすることで幅が変わる（スプライト不要で確実に描画される）。
    /// </summary>
    public class HpBar : MonoBehaviour
    {
        [SerializeField] CharacterHealth health;
        [SerializeField] RectTransform fill;

        static readonly Color High = new Color(0.30f, 0.80f, 0.35f);
        static readonly Color Mid = new Color(0.95f, 0.80f, 0.20f);
        static readonly Color Low = new Color(0.88f, 0.25f, 0.25f);

        Image fillImage;

        void Awake()
        {
            if (fill != null) fillImage = fill.GetComponent<Image>();
        }

        void OnEnable()
        {
            if (health == null || fill == null) return;
            health.OnDamaged += Refresh;
            health.OnHealed += Refresh;
            health.OnFainted += RefreshFaint;
        }

        void OnDisable()
        {
            if (health == null) return;
            health.OnDamaged -= Refresh;
            health.OnHealed -= Refresh;
            health.OnFainted -= RefreshFaint;
        }

        // 初回描画は全 Awake 完了後の Start で
        void Start()
        {
            if (health != null && fill != null) Apply();
        }

        void Refresh(int _) => Apply();
        void RefreshFaint() => Apply();

        void Apply()
        {
            float ratio = health.Max > 0 ? (float)health.Current / health.Max : 0f;
            ratio = Mathf.Clamp01(ratio);

            fill.anchorMin = new Vector2(0f, 0f);
            fill.anchorMax = new Vector2(ratio, 1f);
            fill.offsetMin = Vector2.zero;
            fill.offsetMax = Vector2.zero;

            if (fillImage != null)
                fillImage.color = ratio > 0.5f ? High : ratio > 0.25f ? Mid : Low;
        }
    }
}
