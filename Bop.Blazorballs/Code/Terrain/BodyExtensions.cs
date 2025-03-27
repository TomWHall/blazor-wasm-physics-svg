using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Bop.Blazorballs.Code.Terrain;

public static class BodyExtensions
{
    public static Fixture CreateRectangleRotated(this Body body, float width, float height, float density, Vector2 offset, float rotation)
    {
        var vertices = PolygonTools.CreateRectangle(width / 2f, height / 2f);
        vertices.Rotate(rotation);
        vertices.Translate(ref offset);
        return body.CreateFixture(new PolygonShape(vertices, density));
    }
}