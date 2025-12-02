using UnityEngine;

public class ItemCarta : MonoBehaviour
{
    public GameObject cartaUI;     // Arraste sua UI aqui no inspector
    public string textoDaCarta;    // Texto que aparecerá na carta

    private bool cartaAberta = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cartaUI.SetActive(true);                          // Mostra a carta
            cartaUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = textoDaCarta;
            Time.timeScale = 0f;                              // Pausa o jogo
            cartaAberta = true;
        }
    }

    private void Update()
    {
        if (cartaAberta && Input.GetKeyDown(KeyCode.Escape))  // Tecla para fechar
        {
            cartaUI.SetActive(false);
            Time.timeScale = 1f;  // Despausa
            cartaAberta = false;
        }
    }
}
