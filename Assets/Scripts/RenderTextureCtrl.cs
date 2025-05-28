using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

public class RenderTextureCtrl : MonoBehaviour
{
    [SerializeField] RenderTexture r_Texture;
    public Image[] objectSprites;

    [SerializeField] GameObject customTool;
    [SerializeField] GameObject choosepage;
    [SerializeField] LineController lineController;

    [SerializeField] Sprite baseSprite;

    string name;

    Texture2D texture;
    string path;
    string fileName;

    Vector2 center;
    float radius;

    int width = 0;
    int height = 0;

    private void Start()
    {

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("MainScene");
        }

    }

    public void BackButton() //�ڷΰ���
    {
        customTool.SetActive(false);
        choosepage.SetActive(true);
        lineController.enabled = false;

        objectSprites[0].sprite = LoadSprite("CircleObjectImage0");
        objectSprites[1].sprite = LoadSprite("CircleObjectImage1");
        objectSprites[2].sprite = LoadSprite("CircleObjectImage2");
        objectSprites[3].sprite = LoadSprite("CircleObjectImage3");
        objectSprites[4].sprite = LoadSprite("CircleObjectImage4");
        objectSprites[5].sprite = LoadSprite("CircleObjectImage5");
        objectSprites[6].sprite = LoadSprite("CircleObjectImage6");
        objectSprites[7].sprite = LoadSprite("CircleObjectImage7");
    }
    public void TextureCapture() //���� ��ư ������ ĸ�ĵǰ� ���� sprite �̹��� �ٲ�
    {
        StartCoroutine(SaveTexture());
    }
    public void Name(string renderName)
    {
        name = renderName;
    }

    IEnumerator SaveTexture()
    {
        yield return new WaitForEndOfFrame();

        //Texture2D�� ��ȯ
        RenderTexture.active = r_Texture;

        texture = new Texture2D(r_Texture.width, r_Texture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, r_Texture.width, r_Texture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        CircleMask(texture);

        //Texture2D -> sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        //����
        fileName = name + ".png";
        //path = Application.dataPath + "/Resources/" + fileName;
        path = Path.Combine(Application.persistentDataPath, fileName);
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        //Debug.Log("�̹��� ���� �Ϸ�: " + path);

        //���� �� ���� ��� ��������
        //#if UNITY_EDITOR
        //AssetDatabase.Refresh();
        //#endif

        //ui sprite �ٲ��ֱ�
        //saveObject.GetComponent<Image>().sprite = sprite;
    }

    void CircleMask(Texture2D texture)
    {
        width = texture.width;
        height = texture.height;
        Color[] pixels = texture.GetPixels(); //�ȼ� ������
        center = new Vector2(width / 2, height / 2); //��� ��������
        radius = width / 2; //������ ��������

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //���������� ���̰� ��� ����̹Ƿ� ����ȭ�� ���Ѿ���
                float dist = Vector2.Distance(new Vector2(x, y), center);
                if (dist > radius)
                {
                    int index = y * width + x; //��� ��ġ
                    pixels[index].a = 0;
                }
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
    }

    public Sprite LoadSprite(string fileName) //�ҷ�����
    {
        fileName += ".png";

        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path))
        {
            return baseSprite;
        }

        byte[] imageData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2); //�̸� �ؽ�ó�� ����
        tex.LoadImage(imageData);

        //Debug.Log("�̹��� �ҷ����� ���: " + path);

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 250f);
    }
}
