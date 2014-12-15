/* 
 * LevelTile.cs
*  Last Edited By:	Ryan Morris
*					15 December 2014
*
* The base component for building levels
*/

using UnityEngine;
using System.Collections;

public class LevelTile {
/************************
 *   Member Variables
 ************************/
	Vector3 m_start;
	Vector3 m_end;
	int m_sizeX;
	int m_sizeZ;
	int m_constraint = 5;

/************************
 *   Constructor
 ************************/
	public LevelTile( int startX, int startZ, int sizeX, int sizeZ ) {
		m_start = new Vector3( startX, 0, startZ );
		m_sizeX = sizeX;
		m_sizeZ = sizeZ;
		m_end = new Vector3( startX + sizeX, 0, startZ + sizeZ);
	}

	// sets the size of the tile in constructor, also makes sure that the x and z values are odd
	public LevelTile( int startX, int startZ ) {
		int randomX = Random.Range(1, m_constraint);
		// make sure randomX is odd value
		if( randomX % 2 == 0 ) {
			// randomly choose to round up or down
			int tempRandom = Random.Range(0, 100);
			if( tempRandom >= 50 ) {
				randomX += 1;
			} else {
				randomX -= 1;
			}
		}
		int randomZ;
		// if randomX was a 1, create a tile of size 1x1
		if( randomX == 1 ) {
			randomZ = 1;
		} else {
			randomZ = Random.Range(3, m_constraint);
		}
		// make sure randomZ is odd value
		if( randomZ % 2 == 0 ) {
			// randomly choose to round up or down
			int tempRandom = Random.Range(0, 100);
			if( tempRandom >= 50 ) {
				randomZ += 1;
			} else {
				randomZ -= 1;
			}
		}

		m_start = new Vector3( startX, 0, startZ );
		m_sizeX = randomX;
		m_sizeZ = randomZ;
		m_end = new Vector3( startX + m_sizeX, 0, startZ + m_sizeZ);
		//m_end = new Vector2( startX + m_sizeX, startY + m_sizeZ );
	}

/***********************
 *      Methods
 ***********************/
	public int get_size_x() {
		return m_sizeX;
	}

	public int get_size_z() {
		return m_sizeZ;
	}

	public Vector2 get_start() {
		return m_start;
	}

	public int get_startX() {
		return (int)m_start.x;
	}

	public int get_startZ() {
		return (int)m_start.z;
	}

	public Vector2 get_end() {
		return m_end;
	}
	
	public int get_endX() {
		return (int)m_end.x;
	}
	
	public int get_endZ() {
		return (int)m_end.z;
	}

	public void set_tile( int x, int z ) {
		m_start.x = x;
		m_start.y = 0;
		m_start.z = z;
		m_end.x = m_start.x + m_sizeX;
		m_end.z = m_start.z + m_sizeZ;
	}

	public int get_constraint() {
		return m_constraint;
	}

	public Vector2 get_middle() {
		Vector2 returnVec = new Vector2( this.get_startX() + m_sizeX / 2, this.get_startZ() + m_sizeZ / 2 );
		return returnVec;
	}
}
