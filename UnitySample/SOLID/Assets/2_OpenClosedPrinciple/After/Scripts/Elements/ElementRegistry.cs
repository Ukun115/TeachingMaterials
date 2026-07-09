using System.Collections.Generic;
using UnityEngine;

namespace SOLID.OpenClosed.After
{
    /// <summary>
    /// 合い言葉（Id）から属性クラスを取り出す名簿。
    /// 属性を増やすときに手を入れるのは、下の名簿に1行足すこのクラスだけ。
    /// 相性の計算やキャラのコードは、属性が何種類あっても書き換えなくてよい。
    /// </summary>
    public static class ElementRegistry
    {
        // ここに1行足すだけで新属性が仲間入りする（＝登録の1行）。
        private static readonly List<IElement> Elements = new List<IElement>
        {
            new FireElement(),
            new WaterElement(),
            new WindElement(),
            new ThunderElement(), // ← 雷はこの1行を足しただけ。
        };

        /// <summary>合い言葉から属性を探す。見つからなければ警告を出し、先頭の属性で代用する。</summary>
        public static IElement Get(string id)
        {
            foreach (IElement element in Elements)
            {
                if (element.Id == id)
                {
                    return element;
                }
            }

            Debug.LogWarning($"属性 '{id}' が名簿に見つかりません。ElementRegistry に登録されているか確認してください。");
            return Elements[0];
        }
    }
}
