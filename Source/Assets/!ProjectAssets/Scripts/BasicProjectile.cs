using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour 
{
    public float speed = 40f;

    Transform myTrans;
	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
        Destroy(gameObject, 1.5f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        myTrans.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
