using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Begin()
    {
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        int sceneIndex = PlayerPrefs.GetInt("Stage", 1);
        SceneManager.LoadScene(sceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }
}
