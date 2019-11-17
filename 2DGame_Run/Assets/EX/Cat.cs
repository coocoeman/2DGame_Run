using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;

public class Cat : MonoBehaviour
{
    #region
    [Header("角色屬性")]
    [Tooltip("角色名稱")]public string catName ;
    [Range(0.1f,5.5f),Tooltip("移動速度")]public float speed = 0.1f ;
    [Range(1, 5),Tooltip("跳躍次數")]public int jumpNumder = 2;
    [Tooltip("跳躍高度")]public float jump = 2.5f;
    [Tooltip("是否離開地面")]public bool jumpBool;
    public float HP = 10;
    public float 傷害直 = 20;
    private float _HP;
    private int ta;
    public float HHPP;
    private Transform useCamera;
    private Animator animator;
    private CapsuleCollider2D collider2D;
    private Rigidbody2D rigidbody2D;
    private AudioSource audio;
    public AudioClip clipjump, clipslip;
    private SpriteRenderer sprite;
    public Image image;
    public Tilemap tpa;
    public Text T;
    public GameObject end;
    public Text texta, textb, textc ;
    //public int textaa, textbb, textcc;
    public int[] 分數陣列 = new int[4];
    #endregion

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
        collider2D = transform.GetComponent<CapsuleCollider2D>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        useCamera = GameObject.Find("Main Camera").transform;
        audio = transform.GetComponent<AudioSource>();
        sprite = transform.GetComponent<SpriteRenderer>();
        _HP = HP;
    }

    private void Update()
    {
        catMobile();
        cameraMobile();
        血量();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "地板")
        {
            jumpBool = true;
        }
        if (collision.gameObject.name == "道具")
        {
            吃到道具(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "障礙物")
        {
            受傷();
        }
        if (collision.tag == "道具")
        {
            吃(collision);
        }
        if (collision.name == "死")
        {
            HP = 0;
            死亡();
        }
    }

    void 血量()
    {
        HP -= Time.deltaTime * HHPP;
        image.fillAmount = HP / _HP;
        死亡();
    }

    void 吃(Collider2D collision)
    {
        Destroy(collision.gameObject);
        ta++;
        T.text = ta.ToString();
    }

    void 吃到道具(Collision2D collision)
    {
        Vector3 pos = Vector3.zero;
        Vector3 _v3 = Vector3.zero;
        _v3 = collision.contacts[0].point;
        Debug.Log(_v3);
        Vector3 n = collision.contacts[0].normal;
        pos.x = _v3.x - n.x * 0.01f;
        pos.y = _v3.y - n.y * 0.01f;
        tpa.SetTile(tpa.WorldToCell(pos),null);
    }

    void 受傷()
    {
        HP -= 傷害直;
        image.fillAmount = HP/_HP;
        sprite.enabled = false;
        Invoke("閃爍" , 0.2f);
        死亡();

    }
    void 閃爍()
    {
        sprite.enabled = true;
    }

    /// <summary>
    /// 角色移動
    /// </summary>
    void catMobile()
    {
        transform.Translate(Vector3.right* Time.deltaTime*speed);
    }

    /// <summary>
    /// 攝影機移動
    /// </summary>
    void cameraMobile()
    {
        useCamera.Translate(Vector3.right * Time.deltaTime * speed);
    }


    /// <summary>
    /// 動此動此跳跳跳
    /// </summary>
    public void Jump()
    {
        if (animator.GetBool("死亡"))
            return;
        if (jumpBool == true)
        {
            //transform.position = new Vector2(transform.position.x,1);
            animator.SetBool("跳", true);
            rigidbody2D.AddForce(new Vector2(0, jump));
            jumpBool = false;
            audio.PlayOneShot(clipjump);
        }
    }

    public void Slip()
    {
        if (animator.GetBool("死亡"))
            return;
        animator.SetBool("滑", true);
        collider2D.offset = new Vector2(-0.1f, -0.6f);
        collider2D.size = new Vector2(1f, 1f);
        audio.PlayOneShot(clipslip);
    }

    /// <summary>
    /// 重製動作
    /// </summary>
    public void Remake()
    {
        animator.SetBool("跳", false);
        animator.SetBool("滑", false);
        collider2D.offset = new Vector2(-0.1f, -0.1f);
        collider2D.size = new Vector2(1f,2f);
    }

    private void 死亡()
    {
        if (HP <=0)
        {
            speed = 0;
            animator.SetBool("死亡", true);
            END畫面();

        }
    }

    private void END畫面()
    {
        if (end.activeInHierarchy == false)
        {
            end.SetActive(true);
            StartCoroutine(等待計算(ta,0,100,texta));
            int timeA =(int) Time.timeSinceLevelLoad;
            StartCoroutine(等待計算(timeA, 1,100, textb,ta*0.5f));
        }
    }

    private IEnumerator 等待計算(int 得到數量,int 顯示的分數,int 分數加成,Text text,float 等等等 = 0)
    {
        yield return new WaitForSeconds(等等等); 

        for (int i = 0; i <= 得到數量; i++)
        {
            分數陣列[顯示的分數] = i * 分數加成;
            text.text = 分數陣列[顯示的分數].ToString();
            yield return new WaitForSeconds(0.5f); 
        }
        if (顯示的分數 != 2) 分數陣列[2] += 分數陣列[顯示的分數];
        if (顯示的分數 == 1)
        {
            int total = 分數陣列[2] / 100;
            分數陣列[3] = 0;
            StartCoroutine(等待計算(total, 2, 100, textc, 0));
        }
    }

}
