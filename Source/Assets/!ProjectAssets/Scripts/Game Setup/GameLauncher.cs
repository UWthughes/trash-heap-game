using UnityEngine;
using System.Collections;
using GamepadInput;

public class GameLauncher : MonoBehaviour 
{
    private string[] playerLabels;
    private int playerCount = 0;
    private int currPlayer = 0;
    private GameObject player;
    private bool launching = false;
    private bool keyboardClaimed = false;
    private bool dying = false;
    private PositionAverager pa;

    private void CreateCharacter(int x)
    {
        //spawn character object
        player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.GetComponent<Transform>().position = new Vector3(0f, .5f, 1f);
        player.GetComponent<Transform>().RotateAround(new Vector3(), new Vector3(0, 1, 0), x * 90f);

        //add components

    }

    void Start()
    {
        playerLabels = new string[4];
        GameObject cameraTarget = new GameObject("Camera Target");
        pa = cameraTarget.AddComponent<PositionAverager>();
    }

    void OnGUI()
    {
        if (playerCount == 0)
        {
            GUI.Box(new Rect((Screen.width / 2) - 120, (Screen.height / 2) - 125, 240, 30), "How many players this session?");

            if (GUI.Button(new Rect((Screen.width / 2) - 120, (Screen.height / 2) - 85, 240, 30), "ONE"))
            {
                playerCount = 1;
                for (int i = 0; i < playerCount; i++)
                {
                    playerLabels[i] = "Player " + (i + 1) + " is using: ";
                }
                launching = true;
            }
            if (GUI.Button(new Rect((Screen.width / 2) - 120, (Screen.height / 2) - 45, 240, 30), "TWO"))
            {
                playerCount = 2;
                for (int i = 0; i < playerCount; i++)
                {
                    playerLabels[i] = "Player " + (i + 1) + " is using: ";
                }
                launching = true;
            }
            if (GUI.Button(new Rect((Screen.width / 2) - 120, (Screen.height / 2) - 5, 240, 30), "THREE"))
            {
                playerCount = 3;
                for (int i = 0; i < playerCount; i++)
                {
                    playerLabels[i] = "Player " + (i + 1) + " is using: ";
                }
                launching = true;
            }
            if (GUI.Button(new Rect((Screen.width / 2) - 120, (Screen.height / 2) + 35, 240, 30), "FOUR"))
            {
                playerCount = 4;
                for (int i = 0; i < playerCount; i++)
                {
                    playerLabels[i] = "Player " + (i + 1) + " is using: ";
                }
                launching = true;
            }
        }

        if (launching && currPlayer < playerCount)
        {
            if (keyboardClaimed == false)
                GUI.Box(new Rect((Screen.width / 2) - 200, (Screen.height / 2) - 125, 400, 50), "Player " + (currPlayer + 1) + ": Press Space on the keyboard for keyboard controls\nor press A on your XBox gamepad.");
            else
                GUI.Box(new Rect((Screen.width / 2) - 200, (Screen.height / 2) - 125, 400, 50), "Player " + (currPlayer + 1) + ": Press A on your XBox gamepad.");
        }

        GUI.Box(new Rect((Screen.width / 2) - 120, 10, 240, 10 + (15 * playerCount)), playerLabels[0] + "\n" + playerLabels[1] + "\n" + playerLabels[2] + "\n" + playerLabels[3]);
    }

    void Update()
    {
        if (currPlayer < playerCount)
        {
            if (Input.GetKeyDown("space") && keyboardClaimed == false)
            {
                CreateCharacter(currPlayer);
                KeyboardInputManager kim = player.AddComponent<KeyboardInputManager>();
                playerLabels[currPlayer] += "Keyboard";
                kim.CC = player.AddComponent<CharacterController>();
                currPlayer++;
                keyboardClaimed = true;
                player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
                pa.AddTarget(player.GetComponent<Transform>());
                //Destroy(this, 1.0f);
            }
            if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
            {
                CreateCharacter(currPlayer);
                XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
                xbi.CC = player.AddComponent<CharacterController>();
                xbi.padNum = GamePad.Index.One;
                playerLabels[currPlayer] += "XBox Pad 1";
                currPlayer++;
                player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
                pa.AddTarget(player.GetComponent<Transform>());
                //Destroy(this, 1.0f);
                //displaystring = "XBox controller 1 it is.";
            }
            if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Two))
            {
                CreateCharacter(currPlayer);
                XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
                xbi.CC = player.AddComponent<CharacterController>();
                xbi.padNum = GamePad.Index.Two;
                playerLabels[currPlayer] += "XBox Pad 2";
                currPlayer++;
                player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
                pa.AddTarget(player.GetComponent<Transform>());
                //Destroy(this, 1.0f);
                //displaystring = "XBox controller 2 it is.";
            }
            if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Three))
            {
                CreateCharacter(currPlayer);
                XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
                xbi.CC = player.AddComponent<CharacterController>();
                xbi.padNum = GamePad.Index.Three;
                playerLabels[currPlayer] += "XBox Pad 3";
                currPlayer++;
                player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
                pa.AddTarget(player.GetComponent<Transform>());
                //Destroy(this, 1.0f);
                //displaystring = "XBox controller 3 it is.";
            }
            if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Four))
            {
                CreateCharacter(currPlayer);
                XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
                xbi.CC = player.AddComponent<CharacterController>();
                xbi.padNum = GamePad.Index.Four;
                playerLabels[currPlayer] += "XBox Pad 4";
                currPlayer++;
                player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
                pa.AddTarget(player.GetComponent<Transform>());
                //Destroy(this, 1.0f);
                //displaystring = "XBox controller 4 it is.";
            }
        }
        else if (launching == true && dying == false)
        {
            dying = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().target = pa.GetComponent<Transform>();
            Destroy(this);
        }
    }
}
