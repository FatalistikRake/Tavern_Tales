using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flip_image_Left : MonoBehaviour

{
    private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            SpriteRenderer.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            SpriteRenderer.flipX = false;
        }
    }
}
