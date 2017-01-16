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
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.E) && playerInZone == true)
        {
            SceneManager.LoadSceneAsync(levelToLoad);
        }
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
