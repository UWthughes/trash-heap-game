using UnityEngine;
using System.Collections;

public class R_Metal : Race
{
    public R_Metal()
    {
        tex = (Texture) Resources.Load("T_Metal");
        //Favors Strength
        Intelligence = 3;
        Dexterity = 5;
        Strength = 182;

        //Passive is to reflect a percentage of damage received at attacker.
        racialAbility = new A_R_MetalReflect();
    }
}
