using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag; //�巡�� ���� Ȯ��
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        isDrag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺 ��ġ(��ũ�� ��ǥ���)

            //x�� ��� ����
            float leftBorder = -4.2f + transform.localScale.x / 2f;
            float rightBorder = 4.2f - transform.localScale.x / 2f;

            //�� ����
            if (mousePos.x < leftBorder)
            {
                mousePos.x = leftBorder;
            }
            else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }
            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f); //��ġ ���콺 ��ġ�� ����
        }
    }

    public void Drag()
    {
        isDrag = true;
    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }
}
