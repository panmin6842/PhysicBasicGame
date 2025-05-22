using UnityEngine;

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
    }

    public void ObjectDecision() //���� ��ư ������ ��
    {
        lineController.enabled = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        customCircle.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(renderName);
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
