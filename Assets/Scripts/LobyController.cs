using UnityEngine;
using UnityEngine.SceneManagement;

public class LobyController : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.bgmAudioSource.clip = SoundManager.instance.lobySceneBGM;
        SoundManager.instance.bgmAudioSource.Play();
    }

    public void GameStart()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        SceneManager.LoadScene("MainScene");
    }

    public void Draw()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        SceneManager.LoadScene("DrawScene");
    }

    public void Exit()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        Application.Quit();
    }

}

