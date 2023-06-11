using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private PlayerMovementTopDown piattoPos;

    private void Start()
    {
        piattoPos = FindObjectOfType<PlayerMovementTopDown>();
    }

    public void SpawnDroppedItem()
    {
        Debug.Log(piattoPos.piattoPosition);
        Instantiate(item, piattoPos.piattoPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
