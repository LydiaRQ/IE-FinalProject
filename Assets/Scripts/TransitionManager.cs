using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public string nextSceneName = "NextSceneNameHere"; // ФЪ Inspector МоєГ
    private bool canTransition = false;

    void Update()
    {
        if (canTransition && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void EnableTransition()
    {
        canTransition = true;
    }
}
