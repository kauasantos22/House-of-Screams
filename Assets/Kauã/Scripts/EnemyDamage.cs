using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Configura��es de Dano e Knockback")]
    public int damageAmount = 20;
    public float knockbackForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);

                if (rb != null && playerHealth.currentHealth > 0)
                {
                    // calcula dire��o horizontal
                    Vector2 direction = (collision.transform.position - transform.position).normalized;

                    // empurra s� no eixo X (pra evitar jogar o player pra cima/baixo demais)
                    Vector2 knockback = new Vector2(direction.x, 0.5f).normalized * knockbackForce;

                    // zera a velocidade pra o empurr�o ser forte
                    rb.linearVelocity = Vector2.zero;

                    // aplica o empurr�o instant�neo
                    rb.AddForce(knockback, ForceMode2D.Impulse);
                }
            }
        }
    }
}
