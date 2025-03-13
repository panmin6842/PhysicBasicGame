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
        Application.targetFrameRate = 60; //프레임 설정
    }
    // Start is called before the first frame update
    void Start()
    {
        NextObject();
    }

    CircleObject GetDongle() //오브젝트 생성 할 수 있도록
    {
        GameObject instant = Instantiate(objectPrefab, objectGroupPos);
        CircleObject instantObject = instant.GetComponent<CircleObject>();
        return instantObject;
    }

    void NextObject() //오브젝트 생성
    {
        CircleObject newObject = GetDongle();
        circleObject = newObject;
        circleObject.level = Random.Range(0, 8);
        circleObject.gameObject.SetActive(true); //크기 설정 후 보이게 하기

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext() //다음 오브젝트 생성 코루틴
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
        if (circleObject == null) //오브젝트 없으면 그냥 나가기
            return;

        circleObject.Drag();
    }

    public void TouchUp()
    {
        if (circleObject == null)
            return;

        circleObject.Drop();
        circleObject = null; //터치 끝나면 비워둠
    }
}
