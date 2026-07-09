using System.Collections;
using System.IO;
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

    [Header("--- 커스텀 이미지 사전 로드 배열 ---")]
    public Sprite[] customSprites = new Sprite[8];
    [SerializeField] Sprite baseSprite;

    private void Awake()
    {
        Application.targetFrameRate = 60; //프레임 설정

        for (int i = 0; i < 8; i++)
        {
            customSprites[i] = PreloadCustomSprite("CircleObjectImage" + i);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        NextObject();

        sum = 0;
        gameOver = false;

    }

    Sprite PreloadCustomSprite(string fileName) //불러오기
    {
        fileName += ".png";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path)) return baseSprite;

        byte[] imageData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false); //미리 텍스처를 만듦
        tex.LoadImage(imageData);

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 250f);
    }

    private void Update()
    {
        if (gameOver)
        {
            StopAllCoroutines();
        }
    }

    CircleObject GetDongle() //오브젝트 생성 할 수 있도록
    {
        //이펙트 생성
        GameObject instantEffectObject = Instantiate(effectPrefab, effectGroupPos);
        ParticleSystem instantEffect = instantEffectObject.GetComponent<ParticleSystem>();
        //오브젝트 생성
        GameObject instantCircleObj = Instantiate(objectPrefab, objectGroupPos);
        CircleObject instantCircle = instantCircleObj.GetComponent<CircleObject>();
        instantCircle.effect = instantEffect; //이펙트 넣어줌
        return instantCircle;
    }

    void NextObject() //오브젝트 생성
    {
        CircleObject newObject = GetDongle();
        circleObject = newObject;
        circleObject.gameManager = this;
        circleObject.level = Random.Range(0, 3);
        circleObject.gameObject.SetActive(true); //크기 설정 후 보이게 하기
        circleObject.SetLevelAndSprite(circleObject.level);

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext() //다음 오브젝트 생성 코루틴
    {
        while (circleObject != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        NextObject(); //반복
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
