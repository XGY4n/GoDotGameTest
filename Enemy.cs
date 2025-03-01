using Godot;
using System;

public partial class Enemy : Area2D
{
    private Vector2 _velocity; // Speed and direction of the enemy

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        CollisionLayer = 2;    // 属于Enemy层
        CollisionMask = 1;     // 检测Bullet层
        // 添加碰撞形状
        var collisionShape = new CollisionShape2D();
        var shape = new CircleShape2D();
        shape.Radius = 15;  // 根据实际敌人大小调整
        collisionShape.Shape = shape;
        AddChild(collisionShape);
        // Set bullet texture (if needed)
        var texture = new GradientTexture2D();
        var gradient = new Gradient();
        gradient.AddPoint(0, Colors.Red);
        texture.Gradient = gradient;
        texture.Width = 30;
        texture.Height = 30;

        var sprite = new Sprite2D();
        sprite.Texture = texture;
        AddChild(sprite);

        // Set the initial position of the enemy off the right side of the screen
        int a = (int)GetViewportRect().Size.Y;
        Position = new Vector2(GetViewportRect().Size.X + 100, GD.RandRange(0, a));

        // Set random velocity
        _velocity = new Vector2(GD.RandRange(-100, -200), GD.RandRange(-50, 50));  // Move left with some random vertical movement
        AreaEntered += OnAreaEntered;

    }
    private void OnAreaEntered(Area2D area)
    {
        // 销毁敌人
        QueueFree();

        // 销毁子弹
        if (area is Bullet bullet)
        {
            bullet.QueueFree();
        }

        GD.Print("敌机被击中！");
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Move the enemy based on velocity
        Position += _velocity * (float)delta;

        // Optionally, bounce on the edges
        if (Position.Y < 0 || Position.Y > GetViewportRect().Size.Y)
        {
            _velocity.Y *= -1; // Reverse vertical direction if it hits the top or bottom
        }

        // Destroy the enemy if it goes off-screen
        if (Position.Y < 0)
        {
            QueueFree(); // Remove the enemy from the scene when it goes off-screen
        }
    }
}
