using UnityEngine;
using System.Collections;

public class Race : StatBlock
{
    //might want to add caps / diminishing returns and check for them when leveling up.
    private int strPerLevel = 3;
    private int dexPerLevel = 3;
    private int intPerLevel = 3;
    private float movePerLevel = 1f;

    public Texture tex;
    public Ability racialAbility;
    public void LevelUp ()
    {
        Strength = Strength + strPerLevel;
        Dexterity = Dexterity + dexPerLevel;
        Intelligence = Intelligence + intPerLevel;
        moveSpeed = moveSpeed + movePerLevel;
    }

    public void RegisterRacialAbility()
    {
        if (racialAbility != null)
            racialAbility.OnEquip(this);
    }

    public Race()
    {
    }

}
