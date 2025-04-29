using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public int currentWP = 0;

    //the array of waypoints in order of visiting
    //can be made up of any game objects
    public GameObject[] wps;

    public int speed = 5;
    public int rotationSpeed = 2;

    //how close to get to the waypoint to consider having reached it
    public float accuracy = 5.0f;

    void Update()
    {
        if (wps.Length == 0) return;
        if (Vector3.Distance(wps[currentWP].transform.position,
            transform.position) < accuracy)
        {
            currentWP++;
            if (currentWP >= wps.Length)
            {
                // what to do next when reach the last WP?

                // 1) return to the first WP (periodic motion) 
                currentWP = 0;

                // 2) Orbit around the the last WP
                //currentWP = wps.Length - 1;

                // 3) Stop at the last WP
                //transform.position = wps[wps.Length - 1].transform.position;
                //speed = 0;
                //rotationSpeed = 0;
            }
        }

        //Rotate towards target
        Vector3 direction = wps[currentWP].transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            rotationSpeed * Time.deltaTime);

        // Move in the front dirction
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
