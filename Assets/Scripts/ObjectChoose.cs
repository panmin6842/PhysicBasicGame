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

    public void ObjectDecision() //���� ��ư ������ ��
    {
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
        if (lineLength > 0) //�׷����ִ� �� �� ���ֱ�
        {
            for (int i = 0; i < lineLength; i++)
            {
                Destroy(lines[i]);
            }

        }

        customTool.SetActive(true);
        choosePage.SetActive(false);
    }

    public void Back() //�ڷΰ���(�κ� ������ ��)
    {
        SceneManager.LoadScene("LobyScene");
    }

    //�� ������Ʈ Ŭ�� ��
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
