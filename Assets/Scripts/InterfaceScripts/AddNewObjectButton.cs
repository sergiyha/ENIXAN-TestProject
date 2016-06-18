using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddNewObjectButton : MonoBehaviour {

    public MouseController mouseController;
    public GameObject addingMenu;
	void Start ()
    {
        mouseController = FindObjectOfType<MouseController>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickAddButton()
    {
        addingMenu.SetActive(true);
    }


    public void OnClickCloseButton()
    {
        addingMenu.SetActive(false);
    }


    public void OnClickStoneButton()
    {
        mouseController.cursor = mouseController.stone;
        addingMenu.SetActive(false); 
    }


    public void OnClicTreeButton()
    {
        mouseController.cursor = mouseController.tree;
        addingMenu.SetActive(false);
    }
}
