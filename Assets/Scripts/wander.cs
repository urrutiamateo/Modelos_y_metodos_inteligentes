using UnityEngine;

public class Wander : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float changeDirectionTime = 2f;

    private float timer;
    private Vector2 wanderDirection;

    void Start()
    {
        ChooseNewDirection();
    }

    public SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ChooseNewDirection();
        }

        steering.velocity = wanderDirection * maxSpeed;

        steering.rotation = 0;

        return steering;
    }

    public void ChooseNewDirection()
    {
        timer = changeDirectionTime;

        wanderDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}
