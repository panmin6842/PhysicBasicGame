using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButton : MonoBehaviour
{

    public void ReStart()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        SceneManager.LoadScene("MainScene");
    }

    public void Loby()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        SceneManager.LoadScene("LobyScene");
    }

    public void Exit()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        Application.Quit();
    }

}
