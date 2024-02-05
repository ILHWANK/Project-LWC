using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    Renderer renderer;

    [SerializeField]
    private float scrollSpeed;

    private float offset;
    void Start()
    {
        if(!gameObject.TryGetComponent<Renderer>(out renderer)) { Debug.Log("not found"); }
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;

        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
