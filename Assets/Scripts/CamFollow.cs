using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    public Transform follow;
    public Vector3 offset = Vector3.back * 10f;
    public float lerpConst = 0.1f;

	// Use this for initialization
	void Start () {
        transform.position = follow.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, follow.position + offset, lerpConst);
	}
}
