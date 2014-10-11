/*  InputHandler.cs
*   Created By: Tyler Hughes
* 				Date N/A
*   Last Edited By:	Ryan Morris
*					11 October 2014
*
*   This class handles the controller information. 
* 
*	11 October 2014 Edit:
* 		I added the delegation feature for firing the gun. The A,B,X,Y buttons will change
*   	the firing mode. This can be seen mostly in Update().
*/

using UnityEngine;
using System.Collections;
using GamepadInput;

public class InputHandler : MonoBehaviour 
{
	Vector2 v_move;
	Vector2 v_face;
	
	float moveSpeed = 10f;
	
	// Player transform
	Transform myTrans;
	
	// TEST RangedWeapon Class / Delegation
	RangedWeapon wep;
	public delegate void shootRanged( Transform shooter );
	shootRanged shot;
	
	// Use this for initialization
	void Start () 
	{
		myTrans = GetComponent<Transform>();
		wep = new RangedWeapon ("TEST", "TEST", 10);
		shot = wep.sphere_shot;
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
		
		if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One)) {
			GetComponent<Renderer>().material.color = Color.green;
			shot = wep.sphere_shot;
		}
		if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One)) {
			GetComponent<Renderer>().material.color = Color.red;
			shot = wep.cube_shot;
		}
		if (GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One)) {
			GetComponent<Renderer>().material.color = Color.blue;
			shot = wep.plane_shot;
		}
		if (GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.One)) {
			GetComponent<Renderer>().material.color = Color.yellow;
			shot = wep.sphere_shot;
		}
		
		if (GamePad.GetTrigger(GamePad.Trigger.RightTrigger, GamePad.Index.One) > 0f) 
		{
			shot( myTrans );
		}
	}
	
	void OnGUI()
	{
		
	}
}
