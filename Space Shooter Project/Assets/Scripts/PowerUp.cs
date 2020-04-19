using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float boost = 1.4f;
    public float rateup = .8f;
    public float duration;
    public GameObject pickupEffect;

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine (Pickup(other));
        }
    }

    IEnumerator Pickup (Collider player)
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
    }
}
