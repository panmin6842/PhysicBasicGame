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
        center = customCircle.transform.position; //�߽� ��ǥ
        circleObjCenter = center;
        radius = circleCollder.bounds.size.x / 2; //������ ����

        color = Color.black;
        colorMark.color = Color.black;

        pen = true;
        paint = false;
        eraser = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), circleObjCenter); //�� �߽� ��ǥ�� ���콺 ��ǥ ������ �Ÿ� ����
        if (pen && !paint && !eraser)
            LineDraw();
        else if (!pen && paint && !eraser)
            PaintDraw();
        else if (!pen && !paint && eraser)
            EraserDraw();
    }

    //�׸���
    void LineDraw()
    {
        if (Mathf.Abs(dist) <= radius + 2)
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

                if (pos != null)
                {
                    if (Mathf.Abs(Vector2.Distance(points[points.Count - 1], pos)) > 0.1f) //�������� �־�� �߰��ǵ��� ��
                    {
                        points.Add(pos);
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0)) //���콺 ���� ����Ʈ �ʱ�ȭ
            {
                points.Clear();
            }
        }
    }

    //����Ʈ�� �� ä���
    void PaintDraw()
    {
        if (Mathf.Abs(dist) <= radius)
        {
            if (Input.GetMouseButtonDown(0))
            {
                customCircle.GetComponent<SpriteRenderer>().sprite = baseSprite;
                int lineLength = GameObject.FindGameObjectsWithTag("Line").Length;
                lines = GameObject.FindGameObjectsWithTag("Line");
                if (lineLength > 0) //�׷����ִ� �� �� ���ֱ�
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

    //������ ���찳 ���
    void EraserDraw()
    {
        if (Mathf.Abs(dist) <= radius + 2)
        {
            if (Input.GetMouseButtonDown(0)) //ù��° ������
            {
                GameObject go = Instantiate(linePrefab);
                lineRenderer = go.GetComponent<LineRenderer>();
                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, points[0]);
                //�� ����
                lineRenderer.startWidth = eraserSizeSlider.GetComponent<Slider>().value;
                lineRenderer.endWidth = eraserSizeSlider.GetComponent<Slider>().value;
                //�� ��
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
            }
            else if (Input.GetMouseButton(0)) //���콺 ���� ���� �׷�����
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Mathf.Abs(Vector2.Distance(points[points.Count - 1], pos)) > 0.1f) //�������� �־�� �߰��ǵ��� ��
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

    //�׸��µ��� ��ư Ŭ����
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
