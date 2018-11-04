using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 PlayerSpeed = Vector2.zero;

    public bool AllowInput{ get; set; }

    public enum FacingDirection
    {
        FacingLeft,
        FacingRight
    }

    [SerializeField]
    Player thePlayer;

    public LayerMask lightLayerMask;
    public LayerMask darkLayerMask;

    [SerializeField]
    float cornerBalancingOffset = 0.1f;


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

    // Audio fields
    [SerializeField]
    PlayerSoundManager sfx;

    [SerializeField]
    float AudioTimer = 0.00f;

    // Animation Fields
    [SerializeField]
    Animator anim;

    [SerializeField]
    SpriteRenderer spriteDirection;

    const string moveFloatString = "HorizontalSpeed";
    readonly int moveFloatHash = Animator.StringToHash(moveFloatString);
    const string jumpFloatString = "VerticalSpeed";
    readonly int jumpFloatHash = Animator.StringToHash(jumpFloatString);

    // Use this for initialization
    void Start()
    {
        AllowInput = true;
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<Collider2D>();
        anim.SetFloat(moveFloatHash, PlayerSpeed.x);
        if (thePlayer == null)
            thePlayer = GetComponent<Player>();
   }

    void PlayerMovementHorizontal()
    {
       
        float horizontalAxis = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || horizontalAxis <= -0.2f)
        {
            directionFacing = FacingDirection.FacingLeft;
            WalkAudio();
            if (PlayerSpeed.x > 0.0f)
            {
                PlayerSpeed.x -= ((MovementAcceleration * OppositeMovementMultiplier) * -horizontalAxis) * Time.deltaTime;
            }
            else
                PlayerSpeed.x -= (MovementAcceleration * -horizontalAxis) * Time.deltaTime;

        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || horizontalAxis >= 0.2f)
        {
            directionFacing = FacingDirection.FacingRight;
            WalkAudio();
            if (PlayerSpeed.x < 0.0f)
            {
                PlayerSpeed.x += ((MovementAcceleration * OppositeMovementMultiplier) * horizontalAxis) * Time.deltaTime;
            }
            else
                PlayerSpeed.x += (MovementAcceleration * horizontalAxis) * Time.deltaTime;
        }

        else
        {
            PlayerSpeed.x = Mathf.Lerp(PlayerSpeed.x, 0.0f, MovementDeceleration * Time.deltaTime);
        }

        PlayerSpeed.x = Mathf.Clamp(PlayerSpeed.x, -MaxSpeed, MaxSpeed);
    }

    void GravityandJumping()
    {
        if (IsGrounded())
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Fire1")) && AllowInput)
            {
                PlayerSpeed.y += JumpingForce;
                sfx.PlaySound("jump");
            }

            else
                PlayerSpeed.y = 0.0f;
        }

        else
            PlayerSpeed.y -= GravityAcceleration * Time.deltaTime;
    }

    void Direction()
    {
        // Player Direction
        if (directionFacing == FacingDirection.FacingRight)
            spriteDirection.flipX = false;
        else
            spriteDirection.flipX = true;
    }
    void WalkAudio(){
        if (AudioTimer < .5f)
        {
            AudioTimer += Time.deltaTime;
        }
        else
        {
            AudioTimer = 0.00f;
            sfx.PlaySound("walk");
        }
    }
    void SettingFinalMovement()
    {
        Vector2 newPosition = transform.position;
        newPosition += PlayerSpeed;

        //PlayerRigidbody.MovePosition(newPosition);
        if (thePlayer.PlayerStats.State != PlayerStats.PlayerState.Destroyed)
            PlayerRigidbody.velocity = PlayerSpeed;
        else
            PlayerRigidbody.velocity = Vector2.zero;
    }

    void Animations()
    {
        anim.SetFloat(moveFloatHash, Mathf.Abs(PlayerSpeed.x));
        anim.SetFloat(jumpFloatHash, Mathf.Abs(PlayerSpeed.y));
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSpeed = PlayerRigidbody.velocity;

        if(AllowInput)
            PlayerMovementHorizontal();

        GravityandJumping();

        Direction();

        SettingFinalMovement();

        Animations();
    }

    
    bool IsGrounded()
    {
        RaycastHit2D isGroundedRay = new RaycastHit2D();
        RaycastHit2D isGroundedRayRight = new RaycastHit2D();
        RaycastHit2D isGroundedRayLeft = new RaycastHit2D();



        if (thePlayer.PlayerStats.State == PlayerStats.PlayerState.Alive)
        {
            isGroundedRay = Physics2D.Raycast(transform.position - new Vector3(0.0f, PlayerCollider.bounds.extents.y + 0.01f), -Vector2.up, 0.05f, lightLayerMask.value);
            isGroundedRayRight = Physics2D.Raycast(transform.position - new Vector3(PlayerCollider.bounds.extents.x - cornerBalancingOffset, PlayerCollider.bounds.extents.y + 0.01f), -Vector2.up, 0.05f, lightLayerMask.value);
            isGroundedRayLeft = Physics2D.Raycast(transform.position - new Vector3(-PlayerCollider.bounds.extents.x + cornerBalancingOffset, PlayerCollider.bounds.extents.y + 0.01f), -Vector2.up, 0.05f, lightLayerMask.value);
        }
        else if(thePlayer.PlayerStats.State == PlayerStats.PlayerState.Dead)
        {
            isGroundedRay = Physics2D.Raycast(transform.position - new Vector3(0.0f, PlayerCollider.bounds.extents.y + 0.01f), -Vector2.up, 0.05f, darkLayerMask.value);
            isGroundedRayRight = Physics2D.Raycast(transform.position - new Vector3(PlayerCollider.bounds.extents.x - cornerBalancingOffset, PlayerCollider.bounds.extents.y + 0.01f), -Vector2.up, 0.05f, darkLayerMask.value);
            isGroundedRayLeft = Physics2D.Raycast(transform.position - new Vector3(-PlayerCollider.bounds.extents.x + cornerBalancingOffset, PlayerCollider.bounds.extents.y + 0.01f), -Vector2.up, 0.05f, darkLayerMask.value);
        }

        if (isGroundedRay || isGroundedRayRight || isGroundedRayLeft)
        {
            if (isGroundedRay.collider != null || isGroundedRayRight.collider != null || isGroundedRayLeft.collider != null)
                return true;            
            else
                return false;
        }
        else
            return false;
    }
}

