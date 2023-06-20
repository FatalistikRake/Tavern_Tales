using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteracting { get; private set; }

    [SerializeField] private CapsuleCollider2D interactionCollider; // Esposizione della variabile nell'Editor

    private void Awake()
    {
        if (interactionCollider == null)
            interactionCollider = InteractionPoint.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(InteractionPoint.position, interactionCollider.size, interactionCollider.direction, InteractionPointRadius, InteractionLayer);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null) StartInteraction(interactable);
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteracting = true;
    }

    void EndInteraction()
    {
        IsInteracting = false;
    }
}
