using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Triangle{
	
	private List<Edge> edgeList = new List<Edge>();
	
	public Triangle(Edge _edg0, Edge _edg1, Edge _edg2){
		edgeList.Add(_edg0);
		edgeList.Add(_edg1);
		edgeList.Add(_edg2);
	}
	
	public List<Edge> getEdges(){
		return edgeList;	
	}

	//Find if this triangle contains a vert)
	public bool containsVertex(VertexNode _vert){
		
		foreach(Edge aEdge in edgeList){
			if (aEdge.getNode0() == _vert || aEdge.getNode1() == _vert){
				return true;
			}
		}
		return false;
	}
	
	public bool checkTriangleShareEdge(Triangle _aTri){
		
		foreach(Edge aEdge in _aTri.getEdges()){
			foreach (Edge myEdge in edgeList){
				if (myEdge.checkSame(aEdge)){
					return true;	
				}
			}
			
		}
		return false;
	}
	
	public bool checkTriangleContainsEdge(Edge _aEdge){
		foreach (Edge myEdge in edgeList){
			if (myEdge.checkSame(_aEdge)){
				return true;	
			}
		}
		
		return false;
	}
	
	public void setEdges(Edge _edge0, Edge _edge1, Edge _edge2){
		edgeList.Clear();
		edgeList.Add(_edge0);
		edgeList.Add(_edge1);
		edgeList.Add(_edge2);
	}
}
