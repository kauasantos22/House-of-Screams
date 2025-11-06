using UnityEngine;

public class PlayerInvensivel : MonoBehaviour
{
    [Header("Configuração da Vida")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Invulnerabilidade")]
    public bool invulneravel = false; // impede o jogador de tomar dano

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dano)
    {
        // Impede que o jogador leve dano durante a invencibilidade
        if (invulneravel)
            return;

        currentHealth -= dano;

        // Garante que a vida não fique negativa
        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log("Jogador tomou dano! Vida atual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Jogador morreu!");
        // Aqui você pode adicionar animação de morte, reinício da cena etc.
    }

    public void Curar(int quantidade)
    {
        currentHealth += quantidade;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Jogador curado! Vida atual: " + currentHealth);
    }
}