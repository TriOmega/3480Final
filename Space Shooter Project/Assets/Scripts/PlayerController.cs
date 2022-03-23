using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

}//END CLASS Boundary

public class PlayerController : MonoBehaviour
{


    #region Components


    [Header("Components")]
    public float speed;
    [SerializeField] private float tilt;
    [SerializeField] private Boundary boundary;
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawn;
    public float fireRate;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicClipOne;

    private Rigidbody rb;
    private float nextFire;


    #endregion Components


    #region Methods


    //-------------------------------//
    private void Start()
    //-------------------------------//
    {
        rb = GetComponent<Rigidbody>();

    }//END Start

    //-------------------------------//
    void Update()
    //-------------------------------//
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }

    }//END Update

    //-------------------------------//
    void FixedUpdate()
    //-------------------------------//
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }//END FixedUpdate


    #endregion Methods


}//END CLASS PlayerController
