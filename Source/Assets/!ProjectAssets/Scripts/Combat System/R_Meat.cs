using UnityEngine;
using System.Collections;

public class R_Meat : Race
{
    public R_Meat()
    {
        tex = (Texture)Resources.Load("T_Meat");
        //Favors Dexterity
        Intelligence = 4;
        Dexterity = 30;
        Strength = 105;

        //Passive is constant, gradual health regen.
        racialAbility = new A_R_MeatHeal();
    }
}
