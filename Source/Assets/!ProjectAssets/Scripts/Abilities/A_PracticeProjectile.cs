using UnityEngine;
using System.Collections;

public class A_PracticeProjectile : Ability
{
    Color c;

    public override void OnActivate()
    {
        if (Time.time > readyAt)
        {
            Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
            proj.position = owner.CC.transform.position + owner.CC.transform.forward;
            proj.rotation = owner.CC.transform.rotation;
            proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
            proj.localScale = new Vector3(.3f, .3f, .3f);
            proj.gameObject.AddComponent<BasicProjectile>();
            proj.renderer.material.color = c;
            readyAt = Time.time + cooldown;
        }
    }

    public A_PracticeProjectile()
    {
        c = Color.black;
        cooldown = .3f;
    }

    public A_PracticeProjectile(Color color)
    {
        c = color;
        cooldown = .3f;
    }
}
