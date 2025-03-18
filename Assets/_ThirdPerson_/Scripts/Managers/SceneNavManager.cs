using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavManager : MonoBehaviour
{

    //Singleton
    public static SceneNavManager sceneNavManagerInstance;

    private void Awake()
    {
        if (sceneNavManagerInstance == null)
        {
            sceneNavManagerInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }


}
