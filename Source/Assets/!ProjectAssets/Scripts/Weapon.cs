/* Weapon.cs
*  Last Edited By:	Ryan Morris
*					11 October 2014
*
* This is an abstract class I created for testing delegates.
*/

public abstract class Weapon {
	// Variable Members
	private string model;
	private float damage, speed;
	
	// Constructor
	public Weapon( string modelFile ) {
		model = modelFile;
		damage = 1;
		speed = 1;
	}
}