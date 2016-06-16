using UnityEngine;

using System.Collections;



public class Grid : MonoBehaviour
{

    public Transform transform;

    public Material material;

    public Vector2 gridSize;

    public int rows;

    public int columns;
	
	
	
	void Start()
	{
		
		UpdateGrid();
		
	}
	
	
	
	public void UpdateGrid()
	{
		
		transform.localScale = new Vector3( gridSize.x, gridSize.y, 1.0f );
		
		material.SetTextureScale( "_MainTex", new Vector2( columns, rows ) );
		
	}
	
}
