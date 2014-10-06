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

        myTrans.Translate(moveDirection * moveSpeed * Time.deltaTime * (GamePad.GetTrigger(GamePad.Trigger.LeftTrigger,GamePad.Index.One) + 1f), Space.World);

        if (v_face.sqrMagnitude > .1)
            myTrans.eulerAngles = new Vector3(0, Mathf.Atan2(v_face.x, v_face.y) * 57.2957795f, 0);

        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
            GetComponent<Renderer>().material.color = Color.green;
        if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One))
            GetComponent<Renderer>().material.color = Color.red;
        if (GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One))
            GetComponent<Renderer>().material.color = Color.blue;
        if (GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.One))
            GetComponent<Renderer>().material.color = Color.yellow;

        if (GamePad.GetTrigger(GamePad.Trigger.RightTrigger, GamePad.Index.One) > 0f) 
        {
            Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
            proj.position = myTrans.position + myTrans.forward;
            proj.rotation = myTrans.rotation;
            proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);

            proj.localScale = new Vector3(.2f, .2f, .2f);
            proj.gameObject.AddComponent<BasicProjectile>();
        }
    }

    void OnGUI()
    {

    }
}
