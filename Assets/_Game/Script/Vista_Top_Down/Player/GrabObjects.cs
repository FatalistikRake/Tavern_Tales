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
        RaycastHit2D hitinfo = Physics2D.Raycast(raybPoint.position, transform.right, rayDistance);

        if (hitinfo.collider != null && hitinfo.collider.gameObject.layer == layerIndex)
        {
            //grab objects
            if (Input.GetKeyDown(KeyCode.Space) && grabbedObjects == null)
            {
                grabbedObjects = hitinfo.collider.gameObject;
                grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObjects.transform.position = grabPoint.position;
                grabbedObjects.transform.SetParent(transform);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                grabbedObjects.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObjects.transform.SetParent(null);
                grabbedObjects = null;
            }
        }

        Debug.DrawRay(raybPoint.position, transform.right * rayDistance);
    }
}
