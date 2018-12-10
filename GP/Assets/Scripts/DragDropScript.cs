using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropScript : MonoBehaviour
{

    //Initialize Variables
    GameObject getTarget;
    public Image Icon;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    bool isSuitablePlaceToDrag = false;
    bool allowToolToRotate = false;
    float level = 0;
    //GameObject River;
    float riverLevelRatio = 37/313f;
    float grassLevelRatio = 50 / 313f;
    //
    float mouseX;
    float mouseY;
    float screenX;
    float screenY;
    //
    bool grassCollisionFlag = false;
    bool riverCollisionFlag = false;
    //
    Vector3 startPosition;
    // Use this for initialization
    void Start()
    {
        startPosition = gameObject.transform.position;
       /* River = GameObject.FindWithTag("River");
        if (River != null)
        {
            Debug.Log(River.transform.position.x + ":" + River.transform.position.y + ":" + River.transform.position.z);            
        }
        Debug.Log(Screen.width+"W");
        Debug.Log(Screen.height+":H");*/
    }

    void Update()
    {

        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {            
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget != null)
            {//if this object is toolObject
                StopToolRotation();//change toolRotationStatus
                isMouseDragging = true;
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
                offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
            }
        }

        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
            checkIfSutiableAreaForRotation();
            Debug.Log(gameObject.transform.position.y + ":YPosition");
        }
        
        //Is mouse Moving
        if (isMouseDragging)
        {                        
            //tracking mouse position.
            Vector3 currentScreenSpace = getCorrectMousePosition();
            // Debug.Log(mouseX+":x");
            // Debug.Log(mouseY + "Y");
            //converting screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue;

            //It will update target gameobject's current postion.
            //Debug.Log(currentPosition.y+":YPosition");
            if (currentPosition.y < level )//&& gameObject.tag != "RiverWheel")
            {
                currentPosition.y = level;
            }
            getTarget.transform.position = currentPosition;
        }
        
    }



    Vector3 getCorrectMousePosition()
    {
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        screenX = Screen.width - 20;
        screenY = Screen.height - 20;

        if (mouseX < 20)
            mouseX = 20;
        else if (Input.mousePosition.x > screenX)
            mouseX = screenX;
        else
            mouseX = Input.mousePosition.x;
        if (mouseY < Screen.height * riverLevelRatio)
            mouseY = Screen.height * riverLevelRatio;
        else if (Input.mousePosition.y > screenY)
            mouseY = screenY;
        else
            mouseY = Input.mousePosition.y;

       return new Vector3(mouseX, mouseY, positionOfScreen.z);
    }

    void StopToolRotation()
    {
        if (gameObject.tag == "Fan")
        {
            Constants.FanRotationStatus = false;
        }
        else if (gameObject.tag == "RiverWheel")
        {
            Constants.RiverWheelRotationStatus = false;
        }
    }
    
    void checkIfSutiableAreaForRotation()
    {
        if(checkEnvironment())
        {
            if (gameObject.tag == "Fan" && !riverCollisionFlag && grassCollisionFlag)
            {
                Constants.FanRotationStatus = true;
            }
            else if (gameObject.tag == "RiverWheel" && riverCollisionFlag && grassCollisionFlag)
            {
                Constants.RiverWheelRotationStatus = true;
            }
        }
        else
        {            
            this.gameObject.SetActive(false);
            this.Icon.enabled = true;
            CoinsController.sharedInstance.showCoins(gameObject.transform.position);
            CoinsController.sharedInstance.ChangeCountText(true, Constants.Tool_Price);
            gameObject.transform.position = startPosition;

        }
        

    }


    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform)
            {

                if (hit.transform.gameObject.transform.position == gameObject.transform.position )//&& checkEnvironment(hit.collider.tag))
                   target = hit.collider.gameObject;
            }
        }
        return target;
    }

    bool checkEnvironment()
    {
        string tag = gameObject.tag;
        switch (GameManager.sharedInstance.currentMood)
        {

            case Enums.EnvironmentMood.Windy:
                if (tag == "Fan")
                    return true;
                else
                    return false;
                break;

            case Enums.EnvironmentMood.Sunny:
                if (tag == "SunCell")
                    return true;
                else
                    return false;
                break;

            case Enums.EnvironmentMood.Rainy:
                if (tag == "RiverWheel")
                    return true;
                else
                    return false;
                break;

            default: return false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Enterede");
        //

        //Debug.Log(Input.mousePosition.y);
        if (col.gameObject.tag == "Grass")
        {
            //if (gameObject.tag == "Fan")
            {
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                //Debug.Log(Input.mousePosition.y);
                Debug.Log(col.transform.position.y);
                Debug.Log(gameObject.transform.position.y);
                level = gameObject.transform.position.y-3;//Camera.main.WorldToScreenPoint(new Vector3(0, col.transform.position.y,0)).y;
                Debug.Log(level);
                grassCollisionFlag = true;
                Debug.Log("Enter Grass");
            }
        }
        else if (col.gameObject.tag == "River")
        {
            //if (gameObject.tag == "RiverWheel")
            {
                //Debug.Log(Screen.width + "W");
                //Debug.Log(Screen.height + ":H");
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                riverCollisionFlag = true;
                Debug.Log("Enter River");
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        
        //

        //Debug.Log(Input.mousePosition.y);
        if (col.gameObject.tag == "Grass")
        {
            //if (gameObject.tag == "Fan")
            {
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                //Debug.Log(Input.mousePosition.y);
                //Debug.Log(col.transform.position.y);
                grassCollisionFlag = false;
                Debug.Log("Exit Grass");
            }
        }
        else if (col.gameObject.tag == "River")
        {
            //if (gameObject.tag == "RiverWheel")
            {
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                riverCollisionFlag = false;
                Debug.Log("Exit River");
            }
        }
        level = 0;
    }
    

}