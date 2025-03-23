using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public GameObject linePrefab;

    LineRenderer lineRenderer;
    List<Vector2> points = new List<Vector2>(); //���콺�� ������

    [SerializeField] GameObject customCircle;
    BoxCollider2D circleCollder;

    Vector2 circleObjCenter;
    Vector2 center;
    [SerializeField] float dist;
    [SerializeField] float radius;

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
        if (Mathf.Abs(dist) <= radius) //�� �ȿ����� �׷��� �� �ֵ���
        {
            if (Input.GetMouseButtonDown(0)) //ù��° ������
            {
                GameObject go = Instantiate(linePrefab);
                lineRenderer = go.GetComponent<LineRenderer>();
                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, points[0]);
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
