using UnityEngine;
using System.Collections;

public class Grid{
	
	private int[,] grid;
	
	private int width;
	private int height;
	
	private int lowX;
	private int lowZ;
	
	public Grid(int _width, int _height,int _lowX, int _lowZ){
		
		width = _width;
		height = _height;
		
		grid = new int[width,height];
		
		lowX = _lowX;
		lowZ = _lowZ;
		
		for (int i = 0; i < width; i++){
			for (int j = 0; j < height; j++){
				grid[i,j] = 0;	
			}
		}
	}
	
	//set a cell in grid space
	public void setCell(int x, int y, int v){
		try{
			grid[x,y] = v;	
		}catch{
			Debug.Log("ERROR Grid:setCell");
		}
	}
	
	//set a cell in grid space when world corrdinate is given
	public void setCellWorld(int _x, int _y, int _v){
		try{
			int cX = _x - lowX;
			int cY = _y - lowZ;
			
			grid[cX,cY] = _v;	
		}catch{
			Debug.Log("ERROR Grid:setCellWorld");
		}
	}
	
	public int getCell(int x, int y){
		
		if (x >=0 && x <= width-1 && y >=0 && y <= height-1){
			return grid[x, y];
		}else{
			return -1;	
		}	
	}
	
	public int getCellWorldSpace(int x, int y){
		int cX = x - lowX;
		int cY = y - lowZ;
		
		if (cX >=0 && cX <= width-1 && cY >=0 && cY <= height-1){
			return grid[cX, cY];
		}else{
			return -1;	
		}
	}
	
	public int getWidth(){
		return width;	
	}
	
	public int getHeight(){
		return height;	
	}
	
	public int getLowX(){
		return lowX;	
	}
	
	public int getLowZ(){
		return lowZ;	
	}
	
}
