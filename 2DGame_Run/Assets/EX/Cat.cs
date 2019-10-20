using UnityEngine;

public class Cat : MonoBehaviour
{
    #region
    [Header("角色屬性")]
    [Tooltip("角色名稱")]public string catName ;
    [Range(0.1f,5.5f),Tooltip("移動速度")]public float speed = 0.1f ;
    [Range(1, 5),Tooltip("跳躍次數")]public int jumpNumder = 2;
    [Range(1f, 3.5f), Tooltip("跳躍高度")]public float jump = 2.5f;
    [Tooltip("是否離開地面")]public bool jumpBool;

    [Tooltip("角色與攝影機")]public Transform cat ,useCamera;

    

    #endregion
    private void Start()
    {
        //不要問我會怕();
    }
    void 不要問我會怕()
    {
        Debug.Log(catName);
        Debug.Log(speed);
        Debug.Log(jumpNumder);
        Debug.Log(jump);
        Debug.Log(jumpBool);
    }

    private void Update()
    {
        catMobile();
        cameraMobile();

    }

    /// <summary>
    /// 角色移動
    /// </summary>
    void catMobile()
    {
        cat.Translate(Vector3.right* Time.deltaTime*speed);
    }

    /// <summary>
    /// 攝影機移動
    /// </summary>
    void cameraMobile()
    {
        useCamera.Translate(Vector3.right * Time.deltaTime * speed);
    }


    /// <summary>
    /// 
    /// </summary>
    public void Jump()
    {
        //cat.Translate(Vector3.up*jump/Time.deltaTime);
    }

}
