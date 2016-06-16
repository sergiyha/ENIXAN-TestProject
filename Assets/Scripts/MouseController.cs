using UnityEngine;
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
    public Transform cursor;

    public Limits cameraLimits = new Limits();
    public Limits mouseScrollLimits = new Limits();
    public MouseController Instance;

    public float cameraMoveSpeed;
    public float mouseBorders = 25;

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



    // Update is called once per frame
    void Update()
    {

        transform.position =
            new Vector3(
            Mathf.Clamp(transform.position.x, cameraLimits.leftLimit, cameraLimits.rightLimit),
            transform.position.y,
            Mathf.Clamp(transform.position.z, cameraLimits.downLimit, cameraLimits.upLimit));


       


        if (CheckIfUserCameraInput())
        {
            
            Vector3 cameraDesireMove = GetTranslation();
            if (!IsDesiredPositionOutOfBoundsries(cameraDesireMove))
                transform.Translate(cameraDesireMove, Space.Self);
        }









        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (collider.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            int x = Mathf.FloorToInt(hitInfo.point.x / lengthOfOneTile);
            int z = Mathf.FloorToInt(hitInfo.point.z / lengthOfOneTile);
            // Debug.Log(x+" "+z);
            cursor.transform.position = new Vector3((x * lengthOfOneTile) + (float)lengthOfOneTile / 2, 0, (z * lengthOfOneTile) + (float)lengthOfOneTile / 2);
        }
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
            moveZ =- moveSpeed * Mathf.Cos(transform.rotation.eulerAngles.x * (Mathf.PI / 180));
            moveY =- moveSpeed * Mathf.Sin(transform.rotation.eulerAngles.x * (Mathf.PI / 180));

        }
        return new Vector3(moveX, moveY, moveZ);
    }

    public bool IsDesiredPositionOutOfBoundsries(Vector3 desiredPosition)
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
        return overBoundaries;

    }








}
