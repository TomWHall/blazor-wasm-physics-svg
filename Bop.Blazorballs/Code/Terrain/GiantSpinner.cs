using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Dynamics.Joints;

namespace Bop.Blazorballs.Code.Terrain;

public class GiantSpinner : GameObject
{
    private const float Density = 0.75f;
    private const float AxleWidth = 0.25f;
    
    private readonly Vector2 _position;
    private readonly Body _parentSpinnerBody;

    private readonly List<Fixture> _axleFixtures = [];
    
    public GiantSpinner(Game game, Vector2 position)
    {
        _position = position;
        
        // Main spinner
        _parentSpinnerBody = game.World!.CreateBody(Vector2.Zero, 0, BodyType.Dynamic);
        _parentSpinnerBody.IgnoreGravity = true;
        Bodies.Add(_parentSpinnerBody);

        _parentSpinnerBody.CreateRectangle(12, 1, Density, Vector2.Zero);
        _parentSpinnerBody.CreateRectangleRotated(12, 1, Density, Vector2.Zero, (float)Math.PI / 2);
        _axleFixtures.Add(_parentSpinnerBody.CreateCircle(AxleWidth, Density));

        // Anchor
        var anchorBody = game.World.CreateBody(Vector2.Zero, 0, BodyType.Static);
        anchorBody.IgnoreGravity = true;
        Bodies.Add(anchorBody);
        
        var wheelJoint = new WheelJoint(anchorBody, _parentSpinnerBody, Vector2.Zero, Vector2.Zero)
        {
            CollideConnected = false,
            MotorEnabled = true,
            MotorSpeed = -1000f, // Negative is clockwise
            MaxMotorTorque = 400.0f
        };
        game.World.Add(wheelJoint);
            
        // Child spinners
        const float spinnerDistance = 5.5f;
        
        List<Vector2> childSpinnerOffsets = [
            new Vector2(-spinnerDistance, 0),
            new Vector2(spinnerDistance, 0),
            new Vector2(0, spinnerDistance),
            new Vector2(0, -spinnerDistance)
        ];

        foreach (var spinnerOffset in childSpinnerOffsets)
        {
            var spinnerBody = game.World.CreateBody(spinnerOffset, 0, BodyType.Dynamic);
            spinnerBody.IgnoreGravity = true;
            Bodies.Add(spinnerBody);
                
            spinnerBody.CreateRectangle(6, 1, 1, Vector2.Zero);
            spinnerBody.CreateRectangleRotated(6, 1, 1, Vector2.Zero, (float)Math.PI / 2);
            _axleFixtures.Add(spinnerBody.CreateCircle(AxleWidth, Density));
            
            var revoluteJoint = JointFactory.CreateRevoluteJoint(game.World, _parentSpinnerBody, spinnerBody, spinnerOffset, Vector2.Zero);
            revoluteJoint.CollideConnected = false;
            revoluteJoint.MotorEnabled = true;
            revoluteJoint.MotorSpeed = -3f;
            revoluteJoint.MaxMotorTorque = 400.0f;
        }

        Bodies.ForEach(b => b.Position += position);
    }
    
    protected override string GetClassName(Fixture fixture)
    {
        if (_axleFixtures.Contains(fixture)) return "axle";
        
        return ReferenceEquals(fixture.Body, _parentSpinnerBody)
            ? "tiles-jupiter-blue"
            : "tiles-jupiter-red"; 
    }
    
    public override void Update()
    {
        // For some reason the main spinner won't stay positioned to its static anchor, so move it back
        _parentSpinnerBody.Position = _position;
    }
}