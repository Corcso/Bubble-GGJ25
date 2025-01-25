using Godot;
using System;
using static Godot.OpenXRInterface;

public partial class PushableButton : Area3D
{
    [Signal]
    public delegate void ButtonPressedEventHandler();

    Node3D hand;
	Vector3 originalPosition;
	bool pressedCalledThisHand;

	const float HAND_MIN_DIST = (((0.62f / 2.0f) + 0.19f) * 0.2f + 0.00f);
	const float ACTIVATE_DIST = (((0.62f / 2.0f) + 0.19f) * 0.2f + 0.03f) * 0.5f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += (Area3D area) => {
			if (area.GetParentOrNull<XRController3D>() != null) {
				hand = area;
                EmitSignal(SignalName.ButtonPressed);
            }
		};

        AreaExited += (Area3D area) => {
            if (area.GetParentOrNull<XRController3D>() != null)
            {
                hand = null;
    //            GlobalPosition = originalPosition; 
				//pressedCalledThisHand = false;
            }
        };

        originalPosition = GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//if (hand != null)
		//{
		//	//         Vector3 toHand = originalPosition - hand.GlobalPosition;
		//	//float distance = GlobalBasis.Y.Dot(-toHand);

		//	//if (distance < HAND_MIN_DIST || true) {
		//	//	GlobalPosition = originalPosition + GlobalBasis.Y * (distance - HAND_MIN_DIST);
		//	//         }

		//	//if (distance < ACTIVATE_DIST) { 
		//	//	pressedCalledThisHand = true;
		//	//	EmitSignal(SignalName.ButtonPressed);
		//	//}

		//	GlobalPosition -= GlobalBasis.Y * 0.05f;
  //      }
		//else { 
		//	if (GlobalBasis.Y.Dot(originalPosition - GlobalPosition) > 0) GlobalPosition += GlobalBasis.Y * 0.05f;
  //      }
    }
}
