using System.Collections;
using System.Collections.Generic;
using Script.Controller;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // Joystick
    [SerializeField] DynamicJoystick joyStick;
    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 moveVec;

    // MoveAnimation
    [SerializeField] Animator animator;

    public string currentDialogueGroup;

    public ObjectController.ObjectType InteractionType;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float x = joyStick.Horizontal;
        float y = joyStick.Vertical;

        Vector2 directionVector = new Vector2(0, 0);

        if (x != 0 || y != 0)
        {
            float angle = Quaternion.FromToRotation(new Vector3(x, y, 0), Vector3.up).eulerAngles.z;

            if (22.5 <= angle && angle < 67.5)
                directionVector = new Vector2(1, 1);
            else if (67.5 <= angle && angle < 112.5)
                directionVector = new Vector2(1, 0);
            else if (112.5 <= angle && angle < 157.5)
                directionVector = new Vector2(1, -1);
            else if (157.5 <= angle && angle < 202.5)
                directionVector = new Vector2(0, -1);
            else if (202.5 <= angle && angle < 247.5)
                directionVector = new Vector2(-1, -1);
            else if (247.5 <= angle && angle < 292.5)
                directionVector = new Vector2(-1, 0);
            else if (292.5 <= angle && angle < 337.5)
                directionVector = new Vector2(-1, 1);
            else
                directionVector = new Vector2(0, 1);

            animator.SetBool("IsMove", true);
        }
        else
            animator.SetBool("IsMove", false);

        animator.SetFloat("Inputx", x);
        animator.SetFloat("Inputy", y);

        // Move Position 
        moveVec = directionVector * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveVec);

        if (moveVec.sqrMagnitude == 0)
            return;
    }

    void Update()
    {


    }

    void LateUpdate()
    {
    
    }
}
