using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float speed = 2f;              // velocidade do inimigo
    public float detectionRange = 5f;     // distância pra começar a seguir
    public float attackRange = 1.5f;      // distância pra poder ser morto

    [Header("Debug")]
    public bool debugGizmos = true;

    private Transform player;
    private bool isDead = false;

    void Start()
    {
        // Acha o player pela tag
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogError("[Enemy] Player com tag 'Player' não encontrado na cena.");
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        // Segue o player se estiver dentro do alcance de detecção
        if (dist <= detectionRange)
        {
            Vector3 target = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // Inverte o sprite conforme a posição do player
            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // 💥 Chamado pelo PlayerAttack quando o inimigo é atingido
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("[Enemy] Inimigo morreu!");
        Destroy(gameObject); // Faz o inimigo desaparecer da cena
    }

    // Mostra alcance no editor
    void OnDrawGizmosSelected()
    {
        if (!debugGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
