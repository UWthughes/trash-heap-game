using UnityEngine;
using System.Collections;

public class A_PracticeProjectile : IAbility
{
    Color c;

    public void OnActivate(StatBlock caster)
    {
        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
        proj.position = caster.CC.transform.position + caster.CC.transform.forward;
        proj.rotation = caster.CC.transform.rotation;
        proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
        proj.localScale = new Vector3(.2f, .2f, .2f);
        proj.gameObject.AddComponent<BasicProjectile>();
        proj.renderer.material.color = c;
    }

    public A_PracticeProjectile()
    {
        c = Color.black;
    }

    public A_PracticeProjectile(Color color)
    {
        c = color;
    }
}
