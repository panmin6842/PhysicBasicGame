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
    [SerializeField] GameObject customZone;
    public Color selectedColor;
    public Slider sizeSlider;

    [SerializeField] GameObject lineController;

    Vector2 sizeOfPalette;
    CircleCollider2D paletteCollider;

    float radius;
    bool buttonClick;

    bool paletteAppear;
    // Start is called before the first frame update
    void Start()
    {
        paletteCollider = circlePalette.GetComponent<CircleCollider2D>();
        sizeOfPalette = new Vector2(circlePalette.GetComponent<RectTransform>().rect.width,
            circlePalette.GetComponent<RectTransform>().rect.height);

        radius = sizeOfPalette.x * 0.5f;
        buttonClick = false;
        paletteAppear = true;
    }

    // Update is called once per frame
    void Update()
    {
        //선 굵기
        testLineRenderer.startWidth = sizeSlider.value;
        testLineRenderer.endWidth = sizeSlider.value;
        //선 색
        testLineRenderer.startColor = selectedColor;
        testLineRenderer.endColor = selectedColor;
    }

    void SelectedColor()
    {

        RectTransform paletteRect = GetComponent<RectTransform>();
        Vector2 localPoint;

        //스크린 좌표를 RectTransform 좌표계의 Local Point로 변환시켜줌
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        paletteRect, //변환 기준 좌표계의 Canvas RectTransform
        Input.mousePosition, //출력하고자 하는 스크린 좌표
        null, //스크린 좌표와 연관된 카메라 
        out localPoint //변환한 좌표를 저장할 변수
        );

        //picker가 원 범위 안에 있게
        Vector2 clamped = Vector2.ClampMagnitude(localPoint, radius);

        // picker를 로컬 좌표 기준으로 배치
        picker.GetComponent<RectTransform>().anchoredPosition = clamped;

        if (!buttonClick)
            selectedColor = GetColor();
    }

    public void MousePointDown()
    {
        SelectedColor();
        buttonClick = false;

    }

    public void MouseDrag()
    {
        SelectedColor();
    }

    public Color GetColor()
    {
        RectTransform patetteRect = circlePalette.GetComponent<RectTransform>();
        RectTransform pickerRect = picker.GetComponent<RectTransform>();

        Vector2 relativePos = pickerRect.anchoredPosition + (patetteRect.rect.size * 0.5f); //부모기준 로컬 좌표

        Vector2 normalized = new Vector2(Mathf.Clamp01(relativePos.x / patetteRect.rect.width),
                                         Mathf.Clamp01(relativePos.y / patetteRect.rect.height));

        //Debug.Log(normalized);
        Texture2D texture = circlePalette.mainTexture as Texture2D;
        Color circularSelectedColor = texture.GetPixelBilinear(normalized.x, normalized.y);

        return circularSelectedColor;
    }

    //검정색, 흰색 버튼
    public void BlackColor()
    {
        selectedColor = Color.black;
        buttonClick = true;
    }

    public void WhiteColor()
    {
        selectedColor = Color.white;
        buttonClick = true;
    }

    //색 창 열고 닫기
    public void ColorZoneAppear()
    {
        lineController.GetComponent<LineController>().enabled = false;
        customZone.SetActive(false);
        circlePalette.gameObject.SetActive(true);
        picker.gameObject.SetActive(true);
        bg.SetActive(true);
        testObj.SetActive(true);
        sizeSlider.gameObject.SetActive(true);
        paletteAppear = true;
    }

    public void ColorZoneClose()
    {
        lineController.GetComponent<LineController>().enabled = true;
        circlePalette.gameObject.SetActive(false);
        picker.gameObject.SetActive(false);
        bg.SetActive(false);
        testObj.SetActive(false);
        sizeSlider.gameObject.SetActive(false);
        customZone.SetActive(true);
        paletteAppear = false;
    }
}
