using UnityEngine;
using System.Collections;

public class A_PracticePeriodic : Ability
{
    Color c;

    public override void OnTick()
    {
        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
        proj.position = owner.CC.transform.position + owner.CC.transform.up;
        proj.rotation = owner.CC.transform.rotation;
        proj.Rotate(Vector3.left, 90f);
        proj.localScale = new Vector3(.3f, .3f, .3f);
        proj.gameObject.AddComponent<BasicProjectile>();
        proj.renderer.material.color = c;
    }

    public A_PracticePeriodic()
    {
        c = Color.black;
        cooldown = .3f;
    }

    public A_PracticePeriodic(Color color)
    {
        c = color;
        cooldown = .3f;
    }
}
