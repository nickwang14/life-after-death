﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 PlayerSpeed = Vector2.zero;

    [SerializeField]
    float MovementAcceleration = 1.0f;
    [SerializeField]
    float MaxSpeed = 3.0f;
    [SerializeField]
    float MovementDeceleration = 3.0f;

    Collider2D PlayerCollider;

    [SerializeField]
    float GravityAcceleration = 9.8f;

    [SerializeField]
    float OppositeMovementMultiplier = 2.5f;

    bool IsGround = true;

    Rigidbody2D PlayerRigidbody;

    // Use this for initialization
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (PlayerSpeed.x > 0.0f)
            {
                PlayerSpeed.x -= (MovementAcceleration * OppositeMovementMultiplier) * Time.deltaTime;
            }
            else
                PlayerSpeed.x -= MovementAcceleration * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (PlayerSpeed.x < 0.0f)
            {
                PlayerSpeed.x += (MovementAcceleration * OppositeMovementMultiplier) * Time.deltaTime;
            }
            PlayerSpeed.x += MovementAcceleration * Time.deltaTime;
        }

        else
        {
            PlayerSpeed.x = Mathf.Lerp(PlayerSpeed.x, 0.0f, MovementDeceleration * Time.deltaTime);
        }

        PlayerSpeed.x = Mathf.Clamp(PlayerSpeed.x, -MaxSpeed, MaxSpeed);

        //Gravity
        if (IsGrounded())
        {
            Debug.Log("Grounded");
            PlayerSpeed.y = 0.0f;
        }

        else
            PlayerSpeed.y -= GravityAcceleration * Time.deltaTime;

        Vector2 newPosition = transform.position;
        newPosition += PlayerSpeed;

        PlayerRigidbody.MovePosition(newPosition);

    }

    bool IsGrounded()
    {
        RaycastHit2D isGroundedRay = new RaycastHit2D();

        isGroundedRay = Physics2D.Raycast(transform.position - new Vector3(0.0f, PlayerCollider.bounds.extents.y), -Vector2.up, 0.1f);
        Debug.DrawRay(transform.position - new Vector3(0.0f, PlayerCollider.bounds.extents.y, 0.0f), -Vector2.up, Color.red);


        if (isGroundedRay.collider.tag == "Ground")
            return true;
        else
            return false;
    }
}

