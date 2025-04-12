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

        //Texture2D로 변환
        RenderTexture.active = r_Texture;

        texture = new Texture2D(r_Texture.width, r_Texture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, r_Texture.width, r_Texture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        CircleMask(texture);

        //Texture2D -> sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        //저장
        fileName = name + ".png";
        path = Application.dataPath + "/Resources/" + fileName;
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        //Debug.Log("이미지 저장 완료: " + path);

        //저장 후 에셋 즉시 리프레시
#if UNITY_EDITOR
         AssetDatabase.Refresh();
#endif

        //ui sprite 바꿔주기
        saveObject.GetComponent<Image>().sprite = sprite;
    }

    void CircleMask(Texture2D texture)
    {
        width = texture.width;
        height = texture.height;
        Color[] pixels = texture.GetPixels(); //픽셀 가져옴
        center = new Vector2(width / 2, height / 2); //가운데 가져오기
        radius = width / 2; //반지름 가져오기

        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                //반지름보다 길이가 길면 배경이므로 투명화를 시켜야함
                float dist = Vector2.Distance(new Vector2(x, y), center);
                if (dist > radius)
                {
                    int index = y * width + x; //배경 위치
                    pixels[index].a = 0;
                }
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
    }
}
