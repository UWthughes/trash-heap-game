using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldForge {
	private Grid levelGrid;
	private CorridorBuilder corridorBuilder;
	private List<GameObject> roomList = new List<GameObject>();
	private float floorY;

	public WorldForge(){
		
	}
	
	public WorldForge(ArrayList cellList, List<VertexNode> _roomList, MSTController mst){
		//temp object to parent all cells in 
		GameObject groupCell = new GameObject();
		groupCell.name = "AllCells";
		
		foreach(GameObject aRoom in cellList){	
			aRoom.transform.parent = groupCell.gameObject.transform;
		}
		foreach(VertexNode aNode in _roomList){
			aNode.getParentCell().AddComponent<Room>();
			roomList.Add(aNode.getParentCell());
			
		}
		//transfer the connection data into rooms so each room knows who it is connected too
		foreach(Edge aEdge in mst.getConnections()){
			aEdge.getNode0().getParentCell().GetComponent<Room>().addConnection(aEdge.getNode1().getParentCell());
			aEdge.getNode1().getParentCell().GetComponent<Room>().addConnection(aEdge.getNode0().getParentCell());
		}

		//rotate all cells to be on correct axis, no longer working in 2D
		groupCell.transform.eulerAngles = new Vector3(90,0,0);
	}
	
	public void Create(){
		findGridSize();	
		corridorBuilder = new CorridorBuilder(levelGrid);
		connectRooms();
		addWalls();
	}
	
	private void findGridSize(){
		GameObject[] allWalls = GameObject.FindGameObjectsWithTag("Ground");
		
		int lowX = 0;
		int lowY = 0;
		int lowZ = 0;
		
		int highX = 0;
		int highY = 0;
		int highZ = 0;
		
		foreach(GameObject aWall in allWalls){
			if (aWall.transform.position.x < lowX){
				lowX = (int) Mathf.Round(aWall.transform.position.x);	
			}
			
			if (aWall.transform.position.x > highX){
				highX = (int) Mathf.Round(aWall.transform.position.x);	
			}
			
			if (aWall.transform.position.z < lowZ){
				lowZ = (int) Mathf.Round(aWall.transform.position.z);	
			}
			
			if (aWall.transform.position.z > highZ){
				highZ = (int)  Mathf.Round(aWall.transform.position.z);	
			}
			
		}
		
		int width = highX - lowX;
		int height = highZ - lowZ;
		
		//make the grid abit bigger than the floor size because walls need to get added around floors
		createGrid(width+3, height+3,lowX-1, lowZ-1);
	}
	
	private void createGrid(int _width, int _height, int lowX, int lowZ){
		levelGrid = new Grid(_width, _height, lowX, lowZ);

		GameObject[] allWalls = GameObject.FindGameObjectsWithTag("BaseWall");
		GameObject[] allGrounds = GameObject.FindGameObjectsWithTag("Ground");
		
		foreach(GameObject aWall in allWalls){
			int index = (int) ( Mathf.Round(aWall.transform.position.x - lowX));
			int index1 = (int) ( Mathf.Round(aWall.transform.position.z - lowZ));
			
			levelGrid.setCell(index, index1 , 2);
		}
		
		foreach(GameObject aGround in allGrounds){
			int index = (int) (Mathf.Round(aGround.transform.position.x - lowX));
			int index1 = (int) (Mathf.Round(aGround.transform.position.z - lowZ));
			
			levelGrid.setCell(index, index1 , 1);
		}

		GameObject theHasher = (GameObject) GameObject.FindGameObjectWithTag("CheckCollider");
		theHasher.GetComponent<CheckCollider>().setWorldGrid(levelGrid);
		
		foreach(GameObject aWall in allWalls){
			aWall.GetComponent<Wall>().autoTile();	
		}
			
	}
	
	private void connectRooms(){
		
		//find a floor to get the position the digger should place them. it cant find it out otherwise
		GameObject aFloor = GameObject.FindGameObjectWithTag("Ground");
		floorY = aFloor.transform.position.y;
		corridorBuilder.setFloorY(floorY);
		
		foreach(GameObject currentRoom in roomList){
			
			List<GameObject> connections = currentRoom.GetComponent<Room>().getConnectionList();
			
			foreach(GameObject toConnect in connections){
				toConnect.GetComponent<Room>().removeRoomToConnectTo(currentRoom);
				corridorBuilder.connect(currentRoom, toConnect);
			}
		}
		
	}
	
	private void addWalls(){
		for (int i = 0; i < levelGrid.getWidth(); i++){
			for(int j = 0; j < levelGrid.getHeight(); j++){
				
				if (levelGrid.getCell(i,j) == 0){
					
					if (levelGrid.getCell(i-1,j) == 1 ||
						levelGrid.getCell(i+1,j) == 1 ||
						levelGrid.getCell(i,j-1) == 1 ||
						levelGrid.getCell(i,j+1) == 1 ||
						levelGrid.getCell(i-1,j-1) == 1 ||
						levelGrid.getCell(i-1,j+1) == 1 ||
						levelGrid.getCell(i+1,j-1) == 1 ||
						levelGrid.getCell(i+1,j+1) == 1)
					{
						levelGrid.setCell(i,j,2);
						
						GameObject aWall =(GameObject) GameObject.Instantiate(Resources.Load("Wall"));
						
						float wX = aWall.renderer.bounds.size.x;
						float wY = aWall.renderer.bounds.size.y;
						float wZ = aWall.renderer.bounds.size.z;
						
						aWall.transform.position = new Vector3((int) ( levelGrid.getLowX() + i  ),floorY +(wY/2), levelGrid.getLowZ() + j);
						
					}else{
						GameObject aRoof =(GameObject) GameObject.Instantiate(Resources.Load("TileSets/Roof"));
						
						aRoof.transform.position = new Vector3((int) ( levelGrid.getLowX() + i  ),1.633101f, levelGrid.getLowZ() + j);
					}
				}
				
			}
		}
		
		GameObject[] allWalls = GameObject.FindGameObjectsWithTag("BaseWall");
		
		foreach(GameObject aWall in allWalls){
			aWall.GetComponent<Wall>().autoTile();
		}
	}
}
