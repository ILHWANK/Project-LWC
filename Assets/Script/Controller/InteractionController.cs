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

    bool isTrigger = false;
    bool isCollide = false;
    bool isInteraction = false;

    DialogueManager dialogueManager;

    [SerializeField]
    string tempdialogueGroup;

    void Start(){
        dialogueManager = FindObjectOfType<DialogueManager>();
        interactionButton.interactable = false;
    }

    void Update()
    {
        if (isCollide && Input.GetKeyDown(KeyCode.F)){
            dialogueManager.TempPlayStory();
        }
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

    void Collide(Collider2D pCollider2D){
        if (pCollider2D.gameObject.tag == "Interaction"){
            if(!isCollide){
                isCollide = true;
                interactionButton.interactable = true;
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
        }
    }
}
