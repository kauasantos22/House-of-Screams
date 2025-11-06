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

    private bool isInvincible = false;

    void Start()

    {

        currentHealth = maxHealth;

        if (healthBar != null)

        {

            healthBar.maxValue = maxHealth;

            healthBar.value = currentHealth;

        }

    }

    public void TakeDamage(int amount)

    {

        if (isInvincible) return;

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

        isInvincible = true;

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;

    }

    void Die()

    {

        Debug.Log("💀 Player morreu!");

        gameObject.SetActive(false);

    }

}

