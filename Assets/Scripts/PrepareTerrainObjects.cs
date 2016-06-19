using UnityEngine;
using System.Collections;

public class PrepareTerrainObjects : MonoBehaviour {
    public GameObject terrainObjects;

	// Use this for initialization
	void Awake () {
        Instantiate(terrainObjects, new Vector3(0f,0f,0f), Quaternion.identity); 
	
	}
	
	
}
