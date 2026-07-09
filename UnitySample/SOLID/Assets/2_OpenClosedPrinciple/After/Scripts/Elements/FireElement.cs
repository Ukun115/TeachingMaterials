namespace SOLID.OpenClosed.After
{
    /// <summary>火の属性。風に強い。自分に関することは、この1ファイルの中だけで完結している。</summary>
    public class FireElement : IElement
    {
        public string Id => "Fire";
        public string DisplayName => "火";
        public string OnHitFlavor => "メラメラと燃え上がる！";

        /// <summary>火は風に強い。</summary>
        public bool IsStrongAgainst(IElement target) => target is WindElement;
    }
}
