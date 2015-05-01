using UnityEngine;
using System.Collections;

public class StatBlock
{
    private float _moveSpeed = 20f;
    private int _strength = 10;
    private int _dexterity = 10;
    private int _intelligence = 10;
    private int _maxHP;
    private int _currHP;
    private CharController _cc;

    public delegate void HealthChangeHandler(int curr, int max);
    public event HealthChangeHandler OnHealthChange;


    #region Properties
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
    public float fStrength
    {
        get
        {
            return ((float)_strength) / 100f;
        }
        set
        {
        }
    }

    public float fDexterity
    {
        get
        {
            return ((float)_dexterity) / 100f;
        }
        set
        {
        }
    }
    public float fIntelligence
    {
        get
        {
            return ((float)_intelligence) / 100f;
        }
        set
        {
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

    public int MaxHP
    {
        get
        {
            return _maxHP;
        }
        set
        { }
    }
    public int CurrHP
    {
        get
        {
            return _currHP;
        }
        set
        { }
    }
    #endregion


    public StatBlock()
    {
        Setup(10,10,10,20f);
    }
    public StatBlock(int s, int d, int i, float ms)
    {
        Setup(s, d, i, ms);
    }

    public void Setup(int s, int d, int i, float ms)
    {
        _strength = s;
        _dexterity = d;
        _intelligence = i;
        _moveSpeed = ms;
        _maxHP = 200;
        _currHP = _maxHP;
    }

    public void TakeDamage(int dmg)
    {
        int startHP = _currHP;

        _currHP -= dmg;
        if (_currHP > _maxHP)
            _currHP = _maxHP;
        if (_currHP < 0)
            _currHP = 0;
        //notify event / delegate of damage being taken.
        if (OnHealthChange != null)
            OnHealthChange(_currHP, _maxHP);

        //death event
        if (_currHP <= 0)
        {
            Die();
        }

        //send up a damage number graphic
        //if (startHP != _currHP)
            _cc.SpawnDamageNumber(startHP - _currHP);
    }

    public void Die()
    {
        Debug.Log("Dying.");
        //This might be tricksy, with our current setup.  
        //Disable (but don't destroy) the player.
            //Make invisible
            //Disable controls
            //Disable collisions
            //Keep transform on top of camera target so that camera doesn't go wonky.
        //Need to check for Game Over
        //If not game over, prep for respawn
    }
}
