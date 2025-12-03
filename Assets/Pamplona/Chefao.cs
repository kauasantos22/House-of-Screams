using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Vida")]
    public int health = 3;

    [Header("Movimento")]
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    private Transform target;

    [Header("Ataque no Player")]
    public float attackRange = 1.5f;
    public int damage = 20;
    public float attackCooldown = 1f;
    private float attackTimer;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = pointA;
    }

    void Update()
    {
        Move();
        AttackPlayer();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            target = target == pointA ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    void AttackPlayer()
    {
        if (player == null) return;

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange && attackTimer <= 0)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);

            attackTimer = attackCooldown;
        }
    }

    // 🎯 SÓ ISSO PRECISA PARA O PLAYER MATAR O BOSS!
    public void Die()
    {
        Debug.Log("BOSS MORREU!");
        Destroy(gameObject);
    }

    // 👉 PlayerAttack te acerta por OverlapCircleAll, então isso funciona:
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }
}
