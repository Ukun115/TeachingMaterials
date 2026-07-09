namespace SOLID.OpenClosed.After
{
    /// <summary>水の属性。火に強い。</summary>
    public class WaterElement : IElement
    {
        public string Id => "Water";
        public string DisplayName => "水";
        public string OnHitFlavor => "水しぶきが激しく打ちつける！";

        /// <summary>水は火に強い。</summary>
        public bool IsStrongAgainst(IElement target) => target is FireElement;
    }
}
