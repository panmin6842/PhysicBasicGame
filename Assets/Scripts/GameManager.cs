using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CircleObject circleObject;
    public GameObject objectPrefab;
    public Transform objectGroupPos;
    public GameObject effectPrefab;
    public Transform effectGroupPos;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    public GameObject gameOverPage;
    public bool gameOver;

    public int maxLevel;
    public int sum;

    private void Awake()
    {
        Application.targetFrameRate = 60; //������ ����
    }
    // Start is called before the first frame update
    void Start()
    {
        NextObject();

        sum = 0;
        gameOver = false;
    }

    private void Update()
    {
        if (gameOver)
        {
            StopAllCoroutines();
        }
    }

    CircleObject GetDongle() //������Ʈ ���� �� �� �ֵ���
    {
        //����Ʈ ����
        GameObject instantEffectObject = Instantiate(effectPrefab, effectGroupPos);
        ParticleSystem instantEffect = instantEffectObject.GetComponent<ParticleSystem>();
        //������Ʈ ����
        GameObject instantCircleObj = Instantiate(objectPrefab, objectGroupPos);
        CircleObject instantCircle = instantCircleObj.GetComponent<CircleObject>();
        instantCircle.effect = instantEffect; //����Ʈ �־���
        return instantCircle;
    }

    void NextObject() //������Ʈ ����
    {
        CircleObject newObject = GetDongle();
        circleObject = newObject;
        circleObject.gameManager = this;
        circleObject.level = Random.Range(0, 3);
        circleObject.gameObject.SetActive(true); //ũ�� ���� �� ���̰� �ϱ�

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext() //���� ������Ʈ ���� �ڷ�ƾ
    {
        while (circleObject != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        NextObject(); //�ݺ�
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
