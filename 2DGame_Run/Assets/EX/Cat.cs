using UnityEngine;
using UnityEngine.UI;

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

    private Transform useCamera;
    private Animator animator;
    private CapsuleCollider2D collider2D;
    private Rigidbody2D rigidbody2D;
    private AudioSource audio;
    public AudioClip clipjump, clipslip;
    private SpriteRenderer sprite;
    public Image image;
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

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "地板")
        {
            jumpBool = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        受傷();
    }

    void 受傷()
    {
        HP -= 傷害直;
        image.fillAmount = HP/_HP;
        sprite.enabled = false;
        Invoke("閃爍" , 0.2f);

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
        if (jumpBool == true)
        {
            animator.SetBool("跳", true);
            rigidbody2D.AddForce(new Vector2(0, jump));
            jumpBool = false;
            audio.PlayOneShot(clipjump);
        }
    }

    public void Slip()
    {
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
}
