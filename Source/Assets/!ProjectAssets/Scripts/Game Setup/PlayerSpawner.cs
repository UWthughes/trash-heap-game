using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GamepadInput;

public class PlayerSpawner : MonoBehaviour 
{
    public Text text;

    private int players;
    private int currPlayer;
    private bool started;
    private bool keyboardClaimed = false;
    

    private GameObject player;
    private PositionAverager pa;

    void Start()
    {
        pa = new GameObject("Camera Target").AddComponent<PositionAverager>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().target = pa.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && keyboardClaimed == false)
        {
            CreateCharacter(currPlayer);
            KeyboardInputManager kim = player.AddComponent<KeyboardInputManager>();
            kim.CC = player.AddComponent<CharacterController>();
            keyboardClaimed = true;
            player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
            pa.AddTarget(player.GetComponent<Transform>());
            NextPlayer();
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
        {
            CreateCharacter(currPlayer);
            XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
            xbi.CC = player.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.One;
            player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
            pa.AddTarget(player.GetComponent<Transform>());
            NextPlayer();
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Two))
        {
            CreateCharacter(currPlayer);
            XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
            xbi.CC = player.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.Two;
            player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
            pa.AddTarget(player.GetComponent<Transform>());
            NextPlayer();
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Three))
        {
            CreateCharacter(currPlayer);
            XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
            xbi.CC = player.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.Three;
            player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
            pa.AddTarget(player.GetComponent<Transform>());
            NextPlayer();
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Four))
        {
            CreateCharacter(currPlayer);
            XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
            xbi.CC = player.AddComponent<CharacterController>();
            xbi.padNum = GamePad.Index.Four;
            player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
            pa.AddTarget(player.GetComponent<Transform>());
            NextPlayer();
        }
    }

    public void SpawnPlayers(int numPlayers)
    {
        started = true;
        players = numPlayers;
        currPlayer = 1;
        text.text = "Player 1\nPress Space for Keyboard\nor A for Gamepad Controls.";
        text.color = GameManager.colors[0];
    }

    private void NextPlayer()
    {
        text.color = GameManager.colors[currPlayer];
        currPlayer++;
        if (currPlayer > players)
        {
            //Debug.Log("In the destroy block.");
            text.text = "";
            Destroy(this);
            //send some sort of "start game" signal.
            return;
        }
        if (keyboardClaimed)
        {
            text.text = "Player " + currPlayer + "\nPress A for Gamepad Controls.";
        }
        else
        {
            text.text = "Player " + currPlayer + "\nPress Space for Keyboard\nor A for Gamepad Controls.";
        }
    }

    private void CreateCharacter(int x)
    {
        //spawn character object
        player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.GetComponent<Transform>().position = new Vector3(0f, .5f, 1f);
        player.GetComponent<Transform>().RotateAround(new Vector3(), new Vector3(0, 1, 0), x * 90f);

        //add components
    }
}
