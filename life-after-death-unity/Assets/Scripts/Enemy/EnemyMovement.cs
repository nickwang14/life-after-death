using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const string speedFloatString = "Speed";

    readonly int speedFlotHash = Animator.StringToHash(speedFloatString);

    [SerializeField]
    Enemy enemy;

    [SerializeField]
    Rigidbody2D rigid;

    [SerializeField]
    Animator liveAnim;

    [SerializeField]
    Animator deadAnim;

    [SerializeField]
    float speed = 1f;

    bool leftFacing = true;

    Vector2[] patrolPoints;

    int patrolPointIndex = 0;

    Vector2 target;

    [SerializeField]
    float leftDistance = 1f;

    [SerializeField]
    float rightDistance = 1f;

    void Start()
    {
        liveAnim.SetFloat(speedFlotHash, speed);
        deadAnim.SetFloat(speedFlotHash, speed);
        SetupWaypoints();
        target = patrolPoints[patrolPointIndex];
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    void MoveForward()
    {
        if (leftFacing)
            rigid.MovePosition(transform.position + new Vector3(-speed * Time.deltaTime, 0f, 0f));
        else
            rigid.MovePosition(transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f));
    }

    void MoveTowardsTarget()
    {
        float move = target.x - transform.position.x;
        if (move == 0f)
            SetNextWaypoint();

        if (move > 0f && leftFacing)
            TurnArround();
        else if (move < 0f && !leftFacing)
            TurnArround();

        float newX = Mathf.MoveTowards(transform.position.x, target.x, speed * Time.deltaTime);
        float oldY = transform.position.y;
        rigid.MovePosition(new Vector2(newX, oldY));
    }

    void SetNextWaypoint()
    {
        patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
        target = patrolPoints[patrolPointIndex];
    }

    public void TurnArround()
    {
        leftFacing = !leftFacing;
        enemy.SetSpriteFlip(!leftFacing);
    }

    void SetupWaypoints()
    {
        Vector2 leftWaypoint = transform.position;
        leftWaypoint.x -= leftDistance;

        Vector2 rightWaypoint = transform.position;
        rightWaypoint.x += rightDistance;

        patrolPoints = new Vector2[] { leftWaypoint, rightWaypoint };
    }
}
