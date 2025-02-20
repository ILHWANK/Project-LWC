using System.Collections;
using System.Collections.Generic;
using Script.Controller;
using Script.Manager;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] 
    Camera mainCamera;

    [SerializeField] 
    Button interactionButton;

    bool isTrigger = false;
    bool isCollide = false;
    bool isInteraction = false;

    PlayerAction playerAction;

    void Start(){
        playerAction = FindObjectOfType<PlayerAction>();
        interactionButton.interactable = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other != null){
            Collide(other);
        }
        else{
            NotCollide();
        } 
    }

    private void OnTriggerExit2D(Collider2D other){
        NotCollide();
    }

    private void Collide(Collider2D pCollider2D){
        if (pCollider2D.gameObject.tag == "Interaction"){
            if(!isCollide){
                isCollide = true;

                var objectController = pCollider2D.gameObject.GetComponent<ObjectController>();
                var objectType = objectController.GetObjectType();

                playerAction.InteractionType = objectType;

                interactionButton.interactable = true;
            }
        }
        else{
            NotCollide();
        }
    }

    private void NotCollide(){
        if(isCollide){
            isCollide = false;
            interactionButton.interactable = false;
        }
    }
}
