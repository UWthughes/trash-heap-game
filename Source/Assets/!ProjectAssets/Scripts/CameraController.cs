using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Transform myTrans;

	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        myTrans.position = new Vector3(target.position.x, myTrans.position.y, target.position.z);
	}
}
