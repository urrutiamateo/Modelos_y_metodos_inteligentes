using UnityEngine;

public class Arrive : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float slowRadius = 2f;
    public Transform target;

    public SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;

        float targetSpeed = maxSpeed;
        if (distance < slowRadius)
        {
            targetSpeed = maxSpeed * (distance / slowRadius);
        }

        steering.velocity = direction.normalized * targetSpeed;
        steering.rotation = 0;

        return steering;
    }
}