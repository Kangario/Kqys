using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRaycast : MonoBehaviour
{
    public Vector3 Direction;
    private RaycastHit hit;
    private Transform transform;

    public Vector3 Position => hit.point;
    public Vector3 Normal => hit.normal;

    private void Awake()
    {
        transform = base.transform; 
    }

    private void Update()
    {
        var ray = new Ray(transform.position, Direction);
        Physics.Raycast(ray, out hit);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(gameObject.transform.position, 0.25f);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(gameObject.transform.position, Position);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(Position, 0.15f);
    }
}
