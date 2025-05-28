using System.Collections;
using System.IO;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public GameManager gameManager;
    public ParticleSystem effect;

    public int level;
    public bool isDrag; //�巡�� ���� Ȯ��
    public bool isMerge; //��ĥ �� �ٸ� ������Ʈ�� �������� �ʵ��� ��� ����


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

    void AniSprite() //�� �ܰ��� sprite
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

    Sprite LoadSprite(string fileName) //�ҷ�����
    {
        fileName += ".png";

        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path))
        {
            return baseSprite;
        }

        byte[] imageData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2); //�̸� �ؽ�ó�� ����
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

            if (level == other.level && !isMerge && !other.isMerge && level < 7) //���˵� ���� ������ �� �ִ� ����
            {
                //�ڽŰ� ����� ��ġ ��������
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                //���� �Ʒ��� �ְų� ������ ������ �����̰� ���� �����ʿ� ���� ��� ������ ����� ���� ������
                if (meY < otherY || (meY == otherY && meX > otherX))
                {
                    other.Hide(transform.position);
                    LevelUp();
                }
            }
        }
    }

    public void Hide(Vector3 targetPos) //�����
    {
        isMerge = true;

        rigid.simulated = false;
        circle.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator HideRoutine(Vector3 targetPos) //���������� �̵�
    {
        int frameCount = 0;

        while (frameCount < 20) //updateó�� ���
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

        rigid.velocity = Vector2.zero; //�̵� �ӵ� 0����
        rigid.angularVelocity = 0; //ȸ�� �ӵ� 0����

        StartCoroutine(LevelUpRoutine());
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        ani.SetInteger("Level", level + 1);
        EffectPlay();

        yield return new WaitForSeconds(0.3f);
        level++; //�ִϸ��̼� �ð��� �ֱ� ������ �� �ʰ� �������� ���� �÷���

        //gameManager.maxLevel = Mathf.Max(level, gameManager.maxLevel); //���� �� �� �ִ밪 ��ȯ

        isMerge = false;
    }

    void EffectPlay()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.localScale / 2;
        effect.Play();
    }
}
