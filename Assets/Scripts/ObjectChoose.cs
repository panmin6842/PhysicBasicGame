using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectChoose : MonoBehaviour
{
    [SerializeField] RenderTextureCtrl renderTextureCtrl;
    string renderName;
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject choosePage;
    [SerializeField] GameObject customTool;
    [SerializeField] GameObject customCircle;

    [SerializeField] LineController lineController;

    private void Start()
    {
        SoundManager.instance.bgmAudioSource.clip = SoundManager.instance.drawSceneBGM;
        SoundManager.instance.bgmAudioSource.Play();

        renderName = renderTextureCtrl.name;

        renderTextureCtrl.objectSprites[0].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage0");
        renderTextureCtrl.objectSprites[1].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage1");
        renderTextureCtrl.objectSprites[2].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage2");
        renderTextureCtrl.objectSprites[3].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage3");
        renderTextureCtrl.objectSprites[4].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage4");
        renderTextureCtrl.objectSprites[5].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage5");
        renderTextureCtrl.objectSprites[6].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage6");
        renderTextureCtrl.objectSprites[7].sprite = renderTextureCtrl.LoadSprite("CircleObjectImage7");
    }

    public void ObjectDecision() //결정 버튼 눌렀을 시
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        lineController.ClearAllLines();

        lineController.enabled = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        customCircle.GetComponent<SpriteRenderer>().sprite = renderTextureCtrl.LoadSprite(renderName);
        customCircle.GetComponent<SpriteRenderer>().color = Color.white;
        renderTextureCtrl.Name(renderName);

        int lineLength = GameObject.FindGameObjectsWithTag("Line").Length;
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        if (lineLength > 0) //그려져있는 선 다 없애기
        {
            for (int i = 0; i < lineLength; i++)
            {
                Destroy(lines[i]);
            }

        }

        customTool.SetActive(true);
        choosePage.SetActive(false);
    }

    public void Back() //뒤로가기(로비 씬으로 감)
    {
        SoundManager.instance.buttonAudioSource.PlayOneShot(SoundManager.instance.buttonClickClip);
        SceneManager.LoadScene("LobyScene");
    }

    //원 오브젝트 클릭 시
    public void Object0Click()
    {
        ObjectClickButton("CircleObjectImage0", 0);
    }
    public void Object1Click()
    {
        ObjectClickButton("CircleObjectImage1", 1);
    }

    public void Object2Click()
    {
        ObjectClickButton("CircleObjectImage2", 2);
    }

    public void Object3Click()
    {
        ObjectClickButton("CircleObjectImage3", 3);
    }

    public void Object4Click()
    {
        ObjectClickButton("CircleObjectImage4", 4);
    }

    public void Object5Click()
    {
        ObjectClickButton("CircleObjectImage5", 5);
    }

    public void Object6Click()
    {
        ObjectClickButton("CircleObjectImage6", 6);
    }

    public void Object7Click()
    {
        ObjectClickButton("CircleObjectImage7", 7);
    }

    void ObjectClickButton(string name, int buttonCount)
    {
        if (!buttons[buttonCount].activeSelf)
        {
            buttons[buttonCount].SetActive(true);
            renderName = name;
        }
        else if (buttons[buttonCount].activeSelf)
        {
            buttons[buttonCount].SetActive(false);
            renderName = null;
        }
    }
}
