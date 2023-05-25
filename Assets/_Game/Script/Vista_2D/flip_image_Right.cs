using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flip_image_Right : MonoBehaviour

{
    private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(0f, 180f, 0f);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
