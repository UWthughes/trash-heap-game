using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour 
{
    public float speed = 40f;
    public float lifetime = 1.5f;

    Transform myTrans;
    Rigidbody myRB;
	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
        myRB = GetComponent<Rigidbody>();
        if (myRB == null)
        {
            myRB = gameObject.AddComponent<Rigidbody>();
        }
        myRB.useGravity = false;
        myRB.velocity = myTrans.forward * speed;
        gameObject.layer = LayerMask.NameToLayer("Projectile");
        Destroy(gameObject, lifetime);
	}

    void FixedUpdate()
    {
        //myTrans.Translate(Vector3.forward * speed * Time.deltaTime);
        if (myRB.velocity.magnitude < speed)
            myRB.AddForce(myTrans.forward * speed);
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.GetComponent<BasicProjectile>() == null)
        {
            if (col.gameObject.GetComponent<CharController>() != null)
            {
                //deal damage
            }
            //spawn particles

            Destroy(gameObject);
        }
    }
}
