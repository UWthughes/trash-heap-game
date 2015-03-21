using UnityEngine;
using System.Collections;
public class Cure3: Ability//: MonoBehaviour
{
    protected float cooldown = 0f;
    protected float readyAt = 0f;
    protected string description = "+40 HP";
    protected StatBlock owner;
    public float TotalCooldown
    {
        set { }
        get { return cooldown; }
    }
    public float RemainingCooldown
    {
        set { }
        get
        {
            if (readyAt > Time.time)
                return readyAt - Time.time;
            else
                return 0f;
        }
    }
    public string Description
    {
        set { Description= "Cure3 increases Hit Points by 40.  "; }
        get { return description; }
    }
    public void OnActivate()
    {
        if (owner.HealthPoints + 40 > 99) owner.healthPoints = 100;
        else owner.healthPoints += 40;
        OnDequip(owner);
    }

    public void OnEquip(ref StatBlock newOwner)
    {
        //register with the owner to set the OnTick to listen to an event.
        newOwner.CC.OnTick += OnTick;
        newOwner.CC.OnStruck += OnStruck;
        owner = newOwner;
    }
    public void OnDequip(ref StatBlock owner)
    {
        //undo anything done in OnEquip
        owner.CC.OnTick -= OnTick;
        owner.CC.OnStruck -= OnStruck;
    }
}
