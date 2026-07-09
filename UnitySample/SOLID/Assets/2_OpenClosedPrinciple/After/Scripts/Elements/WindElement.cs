namespace SOLID.OpenClosed.After
{
    /// <summary>風の属性。水に強い。</summary>
    public class WindElement : IElement
    {
        public string Id => "Wind";
        public string DisplayName => "風";
        public string OnHitFlavor => "鋭い風が切り裂く！";

        /// <summary>風は水に強い。</summary>
        public bool IsStrongAgainst(IElement target) => target is WaterElement;
    }
}
