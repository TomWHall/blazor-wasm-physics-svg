namespace Bop.Blazorballs.Code.Rendering;

public class SvgCircleWrapper : SvgNodeWrapper
{
    public override SvgNodeType NodeType => SvgNodeType.Circle;
    
    public double Radius { get; set; }
    public required string Transform { get; set; }
}