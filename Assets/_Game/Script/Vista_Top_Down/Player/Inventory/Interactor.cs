using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float verticalRadius;
    public float horizontalRadius;

    public bool IsInteracting { get; private set; }


    private void Update()
    {
        var colliders = Physics2D.OverlapCapsuleAll(
        InteractionPoint.position,
        new Vector2(horizontalRadius, verticalRadius * 2f),
        CapsuleDirection2D.Vertical,
        0f,
        InteractionLayer
        );

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    StartInteraction(interactable);
                }
            }
        }
        // Disegna la capsula sovrapposta con l'asse verticale più stretta
        DebugDrawOverlapCapsule(InteractionPoint.position, verticalRadius, horizontalRadius, Color.red);
    }

    void DebugDrawOverlapCapsule(Vector2 center, float verticalRadius, float horizontalRadius, Color color)
    {
        Vector2 pointA = center + new Vector2(0, verticalRadius);
        Vector2 pointB = center - new Vector2(0, verticalRadius);

        Debug.DrawLine(pointA + new Vector2(-horizontalRadius, 0), pointA + new Vector2(horizontalRadius, 0), color);
        Debug.DrawLine(pointB + new Vector2(-horizontalRadius, 0), pointB + new Vector2(horizontalRadius, 0), color);

        Debug.DrawLine(pointA + new Vector2(-horizontalRadius, 0), pointB + new Vector2(-horizontalRadius, 0), color);
        Debug.DrawLine(pointA + new Vector2(horizontalRadius, 0), pointB + new Vector2(horizontalRadius, 0), color);
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteracting = true;
    }

    void EndInteracting()
    {
        IsInteracting = false;
    }
}
