using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configuração do Ataque")]
    public float attackRadius = 1.5f;          // alcance
    public float attackCooldown = 0.5f;        // tempo entre ataques
    public LayerMask enemyLayer;               // layer dos inimigos/boss
    public Transform attackPoint;              // ponto onde o ataque sai

    [Header("Debug")]
    public bool debugGizmos = true;

    private float nextAttackTime = 0f;

    void Update()
    {
        // clique esquerdo e cooldown
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            DoAttack();
        }
    }

    void DoAttack()
    {
        // se não tiver attackpoint, usa a posição do player
        Vector2 origin = (attackPoint != null) ? (Vector2)attackPoint.position : (Vector2)transform.position;

        // procura inimigos dentro do círculo
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, attackRadius, enemyLayer);

        Debug.Log("[PlayerAttack] atacou. Inimigos atingidos: " + hits.Length);

        foreach (Collider2D c in hits)
        {
            // 🔥 1. Verifica se é BOSS
            Boss boss = c.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(1);
                continue; // evita rodar o Enemy também
            }

            // 🔥 2. Verifica se é inimigo comum
            Enemy enemy = c.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }
    }

    // desenha o círculo no editor
    void OnDrawGizmosSelected()
    {
        if (!debugGizmos) return;

        Gizmos.color = Color.red;
        Vector3 pos = attackPoint != null ? attackPoint.position : transform.position;
        Gizmos.DrawWireSphere(pos, attackRadius);
    }
}
