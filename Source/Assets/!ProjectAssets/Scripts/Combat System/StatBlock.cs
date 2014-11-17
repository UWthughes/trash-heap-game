using UnityEngine;
using System.Collections;

public class StatBlock
{
    private float _moveSpeed = 20f;
    private int _strength = 10;
    private int _dexterity = 10;
    private int _intelligence = 10;
    private CharacterController _cc;


    #region Parameters
    public int Dexterity
    {
        get
        {
            return _dexterity;
        }
        set
        {
            if (value > 0)
                _dexterity = value;
        }
    }
    public int Intelligence
    {
        get
        {
            return _intelligence;
        }
        set
        {
            if (value > 0)
                _intelligence = value;
        }
    }
    public int Strength
    {
        get
        {
            return _strength;
        }
        set
        {
            if (value > 0)
                _strength = value;
        }
    }
    
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
