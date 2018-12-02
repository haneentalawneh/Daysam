using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropScript : MonoBehaviour
{

    //Initialize Variables
    GameObject getTarget;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    bool isSuitablePlaceToDrag = false;
    private float RotateSpeed = 50f;
    private Vector3 centre;
    bool flag = false;
    public GameObject centerObject;
    // Use this for initialization
    void Start()
    {
        
        
    }

    void Update()
    {

        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
            flag = false;
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget != null)
            {
                
                isMouseDragging = true;
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
                offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
            }
        }

        if (Input.GetMouseButtonUp(0) && isSuitablePlaceToDrag)
        {
            flag = true;
        }

        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }
        
        //Is mouse Moving
        if (isMouseDragging)
        {            
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            float screenX = Screen.width-20;
            float screenY = Screen.height-20;
            
            if (mouseX < 20 )
                mouseX = 20;
            else if (Input.mousePosition.x > screenX)
                mouseX = screenX;
            else
                mouseX = Input.mousePosition.x;
            if (mouseY < 30)
                mouseY = 30;
            else if (Input.mousePosition.y > screenY)
                mouseY = screenY;
            else
                mouseY = Input.mousePosition.y;
            //tracking mouse position.
            Vector3 currentScreenSpace = new Vector3(mouseX, mouseY, positionOfScreen.z);
           // Debug.Log(mouseX+":x");
           // Debug.Log(mouseY + "Y");
            //converting screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue;

            //It will update target gameobject's current postion.
            getTarget.transform.position = currentPosition;
        }
        //run current object animation  
        if (flag)
        {
            //Debug.Log(gameObject.tag);
            centre = centerObject.GetComponent<Renderer>().bounds.center;
            gameObject.transform.RotateAround(centre, gameObject.transform.forward, RotateSpeed * Time.deltaTime);
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

                if (hit.transform.gameObject.transform.position == gameObject.transform.position && checkEnvironment(hit.collider.tag))
                   target = hit.collider.gameObject;
            }
        }
        return target;
    }

    bool checkEnvironment(string tag)
    {
        switch (GameManager.sharedInstance.currentMood)
        {

            case Enums.EnvironmentMood.Sunny:
                if (tag != "Fan")
                    return true;
                else
                    return false;
                break;

            case Enums.EnvironmentMood.Windy:
                if (tag != "SunCell")
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
        Debug.Log("Enterede");
        //

        //Debug.Log(Input.mousePosition.y);
        if (col.gameObject.tag == "Grass")
        {
            if (gameObject.tag == "Fan")
            {
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                //Debug.Log(Input.mousePosition.y);
                //Debug.Log(col.transform.position.y);
                isSuitablePlaceToDrag = true;
            }
        }
        else if (col.gameObject.tag == "River")
        {
            if (gameObject.tag == "RiverWheel")
            {
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                isSuitablePlaceToDrag = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("Exit");
        //

        //Debug.Log(Input.mousePosition.y);
        if (col.gameObject.tag == "Grass")
        {
            if (gameObject.tag == "Fan")
            {
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                //Debug.Log(Input.mousePosition.y);
                //Debug.Log(col.transform.position.y);
                isSuitablePlaceToDrag = false;
            }
        }
        else if (col.gameObject.tag == "River")
        {
            if (gameObject.tag == "RiverWheel")
            {
                //centre = centerObject.GetComponent<Renderer>().bounds.center;
                isSuitablePlaceToDrag = false;
            }
        }
    }
    

}