using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CircleObject circleObject;
    public GameObject objectPrefab;
    public Transform objectGroupPos;

    private void Awake()
    {
        Application.targetFrameRate = 60; //������ ����
    }
    // Start is called before the first frame update
    void Start()
    {
        NextObject();
    }

    CircleObject GetDongle() //������Ʈ ���� �� �� �ֵ���
    {
        GameObject instant = Instantiate(objectPrefab, objectGroupPos);
        CircleObject instantObject = instant.GetComponent<CircleObject>();
        return instantObject;
    }

    void NextObject() //������Ʈ ����
    {
        CircleObject newObject = GetDongle();
        circleObject = newObject;
        circleObject.level = Random.Range(0, 8);
        circleObject.gameObject.SetActive(true); //ũ�� ���� �� ���̰� �ϱ�

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext() //���� ������Ʈ ���� �ڷ�ƾ
    {
        while(circleObject != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        NextObject();
    }

    public void TouchDown()
    {
        if (circleObject == null) //������Ʈ ������ �׳� ������
            return;

        circleObject.Drag();
    }

    public void TouchUp()
    {
        if (circleObject == null)
            return;

        circleObject.Drop();
        circleObject = null; //��ġ ������ �����
    }
}
