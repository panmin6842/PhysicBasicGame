using UnityEngine;
using UnityEngine.SceneManagement;

public class LobyController : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Draw()
    {
        SceneManager.LoadScene("DrawScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

}

