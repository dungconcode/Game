using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] waypoints;
    private int currentMaypointIndex = 0;
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentMaypointIndex].transform.position, transform.position) < .1f)
        {
            currentMaypointIndex++;
            if(currentMaypointIndex >= waypoints.Length)
            {
                currentMaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentMaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
