using UnityEngine;
public class Edge {

	private VertexNode node0;
	private VertexNode node1;

	public Edge(VertexNode _n0, VertexNode _n1){
		node0 = _n0;
		node1 = _n1;
	}
	
	public VertexNode getNode0(){
		return node0;
	}
	
	public VertexNode getNode1(){
		return node1;	
	}
	
	public bool checkSame(Edge _aEdge){
		if 	( (node0 == _aEdge.getNode0() || node0 == _aEdge.getNode1()) &&
			  (node1 == _aEdge.getNode0() || node1 == _aEdge.getNode1())){
			return true;
		}
		
		return false;
	}
	
	public bool edgeContainsVertex(VertexNode _aNode){
		if (node0 == _aNode || node1 == _aNode){
			return true;	
		}
		
		return false;
	}
}
