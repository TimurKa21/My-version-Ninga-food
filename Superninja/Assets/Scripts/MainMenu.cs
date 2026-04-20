using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float _loadDelay = 1f;
    public void LoadGame()
    {
        StartCoroutine(DelayLoad());
    }

    private IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(_loadDelay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
