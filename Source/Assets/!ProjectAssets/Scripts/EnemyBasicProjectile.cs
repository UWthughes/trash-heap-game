using UnityEngine;
using System.Collections;

public class EnemyBasicProjectile : MonoBehaviour {

    public float speed = 25f;

    Transform myTrans;
    // Use this for initialization
    void Start()
    {
        myTrans = GetComponent<Transform>();
        gameObject.AddComponent<Rigidbody>();
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        myTrans.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<EnemyObject>() == null && col.gameObject.GetComponent<SphereCollider>() == null)
        {
            //deal damage
            Destroy(gameObject);
        }
        //show particles

        
    }
}
