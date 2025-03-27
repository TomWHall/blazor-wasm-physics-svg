using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Bop.Blazorballs.Code.Terrain;

public class Ball : GameObject
{
    private const float Density = 0.5f;
    
    public Ball(Game game, Vector2 position)
    {
        var body = game.World!.CreateBody(position, 0, BodyType.Dynamic);
        body.LinearDamping = Config.LinearDamping;
        Bodies.Add(body);

        var circleFixture = body.CreateCircle(0.75f, Density);
        circleFixture.Restitution = 0.75f;
        circleFixture.Friction = 0.5f;
    }
    
    protected override string GetClassName(Fixture fixture) => "ball";
}