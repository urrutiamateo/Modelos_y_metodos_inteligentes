using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Seek : MonoBehaviour
{

    public float maxSpeed = 5f;
    public Transform target;

    public SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        Vector2 direction = target.position - transform.position;

        steering.velocity = direction.normalized * maxSpeed;
        steering.rotation = 0;

        return steering;
    }
}
