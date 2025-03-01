using Godot;
using System;

public partial class CollisionShape2d : CollisionShape2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        var rectShape = new RectangleShape2D();
        rectShape.Size = new Vector2(64, 64); // 
        collisionShape.Shape = rectShape;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
