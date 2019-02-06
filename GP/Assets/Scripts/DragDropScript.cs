using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropScript : MonoBehaviour
{

    //Initialize Variables
    GameObject getTarget;
    public Image Icon;
    //Dragging settings
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 startPosition;
    Vector3 positionOfScreen;
    //Flag to check if this place is suitable place to tool
    bool isSuitablePlaceToDrag = false;
    //Forbiden area boundaries
    float forbidenAreaYAxis = 0;
    float forbidenAreaXAxis;
    float defaultValueForForbidenAreaXAxis;
    //Mouse boundaries
    float mouseX;
    float mouseY;
    //Screen boundaries
    float screenX;
    float screenY;
    //Flags to check collisions
    bool grassCollisionFlag = false;
    bool riverCollisionFlag = false;
    bool riverSideCollisionFlag = false;

    // Use this for initialization
    void Start()
    {
        startPosition = gameObject.transform.position;
        defaultValueForForbidenAreaXAxis = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        forbidenAreaXAxis = defaultValueForForbidenAreaXAxis;
    }

    void Update()
	{
		if (!GameController.sharedInstance.gameIsStopped) {
			//Mouse Button Press Down
			if (Input.GetMouseButtonDown (0)) {            
				RaycastHit hitInfo;
				getTarget = ReturnClickedObject (out hitInfo);
				if (getTarget != null) {
                    //if this object is toolObject
                    StopToolAnimation();//change toolRotationStatus
					isMouseDragging = true;
					//Converting world position to screen position.
					positionOfScreen = Camera.main.WorldToScreenPoint (getTarget.transform.position);
					offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
				}
			}

			//Mouse Button Up
			if (Input.GetMouseButtonUp (0)) {
				isMouseDragging = false;
				checkIfSutiableAreaForRotation ();
			}
        
			//Is mouse Moving
			if (isMouseDragging) {                        
				//tracking mouse position.
				Vector3 currentScreenSpace = getCorrectMousePosition ();
				//converting screen position to world position with offset changes.
				Vector3 currentPosition = Camera.main.ScreenToWorldPoint (currentScreenSpace) + offsetValue;
				//It will update target gameobject's current postion.
                if (currentPosition.y < forbidenAreaYAxis)
				{
                    currentPosition.y = forbidenAreaYAxis;
				}
                if (riverSideCollisionFlag && currentPosition.x > forbidenAreaXAxis)
                {
                    //Debug.Log("TesingReiverSide" + currentPosition.x + "::" + forbidenAreaXAxis);
                    currentPosition.x = forbidenAreaXAxis;                    
                }
				getTarget.transform.position = currentPosition;
			}
        
		}
	}


    //limit mouse position 
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
        if (mouseY < Screen.height * Constants.riverLevelRatio)
            mouseY = Screen.height * Constants.riverLevelRatio;
        else if (Input.mousePosition.y > screenY)
            mouseY = screenY;
        else
            mouseY = Input.mousePosition.y;

       return new Vector3(mouseX, mouseY, positionOfScreen.z);
    }

    //stop Tool Animation
    void StopToolAnimation()
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

            if (!riverCollisionFlag && grassCollisionFlag)
            {
                if (gameObject.tag == "Fan")
                {
                    runWinSetting();
                    Constants.FanRotationStatus = true;
                }
                else if (gameObject.tag == "SunCell")
                {
                    runWinSetting();
                }
            }
            else if (gameObject.tag == "RiverWheel" && riverCollisionFlag && grassCollisionFlag)
            {
                runWinSetting();
                Constants.RiverWheelRotationStatus = true;
            }
        }
        else
        {
            returnToolToToolBox();
        }

    }

    //stop smoke and run win animation
    void runWinSetting()
    {
        SmokeController.sharedInstance.StopSmoke();
        GameController.sharedInstance.winGame();
    }

    //kick the tool back to ToolBox
    void returnToolToToolBox()
    {
        this.gameObject.SetActive(false);
        this.Icon.enabled = true;
        CoinsController.sharedInstance.ChangeCountText(true, Constants.Tool_Price);
        gameObject.transform.position = startPosition;
		AudioManager.sharedInstance.PlaySound (Enums.Sound.WrongTool);
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

    //check if this tool is suitable with current env.
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

    //this method will call if there a collision with this tool
    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Grass")
        {           
            forbidenAreaYAxis = gameObject.transform.position.y - 5;
            grassCollisionFlag = true;
            //Debug.Log("Enter Grass");
        }
        else if (col.gameObject.tag == "River")
        {            
            riverCollisionFlag = true;
            //Debug.Log("Enter River");
        }
        else if (col.gameObject.tag == "RiverSide")
        {            
            forbidenAreaXAxis = gameObject.transform.position.x;
            riverSideCollisionFlag = true;
            //Debug.Log("RiverSide" );
        }
    }

    //this method will call if this tool has left a collision area
    private void OnTriggerExit(Collider col)
    {
        
        if (col.gameObject.tag == "Grass")
        {
            grassCollisionFlag = false;
            forbidenAreaYAxis = 0;
            //Debug.Log("Exit Grass");
        }
        else if (col.gameObject.tag == "River")
        {
            riverCollisionFlag = false;            
            forbidenAreaYAxis = 0;
            //Debug.Log("Exit River");
        }
        else if (col.gameObject.tag == "RiverSide")
        {
            forbidenAreaXAxis = defaultValueForForbidenAreaXAxis;
            riverSideCollisionFlag = false;  
            //Debug.Log("ExitRiverSide");
        }
        //forbidenAreaYAxis = 0;
    }    

}