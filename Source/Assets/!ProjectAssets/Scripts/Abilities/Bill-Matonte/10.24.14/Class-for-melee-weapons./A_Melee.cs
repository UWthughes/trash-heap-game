//Bill Matonte
//10.24.14
//Class for melee weapons. 
//This class inherits IAbility class for hand-to-hand combat 


using UnityEngine;
using System.Collections;

public class A_Melee : IAbility
{
Color c;

    public void OnActivate(StatBlock caster)
    {
        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Cylinder).GetComponent<Transform>();
        proj.position = caster.CC.transform.position + caster.CC.transform.forward;
        proj.rotation = caster.CC.transform.rotation;
        proj.Rotate(Vector3.forward);
        proj.localScale = new Vector3(.2f, .2f, .2f);
        proj.gameObject.AddComponent<BasicProjectile>();
        proj.renderer.material.color = c;
    }


    public A_Melee(Color color)
    {
        c = Color.gray;
    }
}
