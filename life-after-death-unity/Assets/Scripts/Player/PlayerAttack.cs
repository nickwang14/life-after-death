using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer HighAttackSprite;
    [SerializeField]
    BoxCollider2D HighAttackTrigger;

    float hitBoxX;

    //float HighAttackXValue;

    [SerializeField]
    PlayerMovement player;

    [SerializeField]
    float attackDuration = 0.6f;

    bool isAttacking = false;

    // Use this for initialization
    void Start ()
    {
        HighAttackSprite.enabled = false;
        HighAttackTrigger.enabled = false;

        hitBoxX = HighAttackSprite.transform.position.x;

        //HighAttackXValue = HighAttackTrigger.transform.position.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Space) && player.AllowInput)
        {
            if(!isAttacking)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if(player.GetDirectionFacing() == PlayerMovement.FacingDirection.FacingRight)
        {
            if(HighAttackTrigger.transform.localPosition.x < 0)
                HighAttackTrigger.transform.RotateAround(transform.position, Vector3.up, 180.0f);
        }

        else if (player.GetDirectionFacing() == PlayerMovement.FacingDirection.FacingLeft)
        {
            if (HighAttackTrigger.transform.localPosition.x > 0)
                HighAttackTrigger.transform.RotateAround(transform.position, Vector3.up, 180.0f);
        }
        isAttacking = true;
        HighAttackSprite.enabled = true;
        HighAttackTrigger.enabled = true;

        Invoke("StopAttack", attackDuration);
    }

    void StopAttack()
    {
        isAttacking = false;
        HighAttackSprite.enabled = false;
        HighAttackTrigger.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("HitEnemy");
        }
    }
}
