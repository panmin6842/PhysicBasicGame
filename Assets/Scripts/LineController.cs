using System.Collections;
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
    BoxCollider2D circleCollder;

    Vector2 circleObjCenter;
    Vector2 center;
    [SerializeField] float dist;
    [SerializeField] float radius;
    float lineWidth;

    Color color;

    private void OnEnable()
    {
        lineColor = FindObjectOfType<LineColor>();
        color = lineColor.GetColor();
        lineWidth = lineColor.sizeSlider.value;
    }

    // Start is called before the first frame update
    void Start()
    {
        circleCollder = customCircle.GetComponent<BoxCollider2D>();
        center = customCircle.transform.position; //중심 좌표
        circleObjCenter = center;
        radius = circleCollder.bounds.size.x / 2; //반지름 길이
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), circleObjCenter); //원 중심 좌표과 마우스 좌표 사이의 거리 구함
        LineDraw();
    }

    void LineDraw()
    {
        if (Mathf.Abs(dist) <= radius - (lineWidth / 2)) //원 안에서만 그려질 수 있도록
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

                if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f) //움직임이 있어야 추가되도록 함
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
}
