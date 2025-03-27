using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Bop.Blazorballs.Code.Terrain;

public class Walls : GameObject
{
    private const int WallThickness = 1;
    private const float Density = 1f;

    private readonly List<Fixture> _rampFixtures = [];
    
    public Walls(Game game)
    {
        var body = game.World!.CreateBody(Vector2.Zero, 0, BodyType.Static);
        Bodies.Add(body);
        
        // Ramps
        const float rampBoxSize = Config.SvgHeightUnits * 0.65f;
        _rampFixtures.Add(body.CreateRectangleRotated(rampBoxSize, rampBoxSize, Density, Vector2.Zero, -(float)Math.PI / 4));
        _rampFixtures.Add(body.CreateRectangleRotated(rampBoxSize, rampBoxSize, Density, new Vector2(Config.SvgWidthUnits, 0), (float)Math.PI / 4));
        
        // Walls
        body.CreateRectangle(Config.SvgWidthUnits, WallThickness, Density, new Vector2(Config.SvgWidthUnits / 2, Config.SvgHeightUnits - (float)WallThickness / 2));
        body.CreateRectangle(Config.SvgWidthUnits, WallThickness, Density, new Vector2(Config.SvgWidthUnits / 2, (float)WallThickness / 2));
        body.CreateRectangle(WallThickness, Config.SvgHeightUnits - WallThickness * 2, Density, new Vector2((float)WallThickness / 2, Config.SvgHeightUnits / 2));
        body.CreateRectangle(WallThickness, Config.SvgHeightUnits - WallThickness * 2, Density, new Vector2(Config.SvgWidthUnits - (float)WallThickness / 2, Config.SvgHeightUnits / 2));
    }

    protected override string GetClassName(Fixture fixture) => _rampFixtures.Contains(fixture) ? "tiles-bathroom" : "tiles-lisbon";
}