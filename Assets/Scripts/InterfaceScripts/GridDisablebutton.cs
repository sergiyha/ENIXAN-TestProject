using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridDisablebutton : MonoBehaviour {

    public GameObject grigObject;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    private Image currentImage;


    void Start()
    {
        currentImage = GetComponent<Image>();
    }
    public void DisableGrid()
    {
        if (grigObject.activeInHierarchy)
        {
            currentImage.sprite = disabledSprite;
            grigObject.SetActive(false);
        }
        else if (!grigObject.activeInHierarchy)
        {
            currentImage.sprite = enabledSprite;
            grigObject.SetActive(true);
        }
    } 
}
