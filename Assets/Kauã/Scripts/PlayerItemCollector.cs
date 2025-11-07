using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [Header("Configuração da coleta")]
    public KeyCode collectKey = KeyCode.E; // tecla para coletar
    public float collectRange = 1.5f; // distância máxima para coletar
    public LayerMask itemLayer; // layer dos itens

    [Header("Configuração do Power-Up")]
    public float duracaoInvencibilidade = 5f; // duração da invencibilidade em segundos

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color corOriginal;
    public bool estaInvencivel = false;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
    }

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
            Debug.Log($"Destruindo: {item.gameObject.name}");
            Destroy(item.gameObject);
        }
    }

    void AtivarPowerUpInvencibilidade()
    {
        if (!estaInvencivel)
        {
            estaInvencivel = true;
            Debug.Log("Power-up de invencibilidade ativado!");

            // Altera a cor do jogador para indicar invencibilidade
            Debug.Log($"spriteRenderer nulo? {spriteRenderer == null}");
            Debug.Log(spriteRenderer.gameObject.name);
            spriteRenderer.color = Color.magenta;                       
            Debug.Log($"Cor definida para: {spriteRenderer.color}");
            Invoke(nameof(TestarCor), 0.1f);

            // Inicia a contagem para voltar ao normal
            Invoke(nameof(DesativarInvencibilidade), duracaoInvencibilidade);
        }
    }
    void TestarCor()
    {
        Debug.Log($"Cor atual após 0.1s: {spriteRenderer.color}");
    }
    void DesativarInvencibilidade()
    {
        estaInvencivel = false;

        // Volta a cor original
        spriteRenderer.color = corOriginal;

        Debug.Log("Invencibilidade acabou!");
    }

    void OnDrawGizmosSelected()
    {
        // Mostra o raio de coleta no editor
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, collectRange);
    }
}