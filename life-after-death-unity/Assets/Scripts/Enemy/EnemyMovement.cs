using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const string speedFloatString = "Speed";

    readonly int speedFlotHash = Animator.StringToHash(speedFloatString);

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Rigidbody2D rigid;

    [SerializeField]
    Animator anim;

    [SerializeField]
    float speed = 1f;

    bool leftFacing = true;

    [SerializeField]
    Transform[] patrolPoints;

    int patrolPointIndex = 0;

    Vector2 target;

    void Start()
    {
        anim.SetFloat(speedFlotHash, speed);
        target = patrolPoints[patrolPointIndex].position;
    }

    void Update()
    {
        //MoveForward();
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
        target = patrolPoints[patrolPointIndex].position;
    }

    public void TurnArround()
    {
        leftFacing = !leftFacing;
        spriteRenderer.flipX = !leftFacing;
    }
}
