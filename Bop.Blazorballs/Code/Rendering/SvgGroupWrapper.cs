namespace Bop.Blazorballs.Code.Rendering;

public class SvgGroupWrapper : SvgNodeWrapper
{
    public override SvgNodeType NodeType => SvgNodeType.Group;

    public required string Transform { get; set; }
    public required List<SvgNodeWrapper> Children { get; set; }
}