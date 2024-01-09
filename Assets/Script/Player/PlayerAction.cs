using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // Joystick
    public FixedJoystick joy;
    public float speed;

    Rigidbody2D rb;
    Vector2 moveVec;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
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
        
    }

    void LateUpdate()
    {
    
    }
}
