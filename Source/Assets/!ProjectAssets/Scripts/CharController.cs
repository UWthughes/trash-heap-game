using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharController : MonoBehaviour 
{
    public StatBlock stats;
    private Race race;
    Transform myTrans;
    private Dictionary<char, Ability> spellbook;
    private CharacterController cc;
    public PlayerUIHandler pui;

    public delegate void TickHandler();
    public event TickHandler OnTick;
    public delegate void StruckHandler(ref AttackData attack);
    public event StruckHandler OnStruck;
    public delegate void HealthHandler(int currHealth, int maxHealth);
    public event HealthHandler OnHealthChanged;

	// Use this for initialization
	public void SetUp () 
    {
        spellbook = new Dictionary<char, Ability>();
        spellbook.Add('A', new A_PracticeBurst(Color.green));
        spellbook.Add('B', new A_PracticeBurst(Color.red));
        spellbook.Add('X', new A_PracticeProjectile(Color.blue));
        spellbook.Add('Y', new A_PracticeProjectile(Color.yellow));
        spellbook.Add('R', new A_PracticeProjectile(Color.black));
        spellbook.Add('L', new A_PracticeProjectile(Color.magenta));
        spellbook.Add(',', new A_B_Armor());
        spellbook.Add('.', new A_B_Dodge());
        spellbook.Add('?', new A_B_Parry());
        //spellbook.Add('Z', new A_PracticePeriodic(Color.cyan));
        stats = new StatBlock();
        race = new Race();
        stats.CC = this;
        foreach (KeyValuePair<char, Ability> kvp in spellbook)
            kvp.Value.OnEquip(ref stats);
        StartCoroutine(Ticker());
        cc = GetComponent<CharacterController>();
        if (cc == null)
            cc = gameObject.AddComponent<CharacterController>();
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
        //moveDirection = transform.TransformDirection(moveDirection);
        cc.Move(moveDirection * stats.moveSpeed * Time.deltaTime);
        
        if (faceDirection.sqrMagnitude > .1)
            transform.eulerAngles = faceDirection;
            //myTrans.eulerAngles = new Vector3(0, Mathf.Atan2(faceDirection.x, faceDirection.y) * 57.2957795f, 0);
    }

    void Update()
    {
        /*if (Time.frameCount % 30 == 0)
        {
            Debug.Log("Bonked.");
            stats.TakeDamage(25);
        }*/
    }

    public void TakeDamage(ref AttackData attack)
    {
        OnStruck(ref attack);
        stats.TakeDamage(attack.effectiveDamage);
        OnHealthChanged(stats.CurrHP, stats.MaxHP);
    }

    public void Cast(char slot)
    {
        if (spellbook.ContainsKey(slot))
        {
            if (spellbook[slot].RemainingCooldown < .001f)
                pui.StartCooldown(slot, spellbook[slot].TotalCooldown);
            spellbook[slot].OnActivate();
            
        }
    }

    public void ModifyAttack(ref AttackData ad)
    {
        //ad.effectiveDamage = ad.baseDamage * (strength)
        //calculate crit chance / modify accordingly
    }

    /*public void LevelUp()
    {
        race.LevelUp(ref stats);
        Debug.Log("Leveled up.");
    }*/

    IEnumerator Ticker()
    {
        while (true)
        {
            if(GameManager.gm.running)
                OnTick();
            yield return new WaitForSeconds(1f);
        }
    }
}
