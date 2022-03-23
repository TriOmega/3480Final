using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

}//END Mover
