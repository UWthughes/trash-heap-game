using UnityEngine;
using System.Collections;
public class Oil: Ability //: MonoBehaviour
{
    Color c = Color.Black;
    protected float cooldown = 2f;
    protected float readyAt = 0f;
    protected string description = "Create an oil slick.";
    protected StatBlock owner;
    protected int count = 5;
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
        set { "Smoke screen."; }
        get { return description; }
    }
    public  void OnActivate()
    {
        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
        proj.position = caster.CC.transform.position;
        proj.localScale = new Vector3(4f, 4f, 4f);
        proj.gameObject.AddComponent<BasicProjectile>();
        proj.renderer.material.color = c;
    }
    public void OnStruck()
    {
          enemy._moveSpeed *= 5.0; 
    }


    public void OnTick()
    {
        count--;
        if (count = 0) OnDequip(owner);

    }
    public  void OnEquip(ref StatBlock newOwner)
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
