using Godot;
using System;
using static Godot.OpenXRInterface;

public partial class Bubble : Area3D
{
	bool dead = false;
	float timeInDead = 0.0f;
	float timeAlive = 0.0f;

	public GameManager gameManager;

	// Lazy billboarding method. 
	public Node3D XRHeadReference;

	public int myWorth;

	// Pop pitch modulation, set by the game manager as it already has a RNG, doing it this way as it saves making an rng object per bubble. 
	public float pitchModulation;

	AnimatedSprite3D myPopSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myPopSprite = GetNode<AnimatedSprite3D>("./PopSprite");

        AreaEntered += (Area3D area) => {
			Pop();
		};
	}

	float speed = 0.1f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Vector3.Up * speed * (float)delta;
		Position += Vector3.Right * Mathf.Sin(timeAlive) * pitchModulation * 0.02f * (float)delta;
		Position += Vector3.Forward * Mathf.Sin(timeAlive + pitchModulation) * pitchModulation * 0.02f * (float)delta;

		timeAlive += (float)delta;
		
        if (dead) {
			myPopSprite.LookAt(XRHeadReference.GlobalPosition);
            timeInDead += (float)delta;
			if(timeInDead > 0.2) QueueFree();
		}

		if (timeAlive >= 10.0f) Pop();
	}

	public void Pop() {
        if (dead) return;
        GetNode<AudioStreamPlayer3D>("./PopSFX").Play();
        myPopSprite.Show();
        myPopSprite.Play();
        GetNode<MeshInstance3D>("./Mesh").Hide();
        if (gameManager != null)
        {
            gameManager.AddScore(myWorth);
            gameManager.AddTime(timeAlive);
        }
        dead = true;
    }
}
