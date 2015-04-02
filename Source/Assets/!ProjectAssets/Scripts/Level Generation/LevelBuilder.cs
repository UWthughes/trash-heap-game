using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour {
	
	private bool hasStarted = false;
	private bool isFinished = false;
	private bool allComplete = false;
	private bool DTFinished = false;
	private bool PrimFinished = false;
	private int m_minTileSize = 5;
	private int m_maxTileSize = 15;
	//List of all cells created at in start
	private ArrayList cellList = new ArrayList(); 
	//List of cells that have been turned into rooms
	private List<VertexNode> roomList = new List<VertexNode>();
	//the Delaunay Triangulation controller 
	private Delaunay m_delaunayController = new Delaunay();
	private MSTController m_mstController = new MSTController();
	private WorldForge m_worldForge;


	// Use this for initialization
	void Start () {
		int m_numTiles = Random.Range( 25, 50 );
		for (int i = 0; i < m_numTiles; i++){
			GameObject aCell = (GameObject) Instantiate(Resources.Load("Cell"));
			
			int xScale = Random.Range( m_minTileSize, m_maxTileSize );
			int yScale = Random.Range( m_minTileSize, m_maxTileSize );
			
			if (xScale % 2 == 0){ xScale += 1;}
			if (yScale % 2 == 0){ yScale += 1;}
					
			int xPos = Random.Range(0,20);
			int yPos = Random.Range(0,20);

			aCell.transform.localScale = new Vector3(xScale, yScale, aCell.transform.localScale.y);
			aCell.transform.position = new Vector3(-10 + xPos, -10 + yPos, 0);
			aCell.GetComponent<Cell>().setup();
			cellList.Add(aCell);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( !allComplete ) {
			if (!hasStarted){
				if (cellsStill()){
					hasStarted = true;
				}
			}
            else
            {
			    if (!isFinished){
					setRooms();
					m_delaunayController.Initialize(roomList);
					isFinished = true;
				}
                else
                {
					if (!DTFinished){
						if (!m_delaunayController.IsTriangulationComplete()){
							m_delaunayController.Triangulate();	
						}
                        else
                        {
							DTFinished = true;
							m_mstController.Initialize(roomList, m_delaunayController.GetTriangulation());
						}
					}
                    else
                    {
						if (!PrimFinished){
							m_mstController.Update();
							PrimFinished = true;	
							m_worldForge = new WorldForge(cellList,roomList,m_mstController);
						}
                        else
                        {
							m_worldForge.Create();
							allComplete = true;
						}
					}
				} 
			}
		}
	}
	
	
	//returns if all the cells have stopped moving or not
	private bool cellsStill(){
		
		bool placed = true;
		foreach (GameObject aCell in cellList){
			if (!aCell.GetComponent<Cell>().getHasStopped()){
				placed = false;
			}
		}
		return placed;
	}
	
	//handles choosing which cells to turn to rooms
	private void setRooms(){
		foreach (GameObject aCell in cellList){
			aCell.SetActive(false);
			if (aCell.transform.localScale.x > 9 || aCell.transform.localScale.y > 9){
				aCell.SetActive(true);
				VertexNode thisNode = new VertexNode(aCell.transform.position.x, aCell.transform.position.y, aCell.gameObject);
				roomList.Add(thisNode);
			}
			Destroy(aCell.GetComponent<Cell>());
		}
	}
}
