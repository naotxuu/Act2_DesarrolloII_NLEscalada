using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Escenario");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
