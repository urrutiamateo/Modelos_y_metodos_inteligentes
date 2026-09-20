using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public AIState reactionState = AIState.Wander;
    public float detectionDistance = 5f;
    public float loseDistance = 7f;
    public float acceleration = 10f;

    private Wander wander;
    private Seek seek;
    private Flee flee;
    private Arrive arrive;
    private ObstacleAvoidance obstacleAvoidance;
    private StateMachine stateMachine;
    private Rigidbody2D rb;

    void Start()
    {
        wander = GetComponent<Wander>();
        seek = GetComponent<Seek>();
        flee = GetComponent<Flee>();
        arrive = GetComponent<Arrive>();
        obstacleAvoidance = GetComponent<ObstacleAvoidance>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine(AIState.Wander);

        if (arrive != null) arrive.target = target;
        if (seek != null) seek.target = target;
        if (flee != null) flee.target = target;
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(rb.position, target.position);

        if (stateMachine.currentState == AIState.Wander)
        {
            if (reactionState != AIState.Wander && distance <= detectionDistance)
                stateMachine.ChangeState(reactionState);
        }
        else if (distance > loseDistance)
        {
            stateMachine.ChangeState(AIState.Wander);
        }

        SteeringOutput steering = null;
        switch (stateMachine.currentState)
        {
            case AIState.Wander:
                if (wander != null) steering = wander.GetSteering();
                break;
            case AIState.Flee:
                if (flee != null) steering = flee.GetSteering();
                break;
            case AIState.Seek:
                if (arrive != null && distance < arrive.slowRadius)
                    steering = arrive.GetSteering();
                else if (seek != null)
                    steering = seek.GetSteering();
                break;
        }

        if (steering == null) return;

        if (obstacleAvoidance != null)
            steering = obstacleAvoidance.GetSteering(steering.velocity);

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, steering.velocity, acceleration * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (stateMachine.currentState == AIState.Wander && wander != null)
            wander.ChooseNewDirection();
    }
}