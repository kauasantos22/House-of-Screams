using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float knockbackForce = 5f;     // força do empurrão no player
    public float knockbackUp = 2f;        // empurrão vertical opcional

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // botão esquerdo
        {
            ApplyKnockback();
            Destroy(gameObject); // mata o inimigo
        }
    }

    void ApplyKnockback()
    {
        // acha o player na cena
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // direção do knockback (do inimigo para o player)
                Vector2 direction = (player.transform.position - transform.position).normalized;

                // aplica força
                rb.AddForce(new Vector2(direction.x, knockbackUp) * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
