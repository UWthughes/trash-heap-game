using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{
    private int playerNum = 0;

    public int PlayerNumber
    {
        get
        {
            return playerNum;
        }
        set
        {
            playerNum = value;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
