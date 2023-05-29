using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private GameObject grabbedObject;
    private bool isGrabbing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isGrabbing)
            {
                DropObject();
            }
            else
            {
                GrabObject();
            }
        }

        if (isGrabbing)
        {
            MoveObject();
        }
    }

    void GrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("ObjectInteractable"))
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.transform.SetParent(transform);
                isGrabbing = true;
            }
        }
    }

    void MoveObject()
    {
        grabbedObject.transform.position = transform.position + transform.forward;
    }

    void DropObject()
    {
        grabbedObject.transform.SetParent(null);
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject = null;
        isGrabbing = false;
    }
}
