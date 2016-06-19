using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class GridButton
{
    public GameObject gridObject;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    public Image currentImage;
}


[System.Serializable]
public class Windows
{
    public GameObject shopWindow;
    public GameObject addingWindow;
    [HideInInspector]
    public GameObject currentWindow;

}


public class WindowsController : MonoBehaviour
{
    public Windows window;
    public GridButton gb;

    //For Windows
    private MouseController mouseController;


    // Use this for initialization
    void Start()
    {
        mouseController = FindObjectOfType<MouseController>();



    }

    // Update is called once per frame
    void Update()
    {

    }

    void NewWindow(GameObject desiredWindow)
    {
        if (window.currentWindow != null)
        {
            window.currentWindow.SetActive(false);
        }
        window.currentWindow = desiredWindow;
        window.currentWindow.SetActive(true);
    }



    public void OnClickShopButton()
    {
        NewWindow(window.shopWindow);
    }




    public void OnClickAddButton()
    {
        NewWindow(window.addingWindow);
    }

    public void OnClickStoneButton()
    {
        SetObjectAsCurrent(mouseController.stone);
    }

    public void OnClickTreeButton()
    {
        SetObjectAsCurrent(mouseController.tree);
    }
    public void OnClickRemoverButton()
    {
        if (mouseController.currentObject != null)
        {
            mouseController.currentObject.SetActive(false);
        }
        mouseController.currentObject = mouseController.remover;
        mouseController.currentObject.SetActive(true);
        window.currentWindow.SetActive(false);
        mouseController.canRemove = true;
    }




    public void OnClickCloseButton()
    {
        window.currentWindow.SetActive(false);
    }

    public void DisableGrid()
    {
        if (gb.gridObject.activeInHierarchy)
        {
            gb.currentImage.sprite = gb.disabledSprite;
            gb.gridObject.SetActive(false);
        }
        else if (!gb.gridObject.activeInHierarchy)
        {
            gb.currentImage.sprite = gb.enabledSprite;
            gb.gridObject.SetActive(true);
        }
    }



    public void  SetObjectAsCurrent(GameObject desiredObject)
    {
        if (mouseController.currentObject != null)
        {
            mouseController.currentObject.SetActive(false);
        }
        mouseController.currentObject = desiredObject;
        mouseController.currentObject.SetActive(true);
        window.currentWindow.SetActive(false);
        mouseController.canRemove = true;
    } 
}
