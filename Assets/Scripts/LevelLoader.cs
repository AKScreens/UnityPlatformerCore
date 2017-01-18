using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private bool playerInZone;
    public ParticleSysManagement particleManager;
    public string levelToLoad;

	// Use this for initialization
	void Start () {
        playerInZone = false;
        particleManager = FindObjectOfType<ParticleSysManagement>();
        PlayerPrefs.SetInt("Stage", SceneManager.GetActiveScene().buildIndex);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.E) && playerInZone == true)
        {
            if (SceneManager.GetSceneByName(levelToLoad).Equals(SceneManager.GetSceneByBuildIndex(0)))
                PlayerPrefs.SetInt("Stage", 1);
            SceneManager.LoadSceneAsync(levelToLoad);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            playerInZone = true;
            //particleManager.Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInZone = false;
            //particleManager.Deactivate();
        }
    }
}
