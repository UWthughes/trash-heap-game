using UnityEngine;
using System.Collections;

public class A_PracticeBurst : Ability
{
    Color c;


    public override void OnActivate()
    {
        if (Time.time > readyAt)
        {
            for (int i = 0; i < 16; i++)
            {
                Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
                proj.position = owner.CC.transform.position + owner.CC.transform.forward;
                proj.rotation = owner.CC.transform.rotation;
                proj.RotateAround(owner.CC.transform.position, new Vector3(0, 1, 0), (float)i * 22.5f);
                proj.localScale = new Vector3(.4f, .4f, .4f);
                proj.gameObject.AddComponent<BasicProjectile>();
                proj.renderer.material.color = c;
            }
            readyAt = Time.time + cooldown;
        }
    }

    public A_PracticeBurst()
    {
        c = Color.black;
    }

    public A_PracticeBurst(Color color)
    {
        c = color;
    }
}
