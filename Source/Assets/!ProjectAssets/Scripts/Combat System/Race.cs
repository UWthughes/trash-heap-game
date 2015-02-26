using UnityEngine;
using System.Collections;

public class Race
{
    //might want to add caps / diminishing returns and check for them when leveling up.
    private int strPerLevel;
    private int dexPerLevel;
    private int intPerLevel;
    private float movePerLevel;

    public Texture tex;
    public void LevelUp (ref StatBlock sb)
    {
        sb.Strength = sb.Strength + strPerLevel;
        sb.Dexterity = sb.Dexterity + dexPerLevel;
        sb.Intelligence = sb.Intelligence + intPerLevel;
        sb.moveSpeed = sb.moveSpeed + movePerLevel;
    }

    public Race()
    {
        strPerLevel = 3;
        dexPerLevel = 3;
        intPerLevel = 3;
        movePerLevel = 1f;
    }

}
