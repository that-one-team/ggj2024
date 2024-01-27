using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject _loader;

    private void Start()
    {
        _loader.SetActive(false);
    }

    public void LoadScreen(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        var request = SceneManager.LoadSceneAsync(sceneId);

        _loader.SetActive(true);
        while (!request.isDone)
        {
            yield return null;
        }
        _loader.SetActive(false);
    }
}
