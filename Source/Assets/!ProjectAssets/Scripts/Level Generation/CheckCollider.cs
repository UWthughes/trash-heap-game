using UnityEngine;
using System.Collections;

public class CheckCollider : MonoBehaviour {
	
	private int hash = 0;
	private GameObject parent;
	private Hashtable hashMapping = new Hashtable();
	private Grid worldGrid;
	
	void Start(){
		
		hashMapping.Add(0,"RoofBSP");
		
		hashMapping.Add(64,"R_Center");
		hashMapping.Add(192,"R_Center");
		hashMapping.Add(96,"R_Center");
		hashMapping.Add(224,"R_Center");
		
		hashMapping.Add(2,"L_Center");
		hashMapping.Add(3,"L_Center");
		hashMapping.Add(6,"L_Center");
		hashMapping.Add(7,"L_Center");
		
		hashMapping.Add(16,"T_Center");
		hashMapping.Add(144,"T_Center");
		hashMapping.Add(20,"T_Center");
		hashMapping.Add(148,"T_Center");
		
		hashMapping.Add(8,"B_Center");
		hashMapping.Add(9,"B_Center");
		hashMapping.Add(40,"B_Center");
		hashMapping.Add(41,"B_Center");
		
		hashMapping.Add(1,"T_R_Inner");
		hashMapping.Add(4,"B_R_Inner");
		hashMapping.Add(32,"T_L_Inner");
		hashMapping.Add(128,"B_L_Inner");
		
		hashMapping.Add(22,"T_L_Corner");
		hashMapping.Add(23,"T_L_Corner");
		hashMapping.Add(131,"T_L_Corner");
		hashMapping.Add(147,"T_L_Corner");
		hashMapping.Add(150,"T_L_Corner");
		hashMapping.Add(151,"T_L_Corner");
		
		hashMapping.Add(11,"B_L_Corner");
		hashMapping.Add(15,"B_L_Corner");
		hashMapping.Add(43,"B_L_Corner");
		hashMapping.Add(47,"B_L_Corner");
		hashMapping.Add(143,"B_L_Corner");
		hashMapping.Add(46,"B_L_Corner");
		
		hashMapping.Add(116,"T_R_Corner");
		hashMapping.Add(208,"T_R_Corner");
		hashMapping.Add(212,"T_R_Corner");
		hashMapping.Add(240,"T_R_Corner");
		hashMapping.Add(244,"T_R_Corner");
		
		hashMapping.Add(104,"B_R_Corner");
		hashMapping.Add(105,"B_R_Corner");
		hashMapping.Add(193,"B_R_Corner");
		hashMapping.Add(201,"B_R_Corner");
		hashMapping.Add(232,"B_R_Corner");
		hashMapping.Add(233,"B_R_Corner");
		
		hashMapping.Add(189,"=Shape");
		hashMapping.Add(29,"=Shape");
		hashMapping.Add(184,"=Shape");
		hashMapping.Add(61,"=Shape");
		hashMapping.Add(157,"=Shape");
		hashMapping.Add(185,"=Shape");
		hashMapping.Add(188,"=Shape");
		hashMapping.Add(153,"=Shape");
		hashMapping.Add(60,"=Shape");
		hashMapping.Add(24,"=Shape");
		
		
		hashMapping.Add(191,"CShape");
		hashMapping.Add(63,"CShape");
		hashMapping.Add(159,"CShape");
		hashMapping.Add(31,"CShape");
		hashMapping.Add(30,"CShape");
		
		hashMapping.Add(253,"DShape");
		hashMapping.Add(252,"DShape");
		hashMapping.Add(249,"DShape");
		hashMapping.Add(248,"DShape");
		hashMapping.Add(120,"DShape");
		
		hashMapping.Add(231,"11Shape");
		hashMapping.Add(198,"11Shape");
		hashMapping.Add(99,"11Shape");
		hashMapping.Add(230,"11Shape");
		hashMapping.Add(199,"11Shape");
		hashMapping.Add(227,"11Shape");
		hashMapping.Add(103,"11Shape");
		hashMapping.Add(195,"11Shape");
		hashMapping.Add(102,"11Shape");
		hashMapping.Add(66,"11Shape");
		hashMapping.Add(70,"11Shape");
		
		hashMapping.Add(238,"uShape");
		hashMapping.Add(207,"uShape");
		hashMapping.Add(239,"uShape");
		hashMapping.Add(235,"uShape");
		hashMapping.Add(111,"uShape");
		hashMapping.Add(107,"uShape");
		hashMapping.Add(106,"uShape");
		
		hashMapping.Add(243,"nShape");
		hashMapping.Add(247,"nShape");
		hashMapping.Add(246,"nShape");
		hashMapping.Add(215,"nShape");
		hashMapping.Add(214,"nShape");
		hashMapping.Add(119,"nShape");
		hashMapping.Add(86,"nShape");

		hashMapping.Add(255,"oShape");
		
		hashMapping.Add(55,"Section1");
		hashMapping.Add(183,"Section1");
		hashMapping.Add(54,"Section1");
		hashMapping.Add(182,"Section1");
		
		hashMapping.Add(241,"Section2");
		hashMapping.Add(245,"Section2");
		hashMapping.Add(213,"Section2");
		hashMapping.Add(209,"Section2");
		
		hashMapping.Add(175,"Section3");
		hashMapping.Add(139,"Section3");
		
		hashMapping.Add(236,"Section4");
		hashMapping.Add(237,"Section4");
		hashMapping.Add(108,"Section4");
		
		//ONE OF SECTION 5 HASHES IS WRONG
		hashMapping.Add(35,"Section5");
		hashMapping.Add(38,"Section5");
		hashMapping.Add(39,"Section5");
		hashMapping.Add(135,"Section5");
		
		hashMapping.Add(225,"Section6");
		hashMapping.Add(97,"Section6");
		
		hashMapping.Add(134,"Section7");
		
		hashMapping.Add(100,"Section8");
		hashMapping.Add(196,"Section8");
		hashMapping.Add(228,"Section8");
		
		hashMapping.Add(180,"Section9");
		hashMapping.Add(52,"Section9");
		
		hashMapping.Add(21,"Section10");
		hashMapping.Add(149,"Section10");
		hashMapping.Add(145,"Section10");
		
		hashMapping.Add(137,"Section11");
		hashMapping.Add(169,"Section11");
		hashMapping.Add(168,"Section11");
		
		hashMapping.Add(13,"Section12");
		hashMapping.Add(44,"Section12");
		hashMapping.Add(45,"Section12");
		
		hashMapping.Add(165,"Section17");
		
		hashMapping.Add(33,"Section19");
		hashMapping.Add(160,"Section19");
		
		hashMapping.Add(5,"Section20");
		
		hashMapping.Add(132,"Section21");
		
		hashMapping.Add(36,"Section22");
		
		hashMapping.Add(129,"Section23");
		
		hashMapping.Add(164,"Section26");
		
		hashMapping.Add(133,"Section27");
		
	}
	
	public void setup(GameObject _parent){
		//reset the hash
		hash = 0;
		
		float xSize = _parent.renderer.bounds.size.x;
		float ySize = _parent.renderer.bounds.size.y;
		float zSize = _parent.renderer.bounds.size.z;
		
		//check for collisions with all other cells and calculate hash value of tile based of neighbours
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x-1,(int) _parent.transform.position.z +1) == 1){
			hash += 1;
		}
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x,(int) _parent.transform.position.z +1) == 1){
			hash += 2;
		}
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x+1,(int) _parent.transform.position.z +1) == 1){
			hash += 4;
		}
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x + 1,(int) _parent.transform.position.z) == 1){
			hash += 16;
		}
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x+1,(int) _parent.transform.position.z -1) == 1){
			hash += 128;
		}
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x,(int) _parent.transform.position.z -1) == 1){
			hash += 64;
		}
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x-1,(int) _parent.transform.position.z -1) == 1){
			hash += 32;
		}
		
		if (worldGrid.getCellWorldSpace((int) _parent.transform.position.x - 1,(int) _parent.transform.position.z) == 1){
			hash += 8;
		}
	}
	
	public int getHash(){
		return hash;	
	}
	
	public GameObject createTile(int hash){
		if (hashMapping.Contains(hash)){
			GameObject temp = (GameObject) Resources.Load("TileSets/"+hashMapping[hash]);
			return temp;
			
		}else{
			GameObject temp = (GameObject) Resources.Load("TileSets/oShape");
			return temp;
		}
		return null;
	}
	
	public void setWorldGrid(Grid _worldGrid){
		worldGrid = _worldGrid;
	}
}
