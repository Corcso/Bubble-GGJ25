using Godot;
using System;

public partial class Player : XROrigin3D
{
    private XRInterface _xrInterface;

    public override void _Ready()
    {

        // Open XR Setup, from Godot documentation. 
        _xrInterface = XRServer.FindInterface("OpenXR");
        if (_xrInterface != null && _xrInterface.IsInitialized())
        {
            GD.Print("OpenXR initialized successfully");

            // Turn off v-sync!
            DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);

            // Change our main viewport to output to the HMD
            GetViewport().UseXR = true;
        }
        else
        {
            GD.Print("OpenXR not initialized, please check if your headset is connected");
        }


        // Player setup

        // Pressing Y (Left Controler) will trigger a recentre. Moving head to 0, 1.8 , 0 by adjusting XR origin. 
        GetNode<XRController3D>("./Left Hand").ButtonPressed += (string name) => {
            if (name == "by_button") Recentre();
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    private void Recentre() {
        // Recenter with an assumed height of 1.8 meters. 
        //GlobalPosition = new Vector3(0, 1.8f, 0) - GetNode<Node3D>("./Head").GlobalPosition;
        XRServer.CenterOnHmd( XRServer.RotationMode.DontResetRotation, true);
        
    }
}
