using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour 
{
    private StatBlock stats;
    Transform myTrans;
    private Dictionary<char, IAbility> spellbook;

	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
        spellbook = new Dictionary<char, IAbility>();
        spellbook.Add('A', new A_PracticeBurst(Color.green));
        spellbook.Add('B', new A_PracticeBurst(Color.red));
        spellbook.Add('X', new A_PracticeProjectile(Color.blue));
        spellbook.Add('Y', new A_PracticeProjectile(Color.yellow));
        spellbook.Add('R', new A_PracticeProjectile(Color.black));
        spellbook.Add('L', new A_PracticeProjectile(Color.magenta));
        stats = new StatBlock();
        stats.CC = this;
	}
	
    public void SetStatBlock(StatBlock newStats)
    {
        stats = newStats;
        stats.CC = this;
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
            spellbook[slot].OnActivate(stats);
        }
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
