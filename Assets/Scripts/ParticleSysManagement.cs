using UnityEngine;
using System.Collections;

public class ParticleSysManagement : MonoBehaviour {

    ParticleSystem particles;
    

    public void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        //Debug.Log(particles.rateOverTime);
    } 

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            particles.Play();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            particles.Stop();
    } 
}
