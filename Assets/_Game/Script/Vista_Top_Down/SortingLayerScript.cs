using System.Runtime.CompilerServices;
using UnityEngine;

public enum SortingLayer
{
    Player = 0,
    Tables = 1
}

public class SortingLayerScript : MonoBehaviour
{

    private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }
}
