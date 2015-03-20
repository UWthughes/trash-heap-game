using UnityEngine;
using System.Collections;
public class Ability //: MonoBehaviour
{
    protected float cooldown = 1f;
    protected float readyAt = 0f;
    protected string description = "An Ability";
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
        set { }
        get { return description; }
    }
    public virtual void OnActivate()
    {
    }
    public virtual void OnStruck()
    {
    }
    public virtual void OnTick()
    {
    }
    public virtual void OnEquip(ref StatBlock newOwner)
    {
        //register with the owner to set the OnTick to listen to an event.
        newOwner.CC.OnTick += OnTick;
        newOwner.CC.OnStruck += OnStruck;
        owner = newOwner;
    }
    public virtual void OnDequip(ref StatBlock owner)
    {
        //undo anything done in OnEquip
        owner.CC.OnTick -= OnTick;
        owner.CC.OnStruck -= OnStruck;
    }
}Enter file contents hereusing UnityEngine;
using System.Collections;
public class Ability //: MonoBehaviour
{
    protected float cooldown = 1f;
    protected float readyAt = 0f;
    protected string description = "An Ability";
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
        set { }
        get { return description; }
    }
    public virtual void OnActivate()
    {
    }
    public virtual void OnStruck()
    {
    }
    public virtual void OnTick()
    {
    }
    public virtual void OnEquip(ref StatBlock newOwner)
    {
        //register with the owner to set the OnTick to listen to an event.
        newOwner.CC.OnTick += OnTick;
        newOwner.CC.OnStruck += OnStruck;
        owner = newOwner;
    }
    public virtual void OnDequip(ref StatBlock owner)
    {
        //undo anything done in OnEquip
        owner.CC.OnTick -= OnTick;
        owner.CC.OnStruck -= OnStruck;
    }
}
