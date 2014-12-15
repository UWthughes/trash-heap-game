/*
*  LevelBuilder.cs
*  Last Edited By:	Ryan Morris
*					15 December 2014
*
* Uses the LevelGenerator class to generate a level, then builds the 3D components
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
//[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshCollider))]

public class LevelBuilder : MonoBehaviour {

	public int sizeX = 10;
	public int sizeZ = 10;
	public float tileSize = 1.0f;
	int m_tileResolution = 8;

	LevelGenerator_v2 m_levelGenerator;
	List<LevelTile> m_tileList;

	public GameObject testTile;
	
	// Use this for initialization
	void Start() {
		m_levelGenerator = new LevelGenerator_v2( 50 );
		m_levelGenerator.generate_level();
		m_tileList = m_levelGenerator.get_tile_list();
		//build_mesh();
		foreach( LevelTile tile in m_tileList ) {
			for( int k = 0; k < tile.get_size_z(); k++ ) {
				for( int i = 0; i < tile.get_size_x(); i++ ) {
					Instantiate(testTile, new Vector3(tile.get_startX() + i, 0, tile.get_startZ() + k), Quaternion.identity);
				}
			}
			/*Vector2 midPoint = tile.get_middle();
			float x = midPoint.x;
			float z = midPoint.y;
			Debug.Log("MidPoint: " + x + " " + z );
			Instantiate(testTile, new Vector3(x, 0, z), Quaternion.identity);*/
		}
	}

	/*void build_texture() {
		foreach( LevelTile tile in m_tileList ) {
			int textureWidth = tile.get_size_x();
			int textureHeight = tile.get_size_z();
			Texture2D texture = new Texture2D( textureWidth, textureHeight );

			for( int z = 0; z < textureHeight; z++ ) {
				for( int x = 0; x < textureWidth; x++ ) {
					Color c = new Color( Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f) );
					texture.SetPixel( x, z, c );
				}
			}
			texture.filterMode = FilterMode.Point;
			texture.Apply();
			MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
			meshRenderer.sharedMaterials[0].mainTexture = texture;
		}
	}*/

	/*
	void build_texture() {
		foreach( LevelTile tile in m_tileList ) {
			int textureWidth = tile.get_size_x();
			int textureHeight = tile.get_size_z();
			//Texture3D texture = new Texture3D( textureWidth, textureHeight, 1, TextureFormat.ARGB32, true );
			Texture3D texture = new Texture3D( 16, 16, 16, TextureFormat.ARGB32, true );
			//var cols = new Color[textureWidth * textureHeight];
			var cols = new Color[16 * 16 * 16];
			int idx = 0;
			Color c = Color.white;

			for (int z = 0; z < textureHeight; ++z)
			{
				for (int x = 0; x < textureWidth; ++x, ++idx)
				{
					c.r = Random.Range(0f, 1f);
					c.g = Random.Range(0f, 1f);
					c.b = Random.Range(0f, 1f);
					cols[idx] = c;
				}
			}
			texture.SetPixels( cols );
			texture.filterMode = FilterMode.Point;
			texture.Apply();
			MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
			//meshRenderer.sharedMaterials[0].mainTexture = texture;
			meshRenderer.material.SetTexture( "tex", texture );
		}
	}
	
	public void build_mesh() {
		int numTiles = sizeX * sizeZ;
		int numTriangles = numTiles * 2;
		int verticeSizeX = sizeX + 1;
		int verticeSizeZ = sizeZ + 1;
		int numVertices = verticeSizeX * verticeSizeZ;
		
		// Generate mesh data
		Vector3[] vertices = new Vector3[numVertices];
		Vector3[] normals = new Vector3[numVertices];
		Vector2[] uv = new Vector2[numVertices];
		int[] triangles = new int[numTriangles * 3];
		
		int i, k;
		for( k = 0; k < verticeSizeZ; k++ ) {
			for( i = 0; i < verticeSizeX; i++ ) {
				// populate vertices
				vertices[k * verticeSizeX + i] = new Vector3( i*tileSize, 0, k*tileSize );
				// populate normals
				normals[k * verticeSizeX + i] = Vector3.up;
				uv[k * verticeSizeX + i] = new Vector2( (float)i / sizeX, (float)k / sizeZ );
			}
		}
		
		for( k = 0; k < sizeZ; k++ ) {
			for( i = 0; i < sizeX; i++ ) {
				int squareIndex = k * sizeX + i;
				int triOffset = squareIndex * 6;
				
				// Triangle tile 1
				triangles[triOffset + 0] = k * verticeSizeX + i + 0;
				triangles[triOffset + 1] = k * verticeSizeX + i + verticeSizeX + 0;
				triangles[triOffset + 2] = k * verticeSizeX + i + verticeSizeX + 1;
				// Triangle tile 2
				triangles[triOffset + 3] = k * verticeSizeX + i + 0;
				triangles[triOffset + 4] = k * verticeSizeX + i + verticeSizeX + 1;
				triangles[triOffset + 5] = k * verticeSizeX + i + 1;
			}
		}
		
		// Create new mesh and populate with data
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		// Assign mesh to filter/renderer/collider
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();
		
		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
		build_texture();
	}*/
}
