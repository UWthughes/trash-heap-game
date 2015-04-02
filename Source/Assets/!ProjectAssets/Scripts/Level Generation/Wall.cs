using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	
	private GameObject checkCollider;
	
	public int hash;
	
	//Delunay setup
	public void autoTile(){
		checkCollider = GameObject.FindGameObjectWithTag("CheckCollider");
		checkCollider.GetComponent<CheckCollider>().setup(this.gameObject);
		
		hash = checkCollider.GetComponent<CheckCollider>().getHash();
		
		GameObject tile = checkCollider.GetComponent<CheckCollider>().createTile(hash);

		if (tile != null){
					
			tile.transform.position = new Vector3(transform.position.x, (transform.position.y - renderer.bounds.size.y/2) + tile.renderer.bounds.size.y/2, transform.position.z);
			Instantiate(tile);
			Destroy(this.gameObject);
			
		}
	}
}
