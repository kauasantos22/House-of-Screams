using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class TrocarCenaComDelay : MonoBehaviour
{
    public string nomeDaCena;
    public float tempoDeEspera = 1.5f;
    public KeyCode botao = KeyCode.E;

    public Image telaPreta;     // Imagem preta na tela
    public float velocidadeFade = 1f;

    private bool podeTrocar = false;

    void Start()
    {
        // Garante que a tela começa transparente
        if (telaPreta != null)
            telaPreta.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(botao) && !podeTrocar)
        {
            podeTrocar = true;
            StartCoroutine(TrocarDepoisDoTempo());
        }
    }

    IEnumerator TrocarDepoisDoTempo()
    {
        // Começa o fade para o preto
        if (telaPreta != null)
        {
            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * velocidadeFade;
                telaPreta.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }

        // Espera o tempo configurado
        yield return new WaitForSeconds(tempoDeEspera);

        // Troca de cena
        SceneManager.LoadScene(nomeDaCena);
    }
}
