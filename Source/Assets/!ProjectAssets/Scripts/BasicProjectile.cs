using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour 
{
    public float speed = 35f;

    Transform myTrans;
	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
        gameObject.AddComponent<Rigidbody>();
        Destroy(gameObject, 1.5f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        myTrans.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.GetComponent<EnemyStats>() != null)
        {
            //deal damage
            EnemyStats stats = col.gameObject.GetComponent<EnemyStats>();
            stats.DealDamage(1);// .HealthPoints = stats.HealthPoints - 1;
            Destroy(gameObject);
        }
        //show particles
    }
}
