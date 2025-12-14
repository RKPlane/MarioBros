using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float followRange = 3f;
    public Transform player;

    private Rigidbody2D rb;
    private int direction = 1;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = player.position.x - transform.position.x;

        if (Mathf.Abs(distanceToPlayer) < followRange)
        {
            direction = distanceToPlayer > 0 ? 1 : -1;  //patrulla
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    public void Die()
    {
        isDead = true;
        GameManager.instance.EnemyKilled();

        //rebote
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.3f);
    }
}


