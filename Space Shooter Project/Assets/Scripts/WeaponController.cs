 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject shot;

    [SerializeField] private Transform shotSpawn;

    [SerializeField] private float fireRate;
    [SerializeField] private float delay;

    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip musicClipOne;

    //--------------------------//
    void Start()
    //--------------------------//
    {
        InvokeRepeating ("Fire", delay, fireRate) ;
        musicSource = GetComponent<AudioSource>();

    }//END Start

    //--------------------------//
    void Fire ()
    //--------------------------//
    {
        Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

        musicSource.clip = musicClipOne;
        musicSource.Play();
    }//END Fire

}
