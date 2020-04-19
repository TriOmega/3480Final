 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;
    public AudioSource musicSource;
    public AudioClip musicClipOne;

    void Start()
    {
        InvokeRepeating ("Fire", delay, fireRate) ;
        musicSource = GetComponent<AudioSource>();
    }

    void Fire ()
    {
        Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

}
