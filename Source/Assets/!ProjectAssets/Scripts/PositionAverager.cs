using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionAverager : MonoBehaviour 
{
    List<Transform> targets;
    Transform myTrans;
    float cameraDist;

    public float CameraDist
    {
        get
        {
            return cameraDist;
        }
    }


	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
        name = "Camera Target";
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 newPos = new Vector3();
        if (targets != null)
        {
            if (targets.Count > 1)
            {
                foreach (Transform t in targets)
                {
                    newPos += t.position;
                }
                newPos /= (float)targets.Count;
                myTrans.position = newPos;
            }
            else
            {
                myTrans.position = targets[0].position;
            }
        }
	}

    public void AddTarget(Transform target)
    {
        if (targets == null)
            targets = new List<Transform>();
        targets.Add(target);
    }
}
