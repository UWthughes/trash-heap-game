using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour 
{
    public float turnSpeed = 100f;
    public float lifetime = 15f;
    public int healAmount = 100;

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, lifetime);
	}

    void OnCollisionEnter(Collision collision)
    {
        CharController cc = collision.collider.GetComponent<CharController>();
        if (cc != null)
        {
            cc.Heal(healAmount);
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(transform.up, turnSpeed * Time.deltaTime);
	}
}
