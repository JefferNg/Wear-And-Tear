using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private Waypoints waypoints;

    private int waypointIndex;

    private void Start()
    {
        waypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    private void Update()
    {
        // move character position
        transform.position = Vector2.MoveTowards(transform.position, waypoints.waypoints[waypointIndex].position, speed * Time.deltaTime);

        Vector3 direction = waypoints.waypoints[waypointIndex].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // if character reaches a waypoint marker
        if (Vector2.Distance(transform.position, waypoints.waypoints[waypointIndex].position) < 0.001f)
        {
            // if character reaches the last waypoint marker
            if (waypointIndex == waypoints.waypoints.Length - 1) 
            {
                Summoner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
            }
            waypointIndex++;
        }
    }

}
