using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManag : MonoBehaviour
{
    public TextMeshProUGUI textLoad; 
    public void Load(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    IEnumerator LoadSceneAsync(int index)
    {       
        yield return new WaitForSeconds(2.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        
        while (!operation.isDone)
        {
            if (textLoad != null)
                textLoad.text = $"Loading {operation.progress}!";

            yield return null;
        }        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
