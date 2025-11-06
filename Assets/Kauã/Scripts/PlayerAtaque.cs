using UnityEngine;

public class PlayerAtaque : MonoBehaviour
{
    public float attackRadius = 1.5f;
    public float attackCooldown = 0.5f;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public bool debugGizmos = true;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            DoAttack();
        }
    }

    void DoAttack()
    {
        Vector2 origin = (attackPoint != null) ? (Vector2)attackPoint.position : (Vector2)transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, attackRadius, enemyLayer);

        Debug.Log("[PlayerAttack] atacou. Inimigos atingidos: " + hits.Length);

        foreach (Collider2D c in hits)
        {
            Enemy enemy = c.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!debugGizmos) return;
        Gizmos.color = Color.red;
        Vector3 pos = attackPoint != null ? attackPoint.position : transform.position;
        Gizmos.DrawWireSphere(pos, attackRadius);
    }
}
