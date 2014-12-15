/*
 * LevelGenerator_v2.cs
 * Last Edited By:	Ryan Morris
 * 					15 December 2014
 * 
 * Creates the layout of a level as tiles (can be thought of as rooms)
 * using a multi-step algorithm.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Triangulator.Geometry;

public class LevelGenerator_v2 {
/************************
 *   Member Variables
 ************************/
	private int m_numTiles;		   				// total number of tiles to generate in the level
	private int m_gridSizeX, m_gridSizeZ;		// number of columns and rows in the grid
	private int m_minTileX, m_minTileZ;			// minimum tile x and z sizes
	private int m_maxTileX, m_maxTileZ;			// maximum tile x and z sizes
	private List<LevelTile> m_tileList;
	private List<Point> m_centerPoints;
	private List<Triangle> m_triangles;
	private List<Edge> m_mst;
	private float m_constraintRadius;
	private float m_repelDecay;
	private float m_densityFactor;
	private bool m_separationComplete;
	
	private int interInf;
	private int iejInf;
	private int iterInf;
	
/************************
 *   Constructor / Initialization
 ************************/
	public LevelGenerator_v2( int tileNum ) {
		m_numTiles = tileNum;
		m_gridSizeX = m_numTiles * 2;
		m_gridSizeZ = m_numTiles * 2;
		m_minTileX = 1;
		m_minTileZ = 1;
		m_maxTileX = 7;
		m_maxTileZ = 7;
		m_tileList = new List<LevelTile>();
		m_centerPoints = new List<Point>();
		m_mst = new List<Edge>();
		m_constraintRadius = 5.0f;
		m_repelDecay = 1.0f;
		m_densityFactor = 10.0f;
		m_separationComplete = false;

		interInf = 0;
		iejInf = 0;
		iterInf = 0;
	}

/***********************
 *      Methods
 ***********************/
	// populates the tile list
	public void generate_level() {
		// generate the beginning tiles for the level
		for( int i = 0; i < m_numTiles; i++ ) {
			m_tileList.Add( generate_tile() );
		}

		// separation step
		while ( !m_separationComplete ) {
			separate_tiles();
		}
		Debug.Log( "|Loop Values| Iterations:" + iterInf + " same tile: " + iejInf + " not intersection: " + interInf );
		/*foreach( LevelTile tile in m_tileList ) {
			Debug.Log( "New tile loc: " + tile.get_startX() + " " + tile.get_startZ() +
			          " " + tile.get_size_x() + " " + tile.get_size_z() );
		}*/

		foreach( LevelTile tile in m_tileList ) {
			m_centerPoints.Add( new Point( tile.get_startX(), tile.get_startZ() ));
		}
		// calculate a Delaunay Triangulation
		if( m_centerPoints.Count > 2 ) {
			Debug.Log( "DELAUNAY" );
			m_triangles = Triangulator.Delauney.Triangulate( m_centerPoints );
			Debug.Log( "DELAUNAY COMPLETE" );
		}

	}

	// generates a LevelTile
	private LevelTile generate_tile() {
		int xLoc = (int)Random.Range( -m_constraintRadius, m_constraintRadius );
		int zLoc = (int)Random.Range( -m_constraintRadius, m_constraintRadius );

		// check if position is already taken
		bool posNeedsChecked = true;
		while( posNeedsChecked ) {
			if( check_pos( xLoc, zLoc ) ) {
				// in list, generate new values
				xLoc = (int)Random.Range( -m_constraintRadius, m_constraintRadius );
				zLoc = (int)Random.Range( -m_constraintRadius, m_constraintRadius );
			} else {
				posNeedsChecked = false;
			}
		}

		Vector3 position = new Vector3( xLoc, 0, zLoc );
		Vector3 size = new Vector3( Random.Range( m_minTileX, m_maxTileX + 1 ), 0, Random.Range( m_minTileZ, m_maxTileZ + 1 ) );

		// prevent long skinny tiles
		if( size.x * 2 <= size.z ) {
			size.x = size.z;
		}
		if( size.z * 2 <= size.x ) {
			size.z = size.x;
		}
		/*Debug.Log( "Original tile loc: " + (int)position.x + " " + (int)position.z +
		          " " + (int)size.x + " " + (int)size.z ); */
		return new LevelTile( (int)position.x, (int)position.z, (int)size.x, (int)size.z );
	}

	// push overlapping tiles apart
	private void separate_tiles() {
		iterInf++;
		m_separationComplete = true;

		if( interInf > 100000 || iterInf > 100000 || iejInf > 100000 ) {
			Debug.Log( "INFINITE LOOP " + iterInf + " " + iejInf + " " + interInf );
			return;
		}

		for( int i = 0; i < m_tileList.Count; i++ ) {
			Vector2 velocity = new Vector2();
			for( int j = 0; j < m_tileList.Count; j++ ) {
				//Debug.Log( "i / j: " + i + " " + j );
				//Debug.Log( "velocity: " + velocity );
				// skip same tiles
				if( i == j ) {
					iejInf++;
					continue;
				}

				/*
				Rect intersection = calculate_intersection( m_tileList[i], m_tileList[j] );

				if( intersection.width == 0 || intersection.height == 0 ) {
					interInf++;
					continue;
				} */

				if( !is_intersection( m_tileList[i], m_tileList[j] )) {
					//Debug.Log( "PASSING INTERSECTION" );
					interInf++;
					continue;
				}

				//Vector2 diff = new Vector2( m_tileList[j].get_startX() - m_tileList[i].get_endX(),
				//                            m_tileList[j].get_startZ() - m_tileList[i].get_endZ() );

				Vector2 diff = new Vector2( m_tileList[i].get_startX() - m_tileList[j].get_startX(),
				                            m_tileList[i].get_startZ() - m_tileList[j].get_startZ() );

				if( diff.magnitude > 0 ) {
					//Debug.Log( "magnitude: " + diff.magnitude );
					//diff.Normalize();
					//Debug.Log( "diff: " + diff );
					//diff.Scale( new Vector2( m_repelDecay / diff.sqrMagnitude, m_repelDecay / diff.sqrMagnitude ));
					//Debug.Log( "Scale: " + diff );
					velocity += diff;
				}
				
				if ( velocity.magnitude > 0 ) {
					// need to keep separating until velocity vector is zero
					m_separationComplete = false;
					//velocity.Normalize();
					//Debug.Log( "velocity normed: " + velocity );
					float dx = velocity.x;
					float dz = velocity.y;
					//Debug.Log("dx / dz: " + dx + " " + dz);
					//float dx = Mathf.Abs( velocity.x ) < 0.5f ? 0f : velocity.x > 0f ? m_densityFactor : -m_densityFactor;
					//float dz = Mathf.Abs( velocity.y ) < 0.5f ? 0f : velocity.y > 0f ? m_densityFactor : -m_densityFactor;
					//Debug.Log( "dx / dz: " + dx + " " + dz );
					int newX = m_tileList[i].get_startX() + (int)dx;
					int newZ = m_tileList[i].get_startZ() + (int)dz;
					m_tileList[i].set_tile( newX, newZ );
					//Debug.Log( "changed loc: " + m_tileList[i].get_startX() + " " + m_tileList[i].get_startZ() );
				}
			}
		}
	}

	private bool check_pos( int px, int pz ) {
		foreach( LevelTile tile in m_tileList ) {
			if( px == tile.get_startX()  && pz == tile.get_startZ() ) {
				return true;
			}
		}
		return false;
	}

	private bool is_intersection( LevelTile a, LevelTile b ) {
		return (a.get_startX() < b.get_endX() && a.get_endX() > b.get_startX() &&
		        a.get_startZ() < b.get_endZ() && a.get_endZ() > b.get_startZ() );
	}

	private Rect calculate_intersection( LevelTile a, LevelTile b ) {
		float dx = Mathf.Abs(( a.get_startX() - b.get_startX() ));
		float dz = Mathf.Abs(( a.get_startZ() - b.get_startZ() ));
		
		float xIntersection = dx - ( a.get_size_x() / 2 ) - ( b.get_size_x() / 2 );
		float zIntersection = dz - ( a.get_size_z() / 2 ) - ( b.get_size_z() / 2 );
		
		if( xIntersection > 0 ) {
			xIntersection = 0;
		}
		if( zIntersection > 0 ) {
			zIntersection = 0;
		}
		
		return new Rect( 0, 0, xIntersection, zIntersection );
		
	}

	public List<LevelTile> get_tile_list() {
		return m_tileList;
	}

}
