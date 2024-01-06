using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] 
    Camera mainCamera;

    [SerializeField] 
    Button interactionButton;

    RaycastHit hitInfo;

    bool isCollide = false;

    // Update is called once per frame
    void Update()
    {
        CheckCollide();
    }

    void CheckCollide(){
        Vector3 touchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        if (Physics.Raycast(mainCamera.ScreenPointToRay(touchPosition), out hitInfo, 100)){
            Collide();
            Debug.Log("Ture : " + touchPosition);
        }
        else{
            NotCollide();
            Debug.Log("False : " +touchPosition);
        }
    }

    void Collide(){
        if(hitInfo.transform.CompareTag("Interaction")){
            if(!isCollide){
                isCollide = true;
                interactionButton.interactable = true;
                interactionButton.interactable = false;
            }
        }
        else{
            NotCollide();
        }
    }

    void NotCollide(){
        if(isCollide){
            isCollide = false;
            interactionButton.interactable = false;
            interactionButton.interactable = true;
        }
    }
}
