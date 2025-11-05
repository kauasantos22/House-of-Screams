using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [Header("Configuração da coleta")]
    public KeyCode collectKey = KeyCode.E; // tecla para coletar
    public float collectRange = 1.5f; // distância máxima para coletar
    public LayerMask itemLayer; // layer dos itens

    [Header("Configuração do Power-Up")]
    public float duracaoInvencibilidade = 5f; // duração da invencibilidade em segundos

    void Update()
    {
        if (Input.GetKeyDown(collectKey)) 
        {
            ColetarItem();
        }
    }

    void ColetarItem()
    {
        // Detecta o item mais próximo dentro do raio
        Collider2D item = Physics2D.OverlapCircle(transform.position, collectRange, itemLayer);

        if (item != null)
        {
            Debug.Log("Item coletado: " + item.name);

            // Verifica se o item é um power-up de invencibilidade
            if (item.CompareTag("PowerUp"))
            {
                AtivarPowerUpInvencibilidade();
            }

            // destrói o item coletado
            Destroy(item.gameObject);
        }
    }

    void AtivarPowerUpInvencibilidade()
    {
        PlayerInvencivel playerInvencivel = GetComponent<PlayerInvencivel>();
        if (playerInvencivel != null)
        {
            playerInvencivel.AtivarInvencibilidade(duracaoInvencibilidade);
            Debug.Log("Power-up de invencibilidade ativado!");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Mostra o raio de coleta no editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectRange);
    }
}