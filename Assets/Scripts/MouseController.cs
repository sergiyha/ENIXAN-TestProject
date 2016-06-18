﻿using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
    public struct Limits
    {
        public float leftLimit, rightLimit, upLimit, downLimit;
    }


    public Collider collider;

    [HideInInspector]
    public Grid grid;

    private int lengthOfOneTile;
    public GameObject cursor;
    public GameObject tree;
    public GameObject stone;


    public Limits cameraLimits = new Limits();
    public Limits mouseScrollLimits = new Limits();
    public MouseController Instance;

    public float cameraMoveSpeed;
    public float cameraScrollSpead;
    public float maxMouseScroll = 45f;
    public float minMouseScroll = 25f;


    public float mouseBorders = 25;

    private Vector3 pos;
    bool [][] BusyPlaces;
    GameObject[][] PlacedObjects;

    // Use this for initialization
    void Awake()
    {
        grid = FindObjectOfType<Grid>();
        lengthOfOneTile = Mathf.FloorToInt(grid.gridSize.x / grid.columns);
        cameraLimits.leftLimit = -30.0f;
        cameraLimits.rightLimit = 65.0f;
        cameraLimits.upLimit = 65.0f;
        cameraLimits.downLimit = -30.0f;

        mouseScrollLimits.leftLimit = mouseBorders;
        mouseScrollLimits.rightLimit = mouseBorders;
        mouseScrollLimits.upLimit = mouseBorders;
        mouseScrollLimits.downLimit = mouseBorders;

    }
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {


        if (CheckIfUserCameraInput())
        {

            transform.Translate(GetTranslation());

        }

        //Clamp canera position
        transform.position =
            new Vector3(
            Mathf.Clamp(transform.position.x, cameraLimits.leftLimit, cameraLimits.rightLimit),
            transform.position.y,
            Mathf.Clamp(transform.position.z, cameraLimits.downLimit, cameraLimits.upLimit));


        //MouseScroll
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (DesiredScrollRotation(false) < maxMouseScroll)
            {
                transform.rotation = Quaternion.Euler(
                    DesiredScrollRotation(false),
                    transform.rotation.eulerAngles.y,
                    transform.rotation.eulerAngles.z);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (DesiredScrollRotation(true) > minMouseScroll)
            {
                transform.rotation = Quaternion.Euler(
                DesiredScrollRotation(true),
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
            }
        }











        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (collider.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            int x = Mathf.FloorToInt(hitInfo.point.x / lengthOfOneTile);
            int z = Mathf.FloorToInt(hitInfo.point.z / lengthOfOneTile);
            Debug.Log(x + " " + z);
            if (x < grid.columns && x >= 0 && z < grid.rows && z >= 0)
                cursor.transform.position = new Vector3((x * lengthOfOneTile) + (float)lengthOfOneTile / 2, 0, (z * lengthOfOneTile) + (float)lengthOfOneTile / 2);
            else if (x >= grid.columns && z < grid.rows && z >= 0)
                cursor.transform.position = new Vector3((grid.columns - 1) * lengthOfOneTile + (float)lengthOfOneTile / 2, 0, (z * lengthOfOneTile) + (float)lengthOfOneTile / 2);
            else if (x < 0 && z < grid.rows && z >= 0)
                cursor.transform.position = new Vector3((0 * lengthOfOneTile) + (float)lengthOfOneTile / 2, 0, (z * lengthOfOneTile) + (float)lengthOfOneTile / 2);
            else if (z >= grid.rows && x < grid.rows && x >= 0)
                cursor.transform.position = new Vector3((x * lengthOfOneTile) + (float)lengthOfOneTile / 2, 0, ((grid.rows - 1) * lengthOfOneTile) + (float)lengthOfOneTile / 2);
            else if (z < 0 && x < grid.rows && x >= 0)
                cursor.transform.position = new Vector3((x * lengthOfOneTile) + (float)lengthOfOneTile / 2, 0, (0 * lengthOfOneTile) + (float)lengthOfOneTile / 2);


        }
    }



    public float DesiredScrollRotation(bool isScrollDown)
    {
        float desiredPosition = 0.0f;
        if (isScrollDown) desiredPosition = transform.rotation.eulerAngles.x - cameraScrollSpead * Time.deltaTime;
        if (!isScrollDown) desiredPosition = transform.rotation.eulerAngles.x + cameraScrollSpead * Time.deltaTime;
        return desiredPosition;
    }


    public bool IsMousePositionIsWithinTheBoundaries()
    {
        if (
            (Input.mousePosition.x < mouseScrollLimits.leftLimit && Input.mousePosition.x > -5) ||
            (Input.mousePosition.x > (Screen.width - mouseScrollLimits.rightLimit) && Input.mousePosition.x < (Screen.width + 5)) ||
            (Input.mousePosition.y < mouseScrollLimits.downLimit && Input.mousePosition.y > -5) ||
            (Input.mousePosition.y > (Screen.height - mouseScrollLimits.upLimit) && Input.mousePosition.y < (Screen.height + 5))
            )
            return true;
        else return false;
    }




    public bool CheckIfUserCameraInput()
    {
        bool mouseMove;
        if (IsMousePositionIsWithinTheBoundaries())
            mouseMove = true;
        else mouseMove = false;
        return mouseMove;

    }

    public Vector3 GetTranslation()
    {
        float moveX = 0.0f;
        float moveZ = 0.0f;
        float moveY = 0.0f;

        float moveSpeed = cameraMoveSpeed * Time.deltaTime;


        if (Input.mousePosition.x < mouseScrollLimits.leftLimit)
        {
            // Debug.Log("Left");
            moveX = -moveSpeed;
        }
        else if (Input.mousePosition.x > Screen.width - mouseScrollLimits.rightLimit)
        {
            // Debug.Log("Right");
            moveX = moveSpeed;
        }
        else if (Input.mousePosition.y > Screen.height - mouseScrollLimits.upLimit)
        {
            // Debug.Log("up");

            moveZ = moveSpeed * Mathf.Cos(transform.rotation.eulerAngles.x * (Mathf.PI / 180));
            moveY = moveSpeed * Mathf.Sin(transform.rotation.eulerAngles.x * (Mathf.PI / 180));

        }
        else if (Input.mousePosition.y < mouseScrollLimits.downLimit)
        {
            // Debug.Log("down");
            moveZ = -moveSpeed * Mathf.Cos(transform.rotation.eulerAngles.x * (Mathf.PI / 180));
            moveY = -moveSpeed * Mathf.Sin(transform.rotation.eulerAngles.x * (Mathf.PI / 180));

        }
        return new Vector3(moveX, moveY, moveZ);
    }

    /* public bool IsDesiredPositionOutOfBoundsries(Vector3 desiredPosition)
     {

         bool overBoundaries = false;
         if (transform.position.x + desiredPosition.x < cameraLimits.leftLimit)
             overBoundaries = true;
         if (transform.position.x + desiredPosition.x > cameraLimits.rightLimit)
             overBoundaries = true;
         if (transform.position.z + desiredPosition.z > cameraLimits.upLimit)
             overBoundaries = true;
         if (transform.position.z + desiredPosition.z < cameraLimits.downLimit)
             overBoundaries = true;
         return overBoundaries;*/

}









