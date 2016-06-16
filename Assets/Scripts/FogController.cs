using UnityEngine;
using System.Collections;

public class FogController : MonoBehaviour {


    public Transform player;
    private float maxDist,currentDist;
    private float distInPersentage;
    private MouseController mouseController;
    private float fogIntencity;
    private float maxIncrementFogIntencity;
    private float minFogIntencity;



	// Use this for initialization
	void Start () {
        mouseController = FindObjectOfType<MouseController>();
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
     
        minFogIntencity = 130.0f;
        maxIncrementFogIntencity = 70;
        maxDist = Vector3.Distance(new Vector3 (mouseController.cameraLimits.leftLimit,player.transform.position.y, mouseController.cameraLimits.downLimit),this.transform.position);
    }

    // Update is called once per frame
    void Update () {
        currentDist =  Vector3.Distance(player.transform.position, this.transform.position);
        distInPersentage = (currentDist / maxDist) * 100.0f;
        fogIntencity = (maxIncrementFogIntencity * distInPersentage) / 100.0f;
        RenderSettings.fogEndDistance = minFogIntencity + fogIntencity;
    }
}
