namespace Bop.Blazorballs.Code.Rendering;

public class SvgPolygonWrapper : SvgNodeWrapper
{
    public override SvgNodeType NodeType => SvgNodeType.Polygon;
    
    public required string Points { get; set; }
}