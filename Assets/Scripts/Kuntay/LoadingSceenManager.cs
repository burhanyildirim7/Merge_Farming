using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceenManager : MonoBehaviour
{

    public Slider progressBar;
    public Text progressValueText;
    // Start is called before the first frame update

    private void Start()
    {
        Load(1);
    }

    public void Load(int level)
    {

        StartCoroutine(startLoading(level));

    }

    IEnumerator startLoading(int level)
    {

        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {

            progressBar.value = async.progress;
            progressValueText.text = "%"+((int)((progressBar.value)*112)).ToString();
            yield return null;

        }
    }
}
