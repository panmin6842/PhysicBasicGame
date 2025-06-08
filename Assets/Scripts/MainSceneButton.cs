using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButton : MonoBehaviour
{

    public void ReStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Loby()
    {
        SceneManager.LoadScene("LobyScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
