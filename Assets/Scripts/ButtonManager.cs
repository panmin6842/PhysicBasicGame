using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;
    public void Back()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        exitPanel.SetActive(true);
    }

    public void ReBack()
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        exitPanel.SetActive(false);
    }
}
