using UnityEngine;
using System.Collections;
using GamepadInput;

public class InputModeSelector : MonoBehaviour 
{
    private string displaystring = "Press Space on the keyboard for keyboard controls\nor press A on the XBox gamepad for gamepad controls.";

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown("space"))
        {
            KeyboardInputManager kim = gameObject.AddComponent<KeyboardInputManager>();
            displaystring = "Keyboard it is.";
            kim.CC = gameObject.AddComponent<CharacterController>();
            Destroy(this, 1.0f);
        }
        if (GamePad.GetButtonDown(GamePad.Button.A,GamePad.Index.One))
        {
            XBoxInputHandler xbi = gameObject.AddComponent<XBoxInputHandler>();
            xbi.CC = gameObject.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.One;
            Destroy(this, 1.0f);
            displaystring = "XBox controller 1 it is.";
        }
        if (GamePad.GetButtonDown(GamePad.Button.A,GamePad.Index.Two))
        {
            XBoxInputHandler xbi = gameObject.AddComponent<XBoxInputHandler>();
            xbi.CC = gameObject.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.Two;
            Destroy(this, 1.0f);
            displaystring = "XBox controller 2 it is.";
        }
        if (GamePad.GetButtonDown(GamePad.Button.A,GamePad.Index.Three))
        {
            XBoxInputHandler xbi = gameObject.AddComponent<XBoxInputHandler>();
            xbi.CC = gameObject.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.Three;
            Destroy(this, 1.0f);
            displaystring = "XBox controller 3 it is.";
        }
        if (GamePad.GetButtonDown(GamePad.Button.A,GamePad.Index.Four))
        {
            XBoxInputHandler xbi = gameObject.AddComponent<XBoxInputHandler>();
            xbi.CC = gameObject.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.Four;
            Destroy(this, 1.0f);
            displaystring = "XBox controller 4 it is.";
        }
	}

    void OnGUI()
    {
        GUI.Box(new Rect(10,10,400,40), displaystring);
    }
}
