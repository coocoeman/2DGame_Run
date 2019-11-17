using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text t等待;
    public Image i等待;

    public void RT()
    {
        SceneManager.LoadScene("asd");
    }
    public void END()
    {
        Application.Quit();
    }

    public void 等待()
    {
        StartCoroutine(等八());
    }
    IEnumerator 等八()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("asd");
        ao.allowSceneActivation = false;

        while (ao.isDone == false)
        {
            t等待.text = ao.progress/0.9f*100 + "/100";
            i等待.fillAmount = ao.progress /0.9f;
            yield return null;
            if (ao.progress == 0.9f && Input.anyKey)
            {
                ao.allowSceneActivation = true;
            }
        }
    }
}
