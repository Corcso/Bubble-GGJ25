using Godot;
using System;

public partial class MainMenu : Node3D
{

    [Export]
    PackedScene reactionGame;

    [Export]
    PackedScene frenzyGame;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_exit_button_button_pressed() {
		GetTree().Quit();
	}

	public void _on_begin_frenzy_button_pressed() {
		GetParent().AddChild(frenzyGame.Instantiate());
		QueueFree();
	}
	
	public void _on_begin_reaction_button_pressed() {
        GetParent().AddChild(reactionGame.Instantiate());
        QueueFree();
    }
}
