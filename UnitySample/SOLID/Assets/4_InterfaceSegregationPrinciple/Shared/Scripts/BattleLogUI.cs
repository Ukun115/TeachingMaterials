using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SOLID.InterfaceSegregation.Shared
{
    /// <summary>
    /// Debug.Logの内容をCanvas上のテキストに映すだけのシーン用UI。
    /// 表示パネルは実行時に生成し、日本語はOSフォントを動的ロードして描画する。
    /// バトルの進行はログを1行ずつ流して見せる。
    /// </summary>
    public class BattleLogUI : MonoBehaviour
    {
        [SerializeField] private string title = "インターフェース分離の原則：バトルログ";
        [SerializeField] private int fontSize = 24;
        [SerializeField] private int maxLines = 16;
        [SerializeField] private float lineInterval = 0.6f;

        private Text _titleText;
        private Text _logText;
        private readonly Queue<string> _pending = new Queue<string>();
        private readonly List<string> _visible = new List<string>();
        private float _timer;

        private void Awake()
        {
            BuildUI();
        }

        private void OnEnable()
        {
            Application.logMessageReceived += OnLogReceived;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= OnLogReceived;
        }

        private void Update()
        {
            if (_pending.Count == 0)
            {
                return;
            }

            // 一定間隔で1行ずつ表示し、バトルが進んでいく感じを出す。
            _timer += Time.deltaTime;
            if (_timer < lineInterval)
            {
                return;
            }

            _timer = 0f;
            _visible.Add(_pending.Dequeue());
            if (_visible.Count > maxLines)
            {
                _visible.RemoveRange(0, _visible.Count - maxLines);
            }
            _logText.text = string.Join("\n", _visible);
        }

        /// <summary>受け取ったログを表示待ちの列に積む（通常ログと警告のみ）。</summary>
        private void OnLogReceived(string condition, string stackTrace, LogType type)
        {
            if (type != LogType.Log && type != LogType.Warning)
            {
                return;
            }
            _pending.Enqueue(condition);
        }

        /// <summary>背景・タイトル・ログ本文をコードで組み立てる。</summary>
        private void BuildUI()
        {
            Font font = LoadJapaneseFont();

            // 半透明の黒背景。
            var panel = CreateChild("LogPanel", transform);
            Stretch(panel, 20, 20, 20, 20);
            var bg = panel.gameObject.AddComponent<Image>();
            bg.color = new Color(0f, 0f, 0f, 0.6f);

            // 上部のタイトル。
            var titleRect = CreateChild("Title", panel);
            Stretch(titleRect, 24, 24, 24, 24);
            titleRect.anchorMin = new Vector2(0f, 1f);
            titleRect.anchorMax = new Vector2(1f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.sizeDelta = new Vector2(-48, fontSize + 12);
            titleRect.anchoredPosition = new Vector2(0f, -20f);
            _titleText = titleRect.gameObject.AddComponent<Text>();
            _titleText.font = font;
            _titleText.fontSize = fontSize + 4;
            _titleText.fontStyle = FontStyle.Bold;
            _titleText.color = new Color(1f, 0.85f, 0.4f, 1f);
            _titleText.alignment = TextAnchor.UpperLeft;
            _titleText.text = title;

            // ログ本文。上から下へ積み上げる。
            var log = CreateChild("Log", panel);
            Stretch(log, 24, 24, 24 + fontSize + 24, 24);
            _logText = log.gameObject.AddComponent<Text>();
            _logText.font = font;
            _logText.fontSize = fontSize;
            _logText.color = Color.white;
            _logText.alignment = TextAnchor.UpperLeft;
            _logText.horizontalOverflow = HorizontalWrapMode.Wrap;
            _logText.verticalOverflow = VerticalWrapMode.Overflow;
            _logText.text = string.Empty;
        }

        /// <summary>日本語が出せるOSフォントを動的に取得する（見つからなければ既定フォント）。</summary>
        private Font LoadJapaneseFont()
        {
            string[] candidates =
            {
                "Hiragino Sans",
                "Hiragino Kaku Gothic ProN",
                "YuGothic",
                "Yu Gothic",
                "Arial Unicode MS",
                "Arial",
            };
            return Font.CreateDynamicFontFromOSFont(candidates, fontSize);
        }

        private static RectTransform CreateChild(string name, Transform parent)
        {
            var go = new GameObject(name, typeof(RectTransform));
            var rt = go.GetComponent<RectTransform>();
            rt.SetParent(parent, false);
            return rt;
        }

        /// <summary>親いっぱいに広げ、四辺に余白（px）を取る。</summary>
        private static void Stretch(RectTransform rt, float top, float right, float bottom, float left)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.offsetMin = new Vector2(left, bottom);
            rt.offsetMax = new Vector2(-right, -top);
        }
    }
}
