using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
    public int HealthPoints;
    public int AttackPower;
    public int Strength;
    public int Intellect;
    public int Agility;

	// Use this for initialization
	void Start () {
        HealthPoints = 100;
        AttackPower = Random.Range(0, 100);
        Strength = Random.Range(0, 100);
        Intellect = Random.Range(0, 100);
        Agility = Random.Range(0, 100);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthPoints < 0)
            HealthPoints = 0;
        else if (HealthPoints > 100)
            HealthPoints = 100;
	}

    public void DealDamage(int attackPower)
    {
        HealthPoints = HealthPoints - attackPower;
    }
}
