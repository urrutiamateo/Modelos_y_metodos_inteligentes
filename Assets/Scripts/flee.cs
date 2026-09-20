using UnityEngine;

public class Flee : MonoBehaviour
{
    public float maxSpeed = 5f;
    public Transform target;

    public SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        Vector2 direction = transform.position - target.position;

        steering.velocity = direction.normalized * maxSpeed;
        steering.rotation = 0;

        return steering;
    }
}
