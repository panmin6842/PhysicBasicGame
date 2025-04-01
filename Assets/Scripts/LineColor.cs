using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Drawing;

public class LineColor : MonoBehaviour
{
    [SerializeField] Image circlePalette;
    [SerializeField] Image picker;
    [SerializeField] GameObject bg;
    [SerializeField] LineRenderer testLineRenderer;
    [SerializeField] GameObject testObj;
    public Color selectedColor;
    public Slider sizeSlider;

    [SerializeField] GameObject lineController;

    Vector2 sizeOfPalette;
    CircleCollider2D paletteCollider;

    float radius;
    // Start is called before the first frame update
    void Start()
    {
        paletteCollider = circlePalette.GetComponent<CircleCollider2D>();
        sizeOfPalette = new Vector2(circlePalette.GetComponent<RectTransform>().rect.width,
            circlePalette.GetComponent<RectTransform>().rect.height);

        radius = sizeOfPalette.x * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            lineController.GetComponent<LineController>().enabled = true;
            circlePalette.gameObject.SetActive(false);
            picker.gameObject.SetActive(false);
            bg.SetActive(false);
            testObj.SetActive(false);
        }

        //선 굵기
        testLineRenderer.startWidth = sizeSlider.value;
        testLineRenderer.endWidth = sizeSlider.value;
        //선 색
        testLineRenderer.startColor = selectedColor;
        testLineRenderer.endColor = selectedColor;
    }

    void SelectedColor()
    {
        //picker가 palette 내에서만 선택되도록 ClampMagnitude로 offset의 크기를 반지를 미만으로 제한
        Vector3 offset = Input.mousePosition - transform.position;
        Vector3 diff = Vector3.ClampMagnitude(offset, radius);
        picker.transform.position = transform.position + diff;

        selectedColor = GetColor();
    }

    public void MousePointDown()
    {
        SelectedColor();
    }

    public void MouseDrag()
    {
        SelectedColor();
    }
    
    public Color GetColor()
    {
        Vector2 circlePalettePos = circlePalette.transform.position; //팔레트 포지션 구하기
        Vector2 pickerPos = picker.transform.position; //선택 포지션 구하기

        Vector2 pos = pickerPos - circlePalettePos + sizeOfPalette * 0.5f; //팔레트 내에서의 상대 좌표
        
        Vector2 normalized = new Vector2(Mathf.Clamp01((pos.x) / (circlePalette.GetComponent<RectTransform>().rect.width)),
                                         Mathf.Clamp01((pos.y) / (circlePalette.GetComponent<RectTransform>().rect.height))); //정규화된 좌표로 변환

        Debug.Log(normalized);
        Texture2D texture = circlePalette.mainTexture as Texture2D;
        Color circularSelectedColor = texture.GetPixelBilinear(normalized.x, normalized.y);

        return circularSelectedColor;
    }
}
