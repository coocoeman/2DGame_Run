using UnityEngine;

public class ase : MonoBehaviour
{
    [Header("角色屬性")]
    [Range(1,3),Tooltip("跳次數限制")]public int 跳次數;
    [Range(1, 10),Tooltip("跑的速度限制")] public float 跑的速度;
    [Range(1, 2.5f)] public float 跳的高度;

    public Transform 角色,攝影機;
    
    private void Update()
    {
        跑跑跑();
        攝影機跑跑跑();
    }

    void 跑跑跑()
    {
        角色.Translate(跑的速度 * Time.deltaTime, 0, 0);
    }

    void 攝影機跑跑跑()
    {
        攝影機.Translate(跑的速度 * Time.deltaTime, 0, 0);
    }

}
