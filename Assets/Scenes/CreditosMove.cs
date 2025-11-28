using UnityEngine;

public class CreditosMove : MonoBehaviour
{
    [Header("Configurações")]
    public float speed = 50f;        // Velocidade que sobe
    public float limiteY = 800f;     // Altura máxima
    public float delayReset = 1f;    // Tempo de espera antes de reiniciar

    private Vector3 posInicial;      // Posição que ele volta
    private bool reiniciando = false;

    void Start()
    {
        // Salva posição inicial ao iniciar
        posInicial = transform.localPosition;
        transform.localPosition = posInicial;
    }

    void Update()
    {
        // Se estiver reiniciando, não move
        if (reiniciando) return;

        // Move para cima se ainda não atingiu o limite
        if (transform.localPosition.y < limiteY)
        {
            transform.localPosition += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            // Chegou no topo — inicia coroutine de reset
            StartCoroutine(ResetAutomatico());
        }
    }

    System.Collections.IEnumerator ResetAutomatico()
    {
        reiniciando = true;

        // Espera um tempo opcional antes de reiniciar
        yield return new WaitForSeconds(delayReset);

        // Volta para a posição inicial
        transform.localPosition = posInicial;

        reiniciando = false;
    }
}
