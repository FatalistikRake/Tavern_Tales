using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform raybPoint;
    [SerializeField]
    private float rayDistance;

    private GameObject grabbedObjects;
    private int layerIndex;


    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
    }

    // Update is called once per frame
    void Update()
    {

        /*RaycastHit2D hitInfo = Physics2D.Raycast(raybPoint.position, transform.up, rayDistance);

        if (Input.GetKeyDown(KeyCode.Space) && grabbedObjects == null && hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            //grab Objects
            grabbedObjects = hitInfo.collider.gameObject;
            grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = true;
            grabbedObjects.GetComponent<Collider2D>().enabled = false;
            grabbedObjects.transform.position = grabPoint.position;
            grabbedObjects.transform.SetParent(transform);
            grabbedObjects.GetComponent<OpenPersistem>().canDisable = false;


        }
        else if (Input.GetKeyDown(KeyCode.Space) && grabbedObjects != null)
        {
            grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObjects.GetComponent<Collider2D>().enabled = true;
            grabbedObjects.transform.SetParent(null);
            GameEvent.Instance.OnPikupObject(grabbedObjects);
            grabbedObjects.GetComponent<OpenPersistem>().canDisable = true;
            grabbedObjects = null;
        }

        Debug.DrawRay(raybPoint.position, transform.up * rayDistance);*/
    }

    void OnDrawGizmosSelected()
    {
        if (grabPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(grabPoint.position, rayDistance);
    }
}
