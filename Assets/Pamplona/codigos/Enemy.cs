using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CheckGround();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(movingRight ? speed : -speed, rb.linearVelocity.y);
    }

    void CheckGround()
    {
        // Verifica se h� ch�o � frente
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f, groundLayer);
        if (!groundInfo.collider)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 center = collision.collider.bounds.center;

            if (contactPoint.y > center.y)
            {
                // Jogador pulou em cima
                Die();
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 10f); // d� um pulo no jogador
                }
            }
            else
            {
                Debug.Log("Player levou dano!");
                // Aqui voc� pode chamar o script de vida do player
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
