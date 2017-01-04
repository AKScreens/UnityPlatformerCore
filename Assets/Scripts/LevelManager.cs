using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {


    public GameObject currentCheckpoint;
    private PlayerController2d player;
    private GameObject breakableParent;
    public GameObject[] breakables;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController2d>();
        breakableParent = GameObject.Find("Breakables");
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        player.transform.position = currentCheckpoint.transform.position;
        for (int i = 0; i < breakableParent.transform.childCount; i++)
        {
            breakableParent.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
