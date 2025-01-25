using Godot;
using Godot.Collections;
using System;

public partial class TextRenderer3D : Node3D
{
	string text;

	[Export]
	Dictionary<char, Mesh> characterMeshes = new Dictionary<char, Mesh> {
		{ '0', null},
		{ '1', null},
		{ '2', null},
		{ '3', null},
		{ '4', null},
		{ '5', null},
		{ '6', null},
		{ '7', null},
		{ '8', null},
		{ '9', null},
		{ '.', null}
	};

	[Export]
	float characterWidth;

	[Export]
	Material materialForText;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateText(string newText) {
		text = newText;

		foreach (Node child in GetChildren()) {
			child.QueueFree();
		}

		for (int c = 0; c < newText.Length; ++c)
		{
			MeshInstance3D thisCharMeshIns = new MeshInstance3D();
			// If character doesnt have a mesh assigned, skip it. 
			if (!characterMeshes.ContainsKey(newText[c])) continue;
			// Set the mesh & material
			thisCharMeshIns.Mesh = characterMeshes[newText[c]];
			thisCharMeshIns.MaterialOverride = materialForText;
			// Get the offset from centre position
			float positionOffset = c - (newText.Length / 2.0f) + 0.5f;
			// Get the xValue based on spacing and width
			float xValue = positionOffset * (characterWidth);
			// Set the position based on this. 
			thisCharMeshIns.Position = new Vector3(xValue, 0, 0);
			// Add as a child of me
			AddChild(thisCharMeshIns);
		}
	}
}
