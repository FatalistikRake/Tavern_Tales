using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private PlayerMovementTopDown piattoPos;

    public void SpawnDroppedItem()
    {
        Instantiate(item, piattoPos.piattoPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
