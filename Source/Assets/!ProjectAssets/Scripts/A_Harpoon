
using UnityEngine; 
 using System.Collections; 

 public class A_Harpoon : IAbility 
 { 
 Color c;

 protected float action = 1f;
 protected float readyAt = 0f;
 protected string description = "Get over here!" ;
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
     get { return "A harpoon to bring your enemy over!"; }
 }
     public void OnActivate(StatBlock caster) 
     {
         for (i = 0; i < action/2; i += .001)
         {
             Transform proj = GameObject.CreatePrimitive(PrimitiveType.Cylinder).GetComponent<Transform>();
             proj.position = caster.CC.transform.position + caster.CC.transform.forward;
             proj.rotation = caster.CC.transform.rotation;
             proj.Rotate(Vector3.forward);
             proj.localScale = new Vector3(20f*i, 10f*i,2f, 2f);
             proj.gameObject.AddComponent<BasicProjectile>();
             proj.renderer.material.color = c;
         }
         for (i = 0; i >= action/2 && i< action; i += .001)
         {
             Transform proj = GameObject.CreatePrimitive(PrimitiveType.Cylinder).GetComponent<Transform>();
             proj.position = caster.CC.transform.position + caster.CC.transform.forward;
             proj.rotation = caster.CC.transform.rotation;
             proj.Rotate(Vector3.forward);
             proj.localScale = new Vector3((20f - 10f *i-.5), 2f, 2f);
             proj.gameObject.AddComponent<BasicProjectile>();
             proj.renderer.material.color = c;
         }
     } 
 
// If enemy struck, make enemy's coordinates the same as the projectiles'? 
     public void OnStruck()
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
 
      public A_Harpoon(Color color) 
     { 
         c = Color.red; 
     } 
 } 
