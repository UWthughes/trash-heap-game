using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour 
{
    private StatBlock stats;
    private Race race;
    Transform myTrans;
    private Dictionary<char, Ability> spellbook;

    public delegate void TickHandler();
    public event TickHandler OnTick;
    public delegate void StruckHandler();
    public event StruckHandler OnStruck;

	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
        spellbook = new Dictionary<char, Ability>();
        spellbook.Add('A', new A_PracticeBurst(Color.green));
        spellbook.Add('B', new A_PracticeBurst(Color.red));
        spellbook.Add('X', new A_PracticeProjectile(Color.blue));
        spellbook.Add('Y', new A_PracticeProjectile(Color.yellow));
        spellbook.Add('R', new A_PracticeProjectile(Color.black));
        spellbook.Add('L', new A_PracticeProjectile(Color.magenta));
        spellbook.Add('Z', new A_PracticePeriodic(Color.grey));
        stats = new StatBlock();
        race = new Race();
        stats.CC = this;
        foreach (KeyValuePair<char, Ability> kvp in spellbook)
            kvp.Value.OnEquip(ref stats);
        StartCoroutine(Ticker());
	}
	
    public void SetStatBlock(StatBlock newStats)
    {
        stats = newStats;
        stats.CC = this;
    }

    public void SetRace (Race newRace)
    {
        race = newRace;
        renderer.material.mainTexture = race.tex;
    }

    public void MoveAndFace(Vector3 moveDirection, Vector3 faceDirection)
    {
        myTrans.Translate(moveDirection * stats.moveSpeed * Time.deltaTime, Space.World);
        if (faceDirection.sqrMagnitude > .1)
            myTrans.eulerAngles = faceDirection;
            //myTrans.eulerAngles = new Vector3(0, Mathf.Atan2(faceDirection.x, faceDirection.y) * 57.2957795f, 0);
    }

    public void Cast(char slot)
    {
        if (spellbook.ContainsKey(slot))
        {
            spellbook[slot].OnActivate();
        }
    }

    public void LevelUp()
    {
        race.LevelUp(ref stats);
        Debug.Log("Leveled up.");
    }

    IEnumerator Ticker()
    {
        while (GameManager.gm.running)
        {
            OnTick();
            for (float timer = 0; timer < 1f; timer += Time.deltaTime)
                yield return 0;
        }
        yield return 0;
        //Debug.Log("This message appears after 3 seconds!");
    }

	// Update is called once per frame
	void Update () 
    {
	
	}

}
