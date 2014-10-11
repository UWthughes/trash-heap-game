/* RangedWeapon.cs
*  Last Edited By:	Ryan Morris
*					11 October 2014
*
* This is a class that inherits the Weapon class. I created this for testing delegates.
*/

using UnityEngine;

public class RangedWeapon : Weapon {
	// Variable Members
	private float range;
	private string animation;
	
	// Constructor
	public RangedWeapon( string modelFile, string ani, float r )
		: base( modelFile )
	{
		this.range = r;
		this.animation = ani;
	}
	
	// shoot a sphere projectile
	public void sphere_shot( Transform shooter ) {
		Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
		proj.position = shooter.position + shooter.forward;
		proj.rotation = shooter.rotation;
		proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
		proj.localScale = new Vector3(.2f, .2f, .2f);
		proj.gameObject.AddComponent<BasicProjectile>();
	}
	
	// shoot a cube projectile
	public void cube_shot( Transform shooter ) {
		Transform proj = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<Transform>();
		proj.position = shooter.position + shooter.forward;
		proj.rotation = shooter.rotation;
		proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
		proj.localScale = new Vector3(0.3f, 0.2f, 1.5f);
		proj.gameObject.AddComponent<BasicProjectile>();
	}
	
	// shoot a plane projectile
	public void plane_shot( Transform shooter ) {
		Transform proj = GameObject.CreatePrimitive(PrimitiveType.Plane).GetComponent<Transform>();
		proj.position = shooter.position + shooter.forward;
		proj.rotation = shooter.rotation;
		proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
		proj.localScale = new Vector3(.2f, .2f, .2f);
		proj.gameObject.AddComponent<BasicProjectile>();
	}
}
