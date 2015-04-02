using UnityEngine;
using System.Collections;

public class CorridorBuilder  {
	private Grid theLevel;
	
	private Vector2 pos = new Vector2();
	
	public CorridorBuilder(Grid _level){
		theLevel = _level;
	}
	
	private float floorY;
	
	public void connect(GameObject _room0, GameObject _room1){
		pos = new Vector2(_room0.transform.position.x, _room0.transform.position.z);
		bool xDir = false;
		
		if (_room0.transform.position.x <= _room1.transform.position.x){
			xDir = true;	
		}else{
			xDir = false;	
		}
		
		if (xDir){
			while (pos.x < _room1.transform.position.x){
				setXFloors();
				
				pos = new Vector2(pos.x +1, pos.y);
				
			}
		}else{
			while (pos.x > _room1.transform.position.x){
				setXFloors();
				
				pos = new Vector2(pos.x -1, pos.y);
				
			}
		}
		
		bool zDir = false;
		if (_room0.transform.position.z <= _room1.transform.position.z){
			zDir = true;	
		}else{
			zDir = false;	
		}
		
		if (zDir){
			while (pos.y < _room1.transform.position.z){
				
				setZFloors();
				
				pos = new Vector2(pos.x, pos.y +1);
				
			}
		}else{
			while (pos.y > _room1.transform.position.z){
				
				setZFloors();
				
				pos = new Vector2(pos.x, pos.y -1);
				
			}
		}
	}
	
	private void createFloor(){
		
	}
	
	private void createFloor(int _x, int _y){
		GameObject aFloor =(GameObject) GameObject.Instantiate(Resources.Load("TileSets/Ground"));
		aFloor.name = "CorridorFloor";
		float wX = aFloor.renderer.bounds.size.x;
		float wY = aFloor.renderer.bounds.size.y;
		float wZ = aFloor.renderer.bounds.size.z;
		
		aFloor.transform.position = new Vector3(_x, floorY, _y );
	}
	
	private void setXFloors(){
		
		int rX =(int) Mathf.Round(pos.x);
		int rY =(int) Mathf.Round(pos.y);
		
		if (theLevel.getCellWorldSpace(rX, rY) != 1){
				
			theLevel.setCellWorld(rX,rY,1);
			createFloor(rX, rY);
		}
		
		if (theLevel.getCellWorldSpace(rX, rY+1) != 1){
				
			theLevel.setCellWorld(rX,rY+1,1);
			createFloor(rX, rY+1);
		}
		
		if (theLevel.getCellWorldSpace(rX, rY-1) != 1){
				
			theLevel.setCellWorld(rX,rY-1,1);
			createFloor(rX, rY-1);
		}
	}
	
	private void setZFloors(){
		int rX =(int) Mathf.Round(pos.x);
		int rY =(int) Mathf.Round(pos.y);
		
		if (theLevel.getCellWorldSpace(rX, rY) != 1){
				
			theLevel.setCellWorld(rX, rY,1);
			createFloor( rX, rY);
		}
		
		if (theLevel.getCellWorldSpace(rX-1, rY) != 1){
				
			theLevel.setCellWorld(rX-1, rY,1);
			createFloor(rX-1, rY);
		}
		
		if (theLevel.getCellWorldSpace( rX+1,  rY) != 1){
				
			theLevel.setCellWorld(rX+1,rY,1);
			createFloor(rX+1, rY);
		}
		
	}
	
	public void setFloorY(float _y){
		floorY = _y;
	}
}
