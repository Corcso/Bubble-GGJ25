using Godot;
using System;
using System.Collections.Generic;

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

	// For the bubbles sprite pop lazy billboarding method. 
	Node3D XRHeadReference;

	// Score text for rendering the players score. 
	TextRenderer3D scoreText;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();

		XRHeadReference = GetTree().Root.GetNode<Node3D>("./Game Root/XR Player/Head");

		// Get score text 
		scoreText = GetParent().GetNode<TextRenderer3D>("./Score Text");
		scoreText.UpdateText("0");

		timeUntilNextSpawn = rng.RandfRange(spawnSpeed.X, spawnSpeed.Y);
	}

	[Export]
	bool reactionTimeMode = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timeSinceLastSpawn += (float)delta;

		if (timeSinceLastSpawn >= timeUntilNextSpawn) {
			SpawnNewBubble();
            timeUntilNextSpawn = rng.RandfRange(spawnSpeed.X, spawnSpeed.Y);
			timeSinceLastSpawn = 0;
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

		newBubble.gameManager = this;
		newBubble.myWorth = 1;
		newBubble.pitchModulation = rng.RandfRange(0.8f, 1.9f);
		newBubble.XRHeadReference = XRHeadReference;


        GetParent().AddChild(newBubble);
	}

	public void AddScore(int scoreToAdd) {
		score += scoreToAdd;

		if(!reactionTimeMode) scoreText.UpdateText(score.ToString());
	}

	float averageTotal = 0;
	float averageNumber = 0;

    // For the reaction time gamemode. 
    public void AddTime(float timeTookToPop)
    {
		averageTotal += timeTookToPop;
		averageNumber++;

        if(reactionTimeMode) scoreText.UpdateText((Mathf.Round(averageTotal / averageNumber * 1000) / 1000.0f).ToString());
    }
}
