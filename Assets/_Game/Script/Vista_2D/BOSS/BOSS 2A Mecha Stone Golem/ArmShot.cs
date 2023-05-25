using UnityEngine;

public class ArmShot : MonoBehaviour
{
    public Vector3 playerTransform;
    SpriteRenderer sprite;

    public int attackDamage = 30;
    public float bulletSpeed = 10f;

    public Rigidbody2D rb;
    // Serve per far spawnare il proiettile ed spararlo verso il player

    void Update()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = bulletSpeed * playerTransform;

        sprite.flipX = playerTransform.x < 0;
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent(out PlayerCombact player);

        
        if (player != null)
        {
            player.TakeDamagePlayer(attackDamage);
            Destroy(gameObject);
        }
    }
}
