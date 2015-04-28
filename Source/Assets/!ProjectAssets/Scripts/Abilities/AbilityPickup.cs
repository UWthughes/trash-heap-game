using UnityEngine;
using System.Collections;

public class AbilityPickup : MonoBehaviour 
{
    public float rotateSpeed = 75f;
    public float lifetime = 20f;

    public Ability payload;

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
