using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 PlayerSpeed = Vector2.zero;

    public enum FacingDirection
    {
        FacingLeft,
        FacingRight
    }


    FacingDirection directionFacing = FacingDirection.FacingRight;
    public FacingDirection GetDirectionFacing() { return directionFacing; }

    [SerializeField]
    float MovementAcceleration = 1.0f;
    [SerializeField]
    float MaxSpeed = 3.0f;
    [SerializeField]
    float MovementDeceleration = 3.0f;

    [SerializeField]
    float JumpingForce = 0.2f;

    Collider2D PlayerCollider;

    [SerializeField]
    float GravityAcceleration = 9.8f;

    [SerializeField]
    float OppositeMovementMultiplier = 2.5f;

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
            directionFacing = FacingDirection.FacingLeft;
            if (PlayerSpeed.x > 0.0f)
            {
                PlayerSpeed.x -= (MovementAcceleration * OppositeMovementMultiplier) * Time.deltaTime;
            }
            else
                PlayerSpeed.x -= MovementAcceleration * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            directionFacing = FacingDirection.FacingRight;
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
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                PlayerSpeed.y += JumpingForce;
            }

            else
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

        isGroundedRay = Physics2D.Raycast(transform.position - new Vector3(0.0f, PlayerCollider.bounds.extents.y + 0.01f), -Vector2.up, 0.05f);

        if (isGroundedRay)
        {
            if (isGroundedRay.collider != null)
            {
                if (isGroundedRay.collider.tag == "Ground")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }
}

