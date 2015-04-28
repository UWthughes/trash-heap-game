using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public int numPlayers;
    public Text text;
    public bool running = false;
    
    public static GameManager gm;
    public static Color[] colors = { Color.green, Color.red, Color.blue, Color.yellow };

    public DamageNumber dmgNum;
    public Transform sparkProj;
    public Transform healthPickup;
	
    void Awake()
    {
        gm = this;
    }
    
    // Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            PlayerSpawner spawner = GameObject.FindGameObjectWithTag("PlayerSpawner").GetComponent<PlayerSpawner>();
            if (spawner != null)
            {
                spawner.SpawnPlayers(numPlayers);
            }
        }
    }

    public void StartGame (int players)
    {
        numPlayers = players;

        //load the new, empty scene
        //Application.LoadLevel("Junkyard");
        Application.LoadLevel("GeneratedJunkyard");
        
        //build the level
    }

    public void Quit()
    {
        Application.Quit();
    }

    internal static Race GetRace()
    {
        float r = Random.Range(0f, 1f);
        if (r < .33f)
            return new R_Meat();
        else if (r < .67f)
            return new R_Metal();
        else
            return new R_Electronic();
    }

    internal static Ability GetStartingAbility()
    {
        return null;
        //Customize a basic attack ability with slightly random parameters.
    }
}
