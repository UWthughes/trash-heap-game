using UnityEngine;
using System.Collections;

public class R_Electronic : Race
{
    public R_Electronic()
    {
        tex = (Texture)Resources.Load("T_Electronic");
        //Favors Intelligence
        Intelligence = 5;
        Dexterity = 15;
        Strength = 125;

        //Passive is periodic AOE shock.
        racialAbility = new A_R_ElectronicSpark();
    }
}
