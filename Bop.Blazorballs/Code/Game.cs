using Bop.Blazorballs.Code.Terrain;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Bop.Blazorballs.Code;

public class Game
{
    public World? World;
    
    private DateTime _lastTickTime = DateTime.Now;

    public List<GameObject> GameObjects { get; set; } = new();

    public void Init()
    {
        World = new World
        {
            Gravity = new Vector2(0, Config.Gravity)
        };
        
        GameObjects.Add(new Walls(this));
        GameObjects.Add(new GiantSpinner(this, new Vector2(Config.SvgWidthUnits / 2, Config.SvgHeightUnits / 2)));

        GameObjects.Add(new Ball(this, new Vector2(4, Config.SvgHeightUnits - 3)));
        GameObjects.Add(new Ball(this, new Vector2(7, Config.SvgHeightUnits - 4)));
        GameObjects.Add(new Ball(this, new Vector2(17, Config.SvgHeightUnits - 5)));
        GameObjects.Add(new Ball(this, new Vector2(20, Config.SvgHeightUnits - 6)));
    }

    public async Task Tick()
    {
        await Task.Run(() =>
        {
            var now = DateTime.Now;
            var deltaTime = now.Subtract(_lastTickTime).TotalMilliseconds;
            var timeStep = (float)(deltaTime / 1000);
            _lastTickTime = now;

            // Let GameObjects do specific updates if necessary
            GameObjects.ForEach(go => go.Update());
            
            // Move the physics world forward
            World!.Step(timeStep);
        });
    }
}