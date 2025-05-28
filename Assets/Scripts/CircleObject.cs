using System.Collections;
using System.IO;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public GameManager gameManager;
    public ParticleSystem effect;

    public int level;
    public bool isDrag; //드래그 상태 확인
    public bool isMerge; //합칠 때 다른 오브젝트가 방해하지 않도록 잠금 역할


    [SerializeField] Sprite baseSprite;

    Rigidbody2D rigid;
    Animator ani;
    CircleCollider2D circle;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
        isDrag = false;
    }

    private void OnEnable()
    {
        ani.SetInteger("Level", level);
    }

    // Update is called once per frame
    void Update()
    {
        AniSprite();
        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 위치(스크린 좌표계로)

            //x축 경계 설정
            float leftBorder = -4.2f + transform.localScale.x / 2f;
            float rightBorder = 4.2f - transform.localScale.x / 2f;

            //값 고정
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

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f); //위치 마우스 위치로 따라감
        }
    }

    void AniSprite() //각 단계의 sprite
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 0"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage0");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 1"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage1");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 2"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage2");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 3"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage3");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 4"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage4");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 5"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage5");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 6"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage6");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Level 7"))
        {
            this.GetComponent<SpriteRenderer>().sprite = LoadSprite("CircleObjectImage7");
        }
    }

    Sprite LoadSprite(string fileName) //불러오기
    {
        fileName += ".png";

        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path))
        {
            return baseSprite;
        }

        byte[] imageData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2); //미리 텍스처를 만듦
        tex.LoadImage(imageData);

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 250f);
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CircleObject")
        {
            CircleObject other = collision.gameObject.GetComponent<CircleObject>();

            if (level == other.level && !isMerge && !other.isMerge && level < 7) //접촉된 상대와 합쳐질 수 있는 조건
            {
                //자신과 상대편 위치 가져오기
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                //내가 아래에 있거나 상대편과 동일한 높이이고 내가 오른쪽에 있을 경우 상대방은 숨기고 나는 레벨업
                if (meY < otherY || (meY == otherY && meX > otherX))
                {
                    other.Hide(transform.position);
                    LevelUp();
                }
            }
        }
    }

    public void Hide(Vector3 targetPos) //숨기기
    {
        isMerge = true;

        rigid.simulated = false;
        circle.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator HideRoutine(Vector3 targetPos) //상대방쪽으로 이동
    {
        int frameCount = 0;

        while (frameCount < 20) //update처럼 사용
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
            yield return null;
        }

        isMerge = false;
        gameObject.SetActive(false);
    }

    void LevelUp()
    {
        isMerge = true;

        rigid.velocity = Vector2.zero; //이동 속도 0으로
        rigid.angularVelocity = 0; //회전 속도 0으로

        StartCoroutine(LevelUpRoutine());
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        ani.SetInteger("Level", level + 1);
        EffectPlay();

        yield return new WaitForSeconds(0.3f);
        level++; //애니메이션 시간이 있기 때문에 좀 늦게 실질적인 레벨 올려줌

        //gameManager.maxLevel = Mathf.Max(level, gameManager.maxLevel); //인자 값 중 최대값 반환

        isMerge = false;
    }

    void EffectPlay()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.localScale / 2;
        effect.Play();
    }
}
