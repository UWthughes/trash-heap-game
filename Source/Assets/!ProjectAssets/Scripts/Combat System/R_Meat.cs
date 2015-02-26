using UnityEngine;
using System.Collections;

public class R_Meat : Race
{
    public R_Meat()
    {
        tex = (Texture)Resources.Load("T_Meat");
        //Favors Dexterity

        //Passive is constant, gradual health regen.
    }
}
