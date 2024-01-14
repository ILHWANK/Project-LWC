using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject;

    public Vector2 center;
    public Vector2 size;

    public float moveSpeed;
    float height;
    float width;

    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
     
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, Time.deltaTime * moveSpeed);

        float positionX = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -positionX + center.x, positionX + center.x);

        float positionY = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -positionY + center.x, positionY + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
