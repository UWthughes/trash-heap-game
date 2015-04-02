using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
	
	private List<GameObject> roomsToConnectTo = new List<GameObject>();
	private List<GameObject> connectedRooms = new List<GameObject>();
	
	private float xSize;
	private float ySize;
	private float zSize;
	
	// Use this for initialization
	void Start () {
		xSize = renderer.bounds.size.x;
		ySize = renderer.bounds.size.y;
		zSize = renderer.bounds.size.z;
		addFloor();
	}
	
	
	public void addWalls(){
		
		for (int i = 0; i < xSize; i++){
			GameObject aWall =(GameObject) GameObject.Instantiate(Resources.Load("Wall"));
			
			float wX = aWall.renderer.bounds.size.x;
			float wY = aWall.renderer.bounds.size.y;
			float wZ = aWall.renderer.bounds.size.z;
			
			aWall.transform.position = new Vector3((int) (transform.position.x -(xSize/2) + (wX/2) + i), transform.position.y + (wY/2) +(ySize/2), transform.position.z + (zSize/2) - (wZ/2));
			aWall.transform.parent = transform;
			
			GameObject aWall1 =(GameObject) GameObject.Instantiate(Resources.Load("Wall"));
			aWall1.transform.position = new Vector3((int) (transform.position.x -(xSize/2) + (wX/2) + i), transform.position.y + (wY/2) +(ySize/2), transform.position.z - (zSize/2) + (wZ/2));
			aWall1.transform.parent = transform;
		}
		
		for (int i = 1; i < zSize-2; i++){
			GameObject aWall =(GameObject) GameObject.Instantiate(Resources.Load("Wall"));
			
			float wX = aWall.renderer.bounds.size.x;
			float wY = aWall.renderer.bounds.size.y;
			float wZ = aWall.renderer.bounds.size.z;
			
			aWall.transform.position = new Vector3((int) (transform.position.x -(xSize/2) + (wX/2)), transform.position.y + (wY/2) +(ySize/2), transform.position.z + (zSize/2) - (wZ/2) - i);
			aWall.transform.parent = transform;
			
			GameObject aWall1 =(GameObject) GameObject.Instantiate(Resources.Load("Wall"));
			aWall1.transform.position = new Vector3((int) (transform.position.x +(xSize/2) - (wX/2)), transform.position.y + (wY/2) +(ySize/2), transform.position.z + (zSize/2) - (wZ/2) - i);
			aWall1.transform.parent = transform;
		}
	}
	
	private void addFloor(){
		Destroy(GetComponent<MeshRenderer>());
		for (int i = 1; i < xSize-1; i++){
			for (int j = 1; j < zSize-2; j++){
				GameObject aFloor =(GameObject) GameObject.Instantiate(Resources.Load("TileSets/Ground"));
				float wX = aFloor.renderer.bounds.size.x;
				float wY = aFloor.renderer.bounds.size.y;
				float wZ = aFloor.renderer.bounds.size.z;
				
				aFloor.transform.position = new Vector3((transform.position.x -(xSize/2) + (wX/2) + i), transform.position.y + (ySize/2), transform.position.z + (zSize/2) - (wZ/2) - j);
				aFloor.transform.parent = transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void setRoomsToConnectTo(List<GameObject> _rooms){
		roomsToConnectTo = _rooms;
	}
	
	public void addConnection(GameObject _connectedRoom){
		if (roomsToConnectTo.Contains(_connectedRoom)){
			return;	
		}
		
		roomsToConnectTo.Add(_connectedRoom);
	}
	
	public List<GameObject> getConnectionList(){
		return roomsToConnectTo;	
	}
	
	public GameObject needToConnect(){
		if (roomsToConnectTo.Count > 0){
			return roomsToConnectTo[0];
		}
		return null;
	}
	
	public void removeRoomToConnectTo(GameObject _room){
		roomsToConnectTo.Remove(_room);
	}
}
