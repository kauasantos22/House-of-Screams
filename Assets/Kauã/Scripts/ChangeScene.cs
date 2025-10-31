using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Nome da cena para onde voc� quer mudar
    public string sceneName;

    // Essa fun��o ser� chamada quando o bot�o for clicado
    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}