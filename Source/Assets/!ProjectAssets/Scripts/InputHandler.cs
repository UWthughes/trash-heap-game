using UnityEngine;
using System.Collections;
using GamepadInput;

public class InputHandler : MonoBehaviour 
{
    Vector2 v_move;
    Vector2 v_face;

    float moveSpeed = 10f;
    float rotSpeed = 20f;

    Transform myTrans;

	// Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
    void Update()
    {

        v_move = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One);
        v_face = GamePad.GetAxis(GamePad.Axis.RightStick, GamePad.Index.One); 

        Vector3 moveDirection = new Vector3(v_move.x, 0, v_move.y);
        if (moveDirection.sqrMagnitude > 1f)
            moveDirection = moveDirection.normalized;

        myTrans.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        if (v_face.sqrMagnitude > .1)
            myTrans.eulerAngles = new Vector3(0, Mathf.Atan2(v_face.y, v_face.x) * -57.2957795f, 0);
    }

    void OnGUI()
    {

    }
}
