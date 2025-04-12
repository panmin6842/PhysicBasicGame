using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RenderTextureCtrl : MonoBehaviour
{
    [SerializeField] RenderTexture r_Texture;
    [SerializeField] string name;

    Texture2D texture;
    string path;
    string fileName;

    Vector2 center;
    float radius;

    [SerializeField] GameObject saveObject;

    int width = 0;
    int height = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
           StartCoroutine(SaveTexture());
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("MainScene");
        }
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
        path = Application.dataPath + "/Resources/" + fileName;
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        //Debug.Log("�̹��� ���� �Ϸ�: " + path);

        //���� �� ���� ��� ��������
#if UNITY_EDITOR
         AssetDatabase.Refresh();
#endif

        //ui sprite �ٲ��ֱ�
        saveObject.GetComponent<Image>().sprite = sprite;
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
            for(int x = 0; x < width; x++)
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
}
