using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
    public GameObject linePrefab;

    LineRenderer lineRenderer;
    LineColor lineColor;
    List<Vector2> points = new List<Vector2>(); //���콺�� ������

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
        center = customCircle.transform.position; //�߽� ��ǥ
        circleObjCenter = center;
        radius = circleCollder.bounds.size.x / 2; //������ ����
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), circleObjCenter); //�� �߽� ��ǥ�� ���콺 ��ǥ ������ �Ÿ� ����
        LineDraw();
    }

    void LineDraw()
    {
        if (Mathf.Abs(dist) <= radius - (lineWidth / 2)) //�� �ȿ����� �׷��� �� �ֵ���
        {
            if (Input.GetMouseButtonDown(0)) //ù��° ������
            {
                GameObject go = Instantiate(linePrefab);
                lineRenderer = go.GetComponent<LineRenderer>();
                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, points[0]);
                //�� ����
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
                //�� ��
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
            }
            else if (Input.GetMouseButton(0)) //���콺 ���� ���� �׷�����
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f) //�������� �־�� �߰��ǵ��� ��
                {
                    points.Add(pos);
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
                }
            }
            else if (Input.GetMouseButtonUp(0)) //���콺 ���� ����Ʈ �ʱ�ȭ
            {
                points.Clear();
            }
        }
    }
}
