using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private float boost = 1.4f;
    [SerializeField] private float rateup = .8f;
    [SerializeField] private float duration;
    [SerializeField] private GameObject pickupEffect;


    #endregion Components


    #region Methods


    //----------------------------------//
    void OnTriggerEnter (Collider other)
    //----------------------------------//
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine (IPickup(other));
        }

    }//END OnTriggerEnter

    //----------------------------------//
    IEnumerator IPickup (Collider player)
    //----------------------------------//
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        PlayerController playerController = player.GetComponent<PlayerController>();

        playerController.speed *= boost;

        playerController.fireRate *= rateup;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerController.speed /= boost;
        playerController.fireRate /= rateup;

        Destroy(gameObject);

    }//END IPickup


    #endregion Methods


}//END CLASS PowerUp
