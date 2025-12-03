using UnityEngine;

public class Feio : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int amount = 1)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("[Enemy] Morreu: " + gameObject.name);
        Destroy(gameObject);
    }
}
