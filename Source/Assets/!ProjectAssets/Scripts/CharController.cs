using UnityEngine;
using UnityEngine.UI;
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
    public delegate void StruckHandler(AttackData attack);
    public event StruckHandler OnStruck;
    public delegate void HealthHandler(int currHealth, int maxHealth);
    public event HealthHandler OnHealthChanged;

    public Text damageText;
    private Canvas damageCanvas;

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
        //Get a random starting ability
        SetRace(GameManager.GetRace());
        foreach (KeyValuePair<char, Ability> kvp in spellbook)
            kvp.Value.OnEquip(stats);
        StartCoroutine(Ticker());
        cc = GetComponent<CharacterController>();
        if (cc == null)
            cc = gameObject.AddComponent<CharacterController>();
        cc.stepOffset = 0f;
	}
	

    public void SetRace (Race newRace)
    {
        race = newRace;
        stats = race;
        stats.CC = this;
        renderer.material.mainTexture = race.tex;
        race.RegisterRacialAbility();
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

    public void TakeDamage(AttackData attack)
    {
        OnStruck(attack);
        stats.TakeDamage(attack.effectiveDamage);
        if (OnHealthChanged != null)
            OnHealthChanged(stats.CurrHP, stats.MaxHP);
    }

    public void Heal(int amount)
    {
        stats.TakeDamage(-amount);
        if (OnHealthChanged != null)
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

    [ContextMenu("SpawnDamageNumber")]
    public void SpawnDamageTest()
    {
        SpawnDamageNumber(100);
    }


    internal void SpawnDamageNumber(int dmg)
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        //position.x = position.x + ((Random.value * 10f) - 5f);
        //DamageNumber dn = Instantiate(GameManager.gm.dmgNum, position, GameManager.gm.transform.rotation) as DamageNumber;//screenspace       
        DamageNumber dn = Instantiate(GameManager.gm.dmgNum, transform.position, GameManager.gm.dmgNum.transform.rotation) as DamageNumber;//worldspace    
        if (dmg >= 0)
        {
            dn.number = dmg.ToString();
            dn.color = Color.red;
        }
        else
        {
            dn.number = (-dmg).ToString();
            dn.color = Color.green;
        }
    }
}
