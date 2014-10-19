using UnityEngine;
using System.Collections;

public class StatBlock
{
    private float _moveSpeed = 20f;
    private CharacterController _cc;


    #region Parameters
    public float moveSpeed
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            if (value > 0f)
                _moveSpeed = value;
        }
    }

    public CharacterController CC
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
    #endregion


    public StatBlock()
    {

    }
}
