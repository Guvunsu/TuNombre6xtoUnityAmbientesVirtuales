using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    Animator animator;
    public string localSceneName;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SceneChange()
    {
        Debug.Log("Se esta cambiando la escena");
        SceneManager.LoadScene(localSceneName);
    }

    public void SceneChangerWithFade(string sceneName)
    {
        animator.SetTrigger("FadeIn");
        localSceneName = sceneName;
        SceneChange();
    }

}
