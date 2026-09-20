using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public float lookAhead = 1.2f;
    public float radius = 0.3f;
    public float avoidStrength = 1.5f;
    public LayerMask obstacleMask;

    public SteeringOutput GetSteering(Vector2 desiredVelocity)
    {
        SteeringOutput steering = new SteeringOutput();
        steering.velocity = desiredVelocity;
        steering.rotation = 0;

        if (desiredVelocity.sqrMagnitude < 0.0001f) return steering;

        Vector2 dir = desiredVelocity.normalized;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, dir, lookAhead, obstacleMask);

        if (hit.collider != null)
        {
            Vector2 tangent = Vector2.Perpendicular(hit.normal);
            if (Vector2.Dot(tangent, dir) < 0) tangent = -tangent;

            float closeness = 1f - (hit.distance / lookAhead);
            Vector2 newDir = (tangent + hit.normal * avoidStrength * closeness).normalized;

            steering.velocity = newDir * desiredVelocity.magnitude;
        }

        return steering;
    }
}