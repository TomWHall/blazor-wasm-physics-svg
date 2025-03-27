namespace Bop.Blazorballs.Code.Rendering;

public abstract class SvgNodeWrapper
{
    public abstract SvgNodeType NodeType { get; }
    public string? ClassName { get; set; }
}