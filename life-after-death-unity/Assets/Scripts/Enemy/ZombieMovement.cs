using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
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

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    float turnAroundDistance;

    void Start()
    {
        liveAnim.SetFloat(speedFlotHash, speed);
        deadAnim.SetFloat(speedFlotHash, speed);
    }

    void Update()
    {
        CheckForWall();
        MoveForward();
    }

    void MoveForward()
    {
        if (leftFacing)
            rigid.MovePosition(transform.position + new Vector3(-speed * Time.deltaTime, 0f, 0f));
        else
            rigid.MovePosition(transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f));
    }

    public void TurnArround()
    {
        leftFacing = !leftFacing;
        enemy.SetSpriteFlip(!leftFacing);
    }

    void CheckForWall()
    {
        Vector2 direction = leftFacing ? new Vector2(-1f, 0f) : new Vector2(1f, 0f);
        Vector2 origin = transform.position;
        if (Physics2D.Raycast(origin, direction, turnAroundDistance, layerMask.value))
            TurnArround();
    }
}
