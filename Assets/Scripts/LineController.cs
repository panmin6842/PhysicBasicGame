using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
    public GameObject linePrefab;

    LineRenderer lineRenderer;
    LineColor lineColor;
    List<Vector2> points = new List<Vector2>(); //마우스의 포지션

    [SerializeField] GameObject customCircle;
    [SerializeField] Sprite baseSprite;
    [SerializeField] Image colorMark;
    BoxCollider2D circleCollder;

    Vector2 circleObjCenter;
    Vector2 center;
    [SerializeField] float dist;
    [SerializeField] float radius;
    float lineWidth;

    Color color;

    bool pen;
    bool paint;
    bool eraser;
    [SerializeField] GameObject eraserSizeSlider;

    GameObject[] lines;

    private void OnEnable()
    {
        lineColor = FindObjectOfType<LineColor>();
        color = lineColor.selectedColor;
        lineWidth = lineColor.sizeSlider.value;
        colorMark.color = lineColor.selectedColor;
        points.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        circleCollder = customCircle.GetComponent<BoxCollider2D>();
        center = customCircle.transform.position; //중심 좌표
        circleObjCenter = center;
        radius = circleCollder.bounds.size.x / 2; //반지름 길이

        color = Color.black;
        colorMark.color = Color.black;

        pen = true;
        paint = false;
        eraser = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), circleObjCenter); //원 중심 좌표과 마우스 좌표 사이의 거리 구함
        if (pen && !paint && !eraser)
            LineDraw();
        else if (!pen && paint && !eraser)
            PaintDraw();
        else if (!pen && !paint && eraser)
            EraserDraw();
    }

    //그리기
    void LineDraw()
    {
        if (Mathf.Abs(dist) <= radius + 2)
        {
            if (Input.GetMouseButtonDown(0)) //첫번째 포지션
            {
                GameObject go = Instantiate(linePrefab);
                lineRenderer = go.GetComponent<LineRenderer>();
                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, points[0]);


                //선 굵기
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
                //선 색
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
            }
            else if (Input.GetMouseButton(0)) //마우스 누를 동안 그려지게
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (pos != null)
                {
                    if (Mathf.Abs(Vector2.Distance(points[points.Count - 1], pos)) > 0.1f) //움직임이 있어야 추가되도록 함
                    {
                        points.Add(pos);
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0)) //마우스 때면 리스트 초기화
            {
                points.Clear();
            }
        }
    }

    //페인트로 색 채우기
    void PaintDraw()
    {
        if (Mathf.Abs(dist) <= radius)
        {
            if (Input.GetMouseButtonDown(0))
            {
                customCircle.GetComponent<SpriteRenderer>().sprite = baseSprite;
                int lineLength = GameObject.FindGameObjectsWithTag("Line").Length;
                lines = GameObject.FindGameObjectsWithTag("Line");
                if (lineLength > 0) //그려져있는 선 다 없애기
                {
                    for (int i = 0; i < lineLength; i++)
                    {
                        Destroy(lines[i]);
                    }

                }

                customCircle.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    //간단한 지우개 기능
    void EraserDraw()
    {
        if (Mathf.Abs(dist) <= radius + 2)
        {
            if (Input.GetMouseButtonDown(0)) //첫번째 포지션
            {
                GameObject go = Instantiate(linePrefab);
                lineRenderer = go.GetComponent<LineRenderer>();
                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, points[0]);
                //선 굵기
                lineRenderer.startWidth = eraserSizeSlider.GetComponent<Slider>().value;
                lineRenderer.endWidth = eraserSizeSlider.GetComponent<Slider>().value;
                //선 색
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
            }
            else if (Input.GetMouseButton(0)) //마우스 누를 동안 그려지게
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Mathf.Abs(Vector2.Distance(points[points.Count - 1], pos)) > 0.1f) //움직임이 있어야 추가되도록 함
                {
                    points.Add(pos);
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
                }
            }
            else if (Input.GetMouseButtonUp(0)) //마우스 때면 리스트 초기화
            {
                points.Clear();
            }
        }
    }

    //그리는도구 버튼 클릭시
    public void PenClick()
    {
        pen = true;
        paint = false;
        eraser = false;
        eraserSizeSlider.SetActive(false);
    }

    public void PaintClick()
    {
        paint = true;
        pen = false;
        eraser = false;
        eraserSizeSlider.SetActive(false);
    }

    public void EraserClick()
    {
        eraser = true;
        pen = false;
        paint = false;
        eraserSizeSlider.SetActive(true);
    }
}
