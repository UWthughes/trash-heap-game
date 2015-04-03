using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackData 
{
    private int _baseDamage;
    private int _effectiveDamage;
    public StatBlock source;
    public delegate void AttackLanded();
    public event AttackLanded OnAttackLanded;
    public bool isCrit = false;
    public bool negated = false;

    public int baseDamage
    {
        set { }
        get
        {
            return _baseDamage;
        }
    }

    public int effectiveDamage
    {
        set 
        {
            _effectiveDamage = value;
        }
        get
        {
            return _effectiveDamage;
        }
    }

    public AttackData(int bd, StatBlock caster)
    {
        _baseDamage = bd;
        _effectiveDamage = bd;
        source = caster;
    }
}
