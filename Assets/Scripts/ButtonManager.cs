using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;
    public void Back()
    {
        exitPanel.SetActive(true);
    }

    public void ReBack()
    {
        exitPanel.SetActive(false);
    }
}
