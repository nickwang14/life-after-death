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

    void Start()
    {
        anim.SetFloat(speedFlotHash, speed);
    }

    void Update()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        if (leftFacing)
            rigid.MovePosition(transform.position + new Vector3(-speed * Time.deltaTime, 0f, 0f));
        else
            rigid.MovePosition(transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f));
    }

    public void TurnArround()
    {
        leftFacing = !leftFacing;
        spriteRenderer.flipX = !leftFacing;
    }
}
