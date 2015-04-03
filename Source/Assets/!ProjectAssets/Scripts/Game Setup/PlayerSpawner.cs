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
            SetUpKeyboard();
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
        {
            SetUpGamepad(GamePad.Index.One);
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Two))
        {
            SetUpGamepad(GamePad.Index.Two);
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Three))
        {
            SetUpGamepad(GamePad.Index.Three);
        }
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Four))
        {
            SetUpGamepad(GamePad.Index.Four);
        }
    }

    private void SetUpGamepad(GamePad.Index pad)
    {
        CreateCharacter(currPlayer);
        XBoxInputHandler xbi = player.AddComponent<XBoxInputHandler>();
        xbi.CC = player.AddComponent<CharacterController>();
        xbi.padNum = pad;
        player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
        pa.AddTarget(player.GetComponent<Transform>());
        float r = Random.Range(0f, 1f);
        if (r < .33f)
            xbi.CC.SetRace(new R_Meat());
        else if (r < .67f)
            xbi.CC.SetRace(new R_Metal());
        else
            xbi.CC.SetRace(new R_Electronic());
        NextPlayer();
    }

    private void SetUpKeyboard()
    {
        CreateCharacter(currPlayer);
        KeyboardInputManager kim = player.AddComponent<KeyboardInputManager>();
        kim.CC = player.AddComponent<CharacterController>();
        keyboardClaimed = true;
        player.AddComponent<PlayerInfo>().PlayerNumber = (currPlayer);
        pa.AddTarget(player.GetComponent<Transform>());
        float r = Random.Range(0f, 1f);
        if (r < .33f)
            kim.CC.SetRace(new R_Meat());
        else if (r < .67f)
            kim.CC.SetRace(new R_Metal());
        else
            kim.CC.SetRace(new R_Electronic());
        NextPlayer();
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
        if (currPlayer >= players)
        {
            //Debug.Log("In the destroy block.");
            text.text = "";
            GameManager.gm.running = true; //more elaborate game launcher goes here.
            Destroy(this);
            return;
        } 
        text.color = GameManager.colors[currPlayer];
        currPlayer++;

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
