using UnityEngine;
using System.Collections;

public class Race
{
    //might want to add caps / diminishing returns and check for them when leveling up.
    private int strPerLevel = 3;
    private int dexPerLevel = 3;
    private int intPerLevel = 3;
    private float movePerLevel = 1f;
    public void LevelUp (ref StatBlock sb)
    {
        sb.Strength = sb.Strength + strPerLevel;
        sb.Dexterity = sb.Dexterity + dexPerLevel;
        sb.Intelligence = sb.Intelligence + intPerLevel;
        sb.moveSpeed = sb.moveSpeed + movePerLevel;
    }

    public Race()
    {

    }

}
