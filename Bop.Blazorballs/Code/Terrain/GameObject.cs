using Bop.Blazorballs.Code.Rendering;
using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Dynamics;

namespace Bop.Blazorballs.Code.Terrain;

public abstract class GameObject
{
    protected readonly List<Body> Bodies = [];
    
    public virtual List<SvgGroupWrapper> GetSvgGroupsForBodies()
    {
        return Bodies.Select(body => new SvgGroupWrapper
        {
            Transform = $"translate({body.Position.X}px, {Config.SvgHeightUnits - body.Position.Y}px) rotate({-body.Rotation}rad)",
            Children = body.FixtureList.Select(GetSvgNodeForShape).ToList(),
            ClassName = GetClassName(body)
        }).ToList();
    }

    protected virtual SvgNodeWrapper GetSvgNodeForShape(Fixture fixture)
    {
        var className = GetClassName(fixture);
        
        if (fixture.Shape.ShapeType == ShapeType.Polygon)
        {
            var polygonShape = (PolygonShape)fixture.Shape;
            return new SvgPolygonWrapper
            {
                Points = string.Join(" ", polygonShape.Vertices.Select(v => $"{v.X},{-v.Y}")),
                ClassName = className
            };
        }
        
        if (fixture.Shape.ShapeType == ShapeType.Circle)
        {
            var circleShape = (CircleShape)fixture.Shape;
            return new SvgCircleWrapper
            {
                Radius = circleShape.Radius,
                Transform = $"translate({circleShape.Position.X}px, {-circleShape.Position.Y}px)",
                ClassName = className
            };
        }

        throw new NotImplementedException($"Unimplemented Aether shape type: {fixture.Shape.ShapeType}");
    }

    protected virtual string? GetClassName(Body body)
    {
        return null;
    }
    
    protected virtual string? GetClassName(Fixture fixture)
    {
        return null;
    }

    public virtual void Update()
    {
    }
}