 using Godot;
using System;

public partial class Bullet : Area2D
{
    private const float BulletSpeed = 500.0f; // 子弹速度

    public override void _Ready()
    {
        /*CollisionLayer = 1;    // 属于Bullet层
        CollisionMask = 2;     // 检测Enemy层
        // 添加碰撞形状
        var collisionShape = new CollisionShape2D();
        var shape = new CircleShape2D();
        shape.Radius = 8;  // 根据实际子弹大小调整
        collisionShape.Shape = shape;
        AddChild(collisionShape);
        // 设置子弹的纹理（如果需要）
        var texture = new GradientTexture2D();
        var gradient = new Gradient();
        gradient.AddPoint(0, Colors.Red);
        texture.Gradient = gradient;
        texture.Width = 16; // 子弹宽度
        texture.Height = 16; // 子弹高度
        var sprite = new Sprite2D();

        // 方式1：直接加载图片
        //sprite.Texture = GD.Load<Texture2D>("res://assets/images/bullet.png");

        // 方式2：通过资源加载（推荐）
        // var bulletTexture = ResourceLoader.Load<Texture2D>("res://assets/images/bullet.png");
        // sprite.Texture = bulletTexture;

        // 调整显示参数
        //sprite.Centered = true;  // 确保中心点对齐
        //sprite.Scale = new Vector2(0.5f, 0.5f); // 按需缩放

       // AddChild(sprite);
        //var sprite = new Sprite2D();
        //sprite.Texture = texture;
        AddChild(sprite);*/
        CollisionLayer = 1;    // 属于Bullet层
        CollisionMask = 2;     // 检测Enemy层

        // 添加碰撞形状
        var collisionShape = new CollisionShape2D();
        var shape = new CircleShape2D();
        shape.Radius = 8;  // 根据实际子弹大小调整
        collisionShape.Shape = shape;
        AddChild(collisionShape);

        // 设置子弹的纹理
        var texture = new GradientTexture2D();
        var gradient = new Gradient();
        gradient.AddPoint(0, Colors.Red);
        texture.Gradient = gradient;
        texture.Width = 16; // 子弹宽度
        texture.Height = 16; // 子弹高度

        var sprite = new Sprite2D();
        sprite.Texture = texture;
        sprite.Centered = true;  // 确保中心点对齐
        sprite.Scale = new Vector2(0.5f, 0.5f); // 按需缩放
        AddChild(sprite);
    }

    public override void _Process(double delta)
    {
        // 子弹向右移动
        Position += new Vector2(1, 0) * BulletSpeed * (float)delta * 2;

        // 如果子弹超出屏幕，销毁它
        if (Position.X > GetViewport().GetVisibleRect().End.X)
        {
            QueueFree();
        }
    }
}
