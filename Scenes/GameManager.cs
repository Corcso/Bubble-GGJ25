using Godot;
using System;

public partial class GameManager : Node
{

	int score;

	[Export]
	PackedScene regularBubble;

	[Export]
	Vector3 spawnAreaHalfDimentions;

    [Export]
    Vector3 spawnOrigin;

    [Export]
    Vector2 spawnSpeed;

    float timeSinceLastSpawn;
	float timeUntilNextSpawn;

	RandomNumberGenerator rng;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();

		timeUntilNextSpawn = rng.RandfRange(spawnSpeed.X, spawnSpeed.Y);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timeSinceLastSpawn += (float)delta;

		if (timeSinceLastSpawn >= timeUntilNextSpawn) {
			SpawnNewBubble();
            timeUntilNextSpawn = rng.RandfRange(spawnSpeed.X, spawnSpeed.Y);
        }
	}

	private void SpawnNewBubble() {
		Bubble newBubble = regularBubble.Instantiate<Bubble>();

		newBubble.Position = spawnOrigin +
			new Vector3(
				spawnAreaHalfDimentions.X * rng.RandfRange(-1, 1),
				spawnAreaHalfDimentions.Y * rng.RandfRange(-1, 1),
				spawnAreaHalfDimentions.Z * rng.RandfRange(-1, 1)
			);

		GetParent().AddChild(newBubble);
	}

	public void AddScore(int scoreToAdd) {
		score += scoreToAdd;

		// Todo Display Numbers
	}
}
