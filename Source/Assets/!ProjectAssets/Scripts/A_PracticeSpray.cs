//Bill Matonte
//11.22.14
//Class for spraying abilities. 
//This class is used for weapons that project scattered particles. 



using UnityEngine;
using System.Collections;

public class A_PracticeSpray : IAbility
{
    Color c;

    public void OnActivate(StatBlock caster)
    {
        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
        proj.position = caster.CC.transform.position + caster.CC.transform.forward;
        proj.rotation = caster.CC.transform.rotation;
        proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
        proj.localScale = new Vector3(.2f, .2f, .4f);
        proj.localScale = new Vector3(.2f, .2f, .2f);
        proj.localScale = new Vector3(.2f, .2f,  0f);
        proj.localScale = new Vector3(.2f, .2f, -.2f);
        proj.localScale = new Vector3(.2f, .2f, -.4f);

        proj.localScale = new Vector3(.2f, .1f, .4f);
        proj.localScale = new Vector3(.2f, .1f, .2f);
        proj.localScale = new Vector3(.2f, .1f, 0f);
        proj.localScale = new Vector3(.2f, .1f, -.2f);
        proj.localScale = new Vector3(.2f, .1f, -.4f);

        proj.localScale = new Vector3(.2f, .3f, .4f);
        proj.localScale = new Vector3(.2f, .3f, .2f);
        proj.localScale = new Vector3(.2f, .3f, 0f);
        proj.localScale = new Vector3(.2f, .3f, -.2f);
        proj.localScale = new Vector3(.2f, .3f, -.4f);


        proj.gameObject.AddComponent<BasicProjectile>();
        proj.renderer.material.color = c;
    }

    public A_PracticeSpray()
    {
        c = Color.blue;
    }

    public A_PracticeSpray(Color color)
    {
        c = color;
    }
}
