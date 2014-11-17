using UnityEngine;
using System.Collections;

public class A_PracticeBurst : IAbility
{
    Color c;

    string _description;

    public string GetDescription()
    {
        return _description;
    }

    public void OnActivate(StatBlock caster)
    {
        for (int i = 0; i < 16; i++)
        {
            Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
            proj.position = caster.CC.transform.position + caster.CC.transform.forward;
            proj.rotation = caster.CC.transform.rotation;
            proj.RotateAround(caster.CC.transform.position, new Vector3(0,1,0), (float)i * 22.5f);
            proj.localScale = new Vector3(.2f, .2f, .2f);
            proj.gameObject.AddComponent<BasicProjectile>();
            proj.renderer.material.color = c;
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
