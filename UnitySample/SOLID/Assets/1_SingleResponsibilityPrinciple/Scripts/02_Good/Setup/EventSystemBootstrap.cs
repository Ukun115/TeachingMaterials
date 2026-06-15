using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Solid.Srp
{
    /// <summary>
    /// uGUI のボタンクリックに必要な EventSystem を用意するだけの係。
    /// このプロジェクトは新 Input System 専用なので、対応する
    /// InputSystemUIInputModule を付けて既定アクションを割り当てる。
    /// （シーンに EventSystem を手置きしなくてもクリックが効くようにするため）
    /// </summary>
    public class EventSystemBootstrap : MonoBehaviour
    {
        void Awake()
        {
            if (EventSystem.current != null) return;

            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            var module = go.AddComponent<InputSystemUIInputModule>();
            module.AssignDefaultActions();
        }
    }
}
