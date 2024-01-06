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

    // Joystick
    public FixedJoystick joy;
    public float speed;
    
    Rigidbody2D rb;
    Vector2 moveVec;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        interactionButton.interactable = false;
    }

    void FixedUpdate()
    {
        float x = joy.Horizontal;
        float y = joy.Vertical;

        // Move Position 
        moveVec = new Vector2(x, y) * speed * Time.fixedDeltaTime;              
        rb.MovePosition(rb.position + moveVec);
                
        if (moveVec.sqrMagnitude == 0)
            return; 
    }

    void Update()
    {
        if(isCollide && Input.GetKeyDown(KeyCode.F)){
            Debug.Log("확인용");
        }
    }

    void LateUpdate()
    {
        //
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
                interactionButton.interactable = false;
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
            interactionButton.interactable = true;
            interactionButton.interactable = false;
        }
    }
}
