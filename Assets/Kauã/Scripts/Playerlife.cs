using UnityEngine;

using UnityEngine.UI;

public class PlayerLife : MonoBehaviour

{   

    [Header("Configuração de Vida")]

    public int maxHealth = 100;

    public int currentHealth;

    [Header("Referência da Barra de Vida")]

    public Slider healthBar; // arrasta o Slider da UI aqui

    [Header("Efeito de dano")]

    public float invincibleTime = 1f; // tempo de invencibilidade após levar dano

    PlayerItemCollector collector;
    void Start()

    {
        collector = GetComponent<PlayerItemCollector>();
        currentHealth = maxHealth;

        print(collector.estaInvencivel);

        if (healthBar != null)

        {

            healthBar.maxValue = maxHealth;

            healthBar.value = currentHealth;

        }

    }

    public void TakeDamage(int amount)

    {
        print(collector.estaInvencivel);

        if (collector.estaInvencivel) return;

        currentHealth -= amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("🔥 Player levou dano! Vida atual: " + currentHealth);

        if (healthBar != null)

            healthBar.value = currentHealth;

        if (currentHealth <= 0)

        {

            Die();

        }

        else

        {

            StartCoroutine(Invincibility());

        }

    }

    private System.Collections.IEnumerator Invincibility()

    {

        collector.estaInvencivel = true;

        yield return new WaitForSeconds(invincibleTime);

        collector.estaInvencivel = false;

    }

    void Die()

    {

        Debug.Log("💀 Player morreu!");

        gameObject.SetActive(false);

    }

}

