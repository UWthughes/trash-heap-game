using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Delaunay {
	/************************
 *   Member Variables
 ************************/
	private bool m_isComplete;
	int m_infProtect;
	private List<Triangle> m_triangleList;
	// Vertices that still need to be added to the triangulation
	private List<VertexNode> m_toAddList;
	// the current vertex that is being added to the triangulation
	private VertexNode m_nextNode;
	// Edges that have become broken
	private List<Edge> m_dirtyEdges;
	//the omega triangle created at start of triangulation
	private Triangle m_omegaTriangle;
	//the triangle the "m_nextNode" is inside of
	private Triangle inTriangle;
	private List<Edge> m_finalTriangulation;
	
	/************************
 *   Constructor
 ************************/
	public Delaunay() {
		m_isComplete = false;
		m_triangleList = new List<Triangle>();
		m_toAddList = new List<VertexNode>();
		m_nextNode = null;
		m_dirtyEdges = new List<Edge>();
		m_infProtect = 0;
		m_finalTriangulation = new List<Edge>();
	}
	
	/***********************
 *      Methods
 ***********************/
	// Update is called once per frame
	public void Triangulate() {
		while( m_toAddList.Count > 0 ) {
			addVertexToTriangulation();
		}
		trigDone();
	}
	
	public void Initialize( List<VertexNode> roomList ) {
		// puts all vertices into the toDo list
		foreach( VertexNode node in roomList ) {
			m_toAddList.Add( node );	
		}
		// create three artificial vertices for the omega triangle
		VertexNode node0 = new VertexNode( 0, 500, null);
		
		VertexNode node1 = new VertexNode( -500,-500, null );
		
		VertexNode node2 = new VertexNode( 500, -500, null );
		
		// create the omega triangle
		m_omegaTriangle = new Triangle( new Edge( node0, node1 ), new Edge( node0, node2 ), new Edge( node1, node2 ));
		
		// add the omega triangle to the triangle list
		m_triangleList.Add(m_omegaTriangle);
	}
	
	private void addVertexToTriangulation() {
		//Find a Random vertex from the todo list
		int choice = Random.Range( 0, m_toAddList.Count );
		
		//set next node to selected vertex
		m_nextNode = m_toAddList[choice];
		
		//remove selected vertex from todo list
		m_toAddList.Remove( m_nextNode );
		
		//stores triangles created during the loop to be appended to main list after loop
		List<Triangle> tempTriList = new List<Triangle>();
		
		//All edges are clean at this point. Remove any that may be left over from previous loop
		m_dirtyEdges.Clear();
		
		float count = -1;
		foreach(Triangle aTri in m_triangleList){
			List<Edge> triEdges = aTri.getEdges();
			count++;
			//Find which triangle the current vertex being add is located within
			if (LineIntersector.PointInTraingle(m_nextNode.getVertexPosition(), triEdges[0].getNode0().getVertexPosition(),
			                                    triEdges[0].getNode1().getVertexPosition(), triEdges[1].getNode1().getVertexPosition())){

				//cache the triangle we are in so we can delete it after loop
				inTriangle = aTri;
				
				//create three new triangles from each edge of the triangle vertex is in to the new vertex
				foreach(Edge aEdge in aTri.getEdges()){
					Triangle nTri1 = new Triangle(new Edge(m_nextNode,aEdge.getNode0()),
					                              new Edge(m_nextNode,aEdge.getNode1()),
					                              new Edge(aEdge.getNode1(),aEdge.getNode0()));
					
					//cache created triangles so we can add to list after loop
					tempTriList.Add(nTri1);	
					
					//mark the edges of the old triangle as dirty
					m_dirtyEdges.Add(new Edge(aEdge.getNode0(),aEdge.getNode1()));
					
				}
				break;
			}
		}
		
		//add the three new triangles to the triangle list
		foreach(Triangle aTri in tempTriList){
			m_triangleList.Add(aTri);	
		}
		
		//delete the old triangle that the vertex was inside of
		if (inTriangle != null){
			m_triangleList.Remove(inTriangle);
			inTriangle = null;
		}
		
		//recursively check the dirty edges to make sure they are still delaunay
		checkEdges(m_dirtyEdges);
	}
	
	private void checkEdges(List<Edge> _list){
		
		//stores if a flip occured for mode control
		bool didFlip = false;
		
		//the current dirty edge
		if (_list.Count == 0){
			return;
		}
		
		//get the next edge in the dirty list
		Edge currentEdge = _list[0];
		
		Triangle[] connectedTris = new Triangle[2];
		int index =0;
		
		
		foreach(Triangle aTri in m_triangleList){
			if (aTri.checkTriangleContainsEdge(currentEdge)){
				connectedTris[index] = aTri;
				index++;
			}
		}
		
		
		//in first case (omega triangle) this will = 1 so dont flip
		if (index == 2){
			//stores the two vertex from both triangles that arnt on the shared edge
			VertexNode[] uniqueNodes = new VertexNode[2];
			int index1= 0;

			//loop through the connected triangles and there edges. Checking for a vertex that isnt in the edge
			for(int i =0; i < connectedTris.Length; i++){
				foreach(Edge aEdge in connectedTris[i].getEdges()){
					if (!currentEdge.edgeContainsVertex(aEdge.getNode0())){
						uniqueNodes[index1] = aEdge.getNode0();
						index1++;
						break;
					}

					if (!currentEdge.edgeContainsVertex(aEdge.getNode1())){
						uniqueNodes[index1] = aEdge.getNode1();
						index1++;
						break;
					}
				}
			}

			//find the angles of the two unique vertex
			float angle0 = calculateVertexAngle(uniqueNodes[0].getVertexPosition(), 
			                                    currentEdge.getNode0().getVertexPosition(), 
			                                    currentEdge.getNode1().getVertexPosition());
			
			float angle1 = calculateVertexAngle(uniqueNodes[1].getVertexPosition(), 
			                                    currentEdge.getNode0().getVertexPosition(), 
			                                    currentEdge.getNode1().getVertexPosition());
			
			//Check if the target Edge needs flipping
			if (angle0 + angle1 > 180){
				didFlip = true;
				
				//create the new edge after flipped
				Edge flippedEdge = new Edge(uniqueNodes[0], uniqueNodes[1]);
				
				//store the edges of both triangles in the Quad
				Edge[] firstTriEdges = new Edge[3];
				Edge[] secondTriEdges = new Edge[3];
				
				VertexNode sharedNode0;
				VertexNode sharedNode1;

				//set the shared nodes on the shared edge
				sharedNode0 = currentEdge.getNode0();
				sharedNode1 = currentEdge.getNode1();
				
				//construct a new triangle to update old triangle after flip
				firstTriEdges[0] = new Edge(uniqueNodes[0], sharedNode0);
				firstTriEdges[1] = new Edge(sharedNode0, uniqueNodes[1]);
				firstTriEdges[2] = flippedEdge;
				
				//construct a new triangle to update the other old triangle after flip
				secondTriEdges[0] = new Edge(uniqueNodes[1], sharedNode1);
				secondTriEdges[1] = new Edge(sharedNode1, uniqueNodes[0]);
				secondTriEdges[2] = flippedEdge;
				
				//update the edges of the triangles involved in the flip
				connectedTris[0].setEdges(firstTriEdges[0],firstTriEdges[1], firstTriEdges[2]);
				connectedTris[1].setEdges(secondTriEdges[0],secondTriEdges[1], secondTriEdges[2]);
				
				
				//Adds all edges to be potentially dirty. This is bad and should only add the edges that *could* be dirty
				foreach(Edge eEdge in connectedTris[0].getEdges()){
					_list.Add(eEdge);	
				}
				
				foreach(Edge eEdge in connectedTris[1].getEdges()){
					_list.Add(eEdge);	
				}
				
				//also add new edge to dirty list
				_list.Add(flippedEdge);
			}
		}
		
		//remove the current edge from the dirty list
		_list.Remove(currentEdge);
		
		// if m_infProtect goes over 100 for the current level size, something probably is wrong...
		if( m_infProtect < 100 ) {
			m_infProtect++;
			checkEdges(_list);
		} else {
			Debug.Log( "m_infProtect ERROR: " + m_infProtect );
		}
	}
	
	//calculates the angle at vertex _target in triangle (_target _shared0 _shared1) in degrees
	private float calculateVertexAngle( Vector2 target, Vector2 _shared0, Vector2 _shared1 ) {
		float length0 = Vector2.Distance( target, _shared0 );
		float length1 = Vector2.Distance( _shared0, _shared1 );
		float length2 = Vector2.Distance( _shared1, target );
		
		return  Mathf.Acos( ((length0 * length0) + (length2 * length2) - (length1 * length1)) /(2 * length0 * length2) ) * Mathf.Rad2Deg; 
	}
	
	private void trigDone(){
		m_isComplete = true;
		ConstructFinal();
	}

	public bool IsTriangulationComplete() {
		return m_isComplete;
	}

	// Construct a list of all the edges actually in the triangulation
	private void ConstructFinal() {
		foreach( Triangle triangle in m_triangleList ) {
			foreach( Edge edge in triangle.getEdges() ) {
				//stop edges connecting to the omega triangle to be added to the final list
				if (edge.getNode0().getParentCell() != null && edge.getNode1().getParentCell() != null){
					m_finalTriangulation.Add( edge );	
				}
			}
		}
	}
	
	public List<Edge> GetTriangulation() {
		return m_finalTriangulation;	
	}
}