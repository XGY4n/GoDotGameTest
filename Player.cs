using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
    private float colddown = 0.2f;
    private float colddownEnemy = 0.1f;
    private float shootTimerEnemy = 0.0f;
    private float shootTimer = 0.0f;
    public override void _Ready()
    {
        var texture = new GradientTexture2D();
        var gradient = new Gradient();
        gradient.AddPoint(0, Colors.White);
        texture.Gradient = gradient;
        texture.Width = 64; 
        texture.Height = 64; 
        GetNode<Sprite2D>("Sprite2D").Texture = texture;
    }
    [Export] 
    public float MoveSpeed = 300.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        // 
        Velocity = inputDirection * MoveSpeed * 2;
        MoveAndSlide();

        RestrictToScreenBounds();

        if (Input.IsActionPressed("ui_select") && shootTimer >= colddown) // 默认空格键对应 "ui_select" 输入动作
        {
            GD.Print(1);
            ShootBullet();
            shootTimer = 0;
        }
        shootTimer = shootTimer + (float)delta;
        if(shootTimerEnemy >= colddownEnemy)
        {
            EnemyGenerator();
            shootTimerEnemy = 0;
        }
        shootTimerEnemy = shootTimerEnemy + (float)delta;
    }

    private void EnemyGenerator()
    {
        Enemy enemy = new Enemy();
        GetParent().AddChild(enemy);

    }
    private void ShootBullet()
    {
        // 创建子弹实例
        Bullet bullet = new Bullet();

        // 设置子弹的位置
        bullet.Position = Position + new Vector2(64, 0); // 让子弹从玩家方块的右侧发射

        // 将子弹添加到场景中
        GetParent().AddChild(bullet); 
        
    }
    private void RestrictToScreenBounds()
    {
        // 获取视口可见范围
        var viewport = GetViewport().GetVisibleRect();
        var spriteSize = new Vector2(64, 64); 

        // 计算有效边界
        float minX = viewport.Position.X + spriteSize.X /  2;
        float maxX = viewport.End.X - spriteSize.X / 2;
        float minY = viewport.Position.Y + spriteSize.Y / 2;
        float maxY = viewport.End.Y - spriteSize.Y / 2;

        // 限制位置
        var clampedPosition = new Vector2(
            Mathf.Clamp(GlobalPosition.X, minX, maxX),
            Mathf.Clamp(GlobalPosition.Y, minY, maxY)
        );

        GlobalPosition = clampedPosition;
    }

}
