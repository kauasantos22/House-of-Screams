using UnityEngine;

public class PlayerInvencivel : MonoBehaviour
{
    private bool estaInvencivel = false;
    private SpriteRenderer spriteRenderer;
    private Color corOriginal;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
    }

    public void AtivarInvencibilidade(float duracao)
    {
        if (!estaInvencivel)
            StartCoroutine(InvencibilidadeCoroutine(duracao));
    }

    private System.Collections.IEnumerator InvencibilidadeCoroutine(float duracao)
    {
        estaInvencivel = true;

        // Muda a cor para indicar invencibilidade (ex: amarelo)
        spriteRenderer.color = Color.rebeccaPurple;

        // Aqui você pode desativar o dano, colisões, etc.
        // Exemplo: GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(duracao);

        // Volta ao normal
        spriteRenderer.color = corOriginal;
        estaInvencivel = false;

        // Reativa o que foi desativado (se aplicável)
        // GetComponent<Collider2D>().enabled = true;
    }

    // Método para verificar se o player está invencível
    public bool EstaInvencivel()
    {
        return estaInvencivel;
    }
}
