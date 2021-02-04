using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenePersistent : MonoBehaviour
{
    int StartingSceneIndex;
    private void Awake()
    {
        int numScenePersistent = FindObjectsOfType<scenePersistent>().Length;
        if (numScenePersistent > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(CurrentSceneIndex != StartingSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
