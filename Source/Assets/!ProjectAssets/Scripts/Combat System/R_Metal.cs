using UnityEngine;
using System.Collections;

public class R_Metal : Race
{
    public R_Metal()
    {
        tex = (Texture) Resources.Load("T_Metal");
        //Favors Strength

        //Passive is to reflect a percentage of damage received at attacker.
    }
}
