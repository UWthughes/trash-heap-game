using UnityEngine;
using System.Collections;

public interface IAbility 
{
    void OnActivate(StatBlock caster);

    string GetDescription();
}
