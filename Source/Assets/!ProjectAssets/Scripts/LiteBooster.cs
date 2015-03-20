using UnityEngine;
using System.Collections;
public class LiteBooster:Ability //: MonoBehaviour
{
    public int count = 5;
    protected float cooldown = 20f;
    protected float readyAt = 0f;
    protected string description = "Slow Burn Booster Rocket.";
    protected StatBlock owner;
    public float TotalCooldown
    {
        set { TotalCooldown-= Time.time; }
        get { return cooldown; }
    }
    public float RemainingCooldown
    {
        set { cooldown; }
        get{
            if (readyAt > Time.time)
                return readyAt - Time.time;
            else
                return 0f;
        }
    }
    public string Description
    {
        set { Description = "The rocket half agains your speed but for a longer time."; }
        get { return description; }
    }
    public void OnActivate()
    {
        owner._moveSpeed *=1.5; 
    }

    public void OnTick(){
        count--;
        if (count == 0) OnDequip(owner);
    }
    public void OnEquip(ref StatBlock newOwner){
        //register with the owner to set the OnTick to listen to an event.
        newOwner.CC.OnTick += OnTick;
        owner = newOwner;
    }
    public void OnDequip(ref StatBlock owner){
        //undo anything done in OnEquip
        owner.CC.OnTick -= OnTick;
    }
}
