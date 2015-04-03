using UnityEngine;
using System.Collections;
using GamepadInput;

public class XBoxInputHandler : MonoBehaviour 
{
    private GamePad.Index _padNum;
    private CharController _cc;
    private Vector2 v_move;
    private Vector2 v_face;

    public GamepadInput.GamePad.Index padNum
    {
        get
        { 
            return _padNum;
        }
        set
        {
            _padNum = value;
        }
    }

    public CharController CC
    {
        get
        {
            return _cc;
        }
        set
        {
            _cc = value;
        }
    }

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        v_move = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One);
        v_face = GamePad.GetAxis(GamePad.Axis.RightStick, GamePad.Index.One);

        Vector3 moveDirection = new Vector3(v_move.x, 0, v_move.y);
        if (moveDirection.sqrMagnitude > 1f)
            moveDirection = moveDirection.normalized;

        if (v_face.sqrMagnitude > .1f)
            _cc.MoveAndFace(moveDirection, new Vector3(0, Mathf.Atan2(v_face.x, v_face.y) * 57.2957795f, 0));
        else
            _cc.MoveAndFace(moveDirection, new Vector3());
        

        if (GamePad.GetTrigger(GamePad.Trigger.RightTrigger, _padNum) > 0f)
            _cc.Cast('R');
        if (GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, _padNum) > 0f)
            _cc.Cast('L');

        if (GamePad.GetButton(GamePad.Button.A, _padNum))
            _cc.Cast('A');
        if (GamePad.GetButton(GamePad.Button.B, _padNum))
            _cc.Cast('B');
        if (GamePad.GetButton(GamePad.Button.X, _padNum))
            _cc.Cast('X');
        if (GamePad.GetButton(GamePad.Button.Y, _padNum))
            _cc.Cast('Y');

        //if (GamePad.GetAxis(GamePad.Axis.Dpad, _padNum).y > 0f)
            //_cc.LevelUp();

	}
}
