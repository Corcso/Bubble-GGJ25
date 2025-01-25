using Godot;
using System;

public partial class MenuQuitter : Node
{
	[Export]
	PackedScene menu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		menu = ResourceLoader.Load<PackedScene>("res://Scenes/Main Menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_exit_button_button_pressed() {
		GetParent().GetParent().AddChild(menu.Instantiate());
		GetParent().QueueFree();
	}
}
