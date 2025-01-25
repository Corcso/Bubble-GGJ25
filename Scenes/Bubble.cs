using Godot;
using System;
using static Godot.OpenXRInterface;

public partial class Bubble : Area3D
{
	bool dead = false;
	float timeInDead = 0.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        AreaEntered += (Area3D area) => {
			GetNode<AudioStreamPlayer3D>("./PopSFX").Play();
            GetNode<CpuParticles3D>("./PopParticles").Emitting = true;
            GetNode<MeshInstance3D>("./Mesh").Hide();
			dead = true;
        };
    }

	float speed = 0.1f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Vector3.Up * speed * (float)delta;

		if (dead) {

			//Scale *= timeInDead * 3;
			timeInDead += (float)delta;
			if(timeInDead > 0.33) QueueFree();
		}

    }
}
