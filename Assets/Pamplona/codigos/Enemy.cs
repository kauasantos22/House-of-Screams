using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRange = 5f;
    public float speed = 2f;
    public bool debugGizmos = true;
    public int damage = 20;
    public float knockbackForce = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
        else
            Debug.LogError("[Enemy] Player com tag 'Player' não encontrado.");

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        // Segue o player se estiver dentro do alcance
        if (dist <= detectionRange)
        {
            Vector3 target = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // Vira o sprite pro lado certo
            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);

                // Aplica empurrão no player
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                    playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log("[Enemy] Inimigo morto!");
        Destroy(gameObject, 0.05f);
    }

    void OnDrawGizmosSelected()
    {
        if (!debugGizmos) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
