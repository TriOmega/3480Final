using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{


    #region Components


    [SerializeField] private float dodgeChance;
    [SerializeField] private float smoothing;
    [SerializeField] private float tilt;

    [SerializeField] private Vector2 startWait;
    [SerializeField] private Vector2 maneuverTime;
    [SerializeField] private Vector2 maneuverWait;

    [SerializeField] private Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;


    #endregion Components


    #region Methods


    //--------------------------//
    void Start()
    //--------------------------//
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(IEvade());
    }//END Start

    //--------------------------//
    IEnumerator IEvade()
    //--------------------------//
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodgeChance) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }//END IEvade

    //--------------------------//
    void FixedUpdate()
    //--------------------------//
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

    }//END FIxedUpdate


    #endregion Methods

}//END CLASS Evasive Maneuver

