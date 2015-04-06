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
			tile.transform.position = new Vector3(transform.position.x, 1.164361f, transform.position.z);
			//tile.transform.position = new Vector3(transform.position.x, (transform.position.y - renderer.bounds.size.y/2) + tile.renderer.bounds.size.y/2, transform.position.z);
			//Debug.Log((transform.position.y - renderer.bounds.size.y/2) + tile.renderer.bounds.size.y/2);
			//Debug.Log( "trans.pos.y: " + transform.position.y +" rend.bo.s.y/2: " + renderer.bounds.size.y/2 + " tile.rend.b.s.y/2: " + tile.renderer.bounds.size.y/2);
			Instantiate(tile);
			Destroy(this.gameObject);
			
		}
	}
}
