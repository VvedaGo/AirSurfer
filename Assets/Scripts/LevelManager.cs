using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int _countScene;
    private void Start()
    {
        _countScene = SceneManager.sceneCountInBuildSettings;
    }
    public void Refresh()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Next()
    {
        int numberNextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(numberNextScene);
        Debug.Log(_countScene);
        if (numberNextScene == _countScene)
        {
            numberNextScene = 0;
        }
        SceneManager.LoadScene(numberNextScene);
    }

}
