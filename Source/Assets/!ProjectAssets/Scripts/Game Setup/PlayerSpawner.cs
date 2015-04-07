using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GamepadInput;

public class PlayerSpawner : MonoBehaviour 
{
    public Text text;
    public Transform selectCircle;

    private int players;
    private int currPlayer;
    private bool started;
    private bool keyboardClaimed = false;

    public Image[] healthBars;
    public Image[] abilityCDsA;
    public Image[] abilityCDsB;
    public Image[] abilityCDsX;
    public Image[] abilityCDsY;


    private GameObject player;
    private PositionAverager pa;

    void Start()
    {
        pa = new GameObject("Camera Target").AddComponent<PositionAverager>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().target = pa.transform;
        //canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
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
        xbi.CC = player.AddComponent<CharController>();
        xbi.CC.SetUp();
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
        AssignUI(xbi.CC, currPlayer);
        NextPlayer();
    }

    private void SetUpKeyboard()
    {
        CreateCharacter(currPlayer);
        KeyboardInputManager kim = player.AddComponent<KeyboardInputManager>();
        kim.CC = player.AddComponent<CharController>();
        kim.CC.SetUp();
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
        AssignUI(kim.CC, currPlayer);
        NextPlayer();
    }

    private void AssignUI(CharController charController, int currPlayer)
    {
        //Debug.Log("Inside AssignUI for " + currPlayer);
        PlayerUIHandler pu = charController.gameObject.AddComponent<PlayerUIHandler>();
        pu.SetUp(healthBars[currPlayer - 1], abilityCDsX[currPlayer - 1], abilityCDsY[currPlayer - 1], abilityCDsA[currPlayer - 1], abilityCDsB[currPlayer - 1], charController);
        charController.pui = pu;
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
            text.text = "";
            GameManager.gm.running = true; //more elaborate game launcher goes here.
            //disable unused UI bits
            for (int i = currPlayer; i < 4; i++)
            {
                healthBars[i].transform.parent.parent.gameObject.SetActive(false);
                abilityCDsA[i].transform.parent.gameObject.SetActive(false);
                abilityCDsB[i].transform.parent.gameObject.SetActive(false);
                abilityCDsX[i].transform.parent.gameObject.SetActive(false);
                abilityCDsY[i].transform.parent.gameObject.SetActive(false);
            }
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
        player.GetComponent<Transform>().position = new Vector3(0f, 1.55f, 1f);
        player.GetComponent<Transform>().RotateAround(new Vector3(), new Vector3(0, 1, 0), x * 90f);
        player.name = "Player " + x;
		player.tag = "Player";
        Transform circle = (Transform)Instantiate(selectCircle, player.transform.position, player.transform.rotation);
        circle.SetParent(player.transform);
        circle.renderer.material.color = GameManager.colors[x-1];
        circle.Rotate(Vector3.right, 90f);
        circle.Rotate(Vector3.forward, -90f);
        circle.Translate(new Vector3(0, 0, 1f));
        circle.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        Destroy(circle.GetComponent<Collider>());
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        //add components
    }
}
