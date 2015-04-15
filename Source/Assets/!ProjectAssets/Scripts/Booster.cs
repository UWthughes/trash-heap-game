using UnityEngine;
using System.Collections;
public class Booster:Ability //: MonoBehaviour
{
    public int count = 3;
    protected float cooldown = 0.3f;
    protected float readyAt = 0f;
    protected string description = "Booster Rocket!";
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
        set { Description = "The rocket triples your speed."; }
        get { return description; }
    }
    public void OnActivate()
    {
        owner._moveSpeed *= 3.0; 
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
