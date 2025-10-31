using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Nome da cena para onde você quer mudar
    public string sceneName;

    // Essa função será chamada quando o botão for clicado
    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}