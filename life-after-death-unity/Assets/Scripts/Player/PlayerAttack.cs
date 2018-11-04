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

    [SerializeField]
    int Damage = 10;

    //float HighAttackXValue;

    [SerializeField]
    PlayerMovement player;

    [SerializeField]
    float attackDuration = 0.6f;

    [SerializeField]
    PlayerSoundManager sfx;

    bool isAttacking = false;

    // Use this for initialization
    void Start()
    {
        HighAttackSprite.enabled = false;
        HighAttackTrigger.enabled = false;

        hitBoxX = HighAttackSprite.transform.position.x;

        //HighAttackXValue = HighAttackTrigger.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Fire3")) && player.AllowInput)
        {
            if (!isAttacking)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (player.GetDirectionFacing() == PlayerMovement.FacingDirection.FacingRight)
        {
            if (HighAttackTrigger.transform.localPosition.x < 0)
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
        sfx.PlaySound("atk");
    }

    void StopAttack()
    {
        isAttacking = false;
        HighAttackSprite.enabled = false;
        HighAttackTrigger.enabled = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (isAttacking)
        {
            Enemy enemy = col.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
                Debug.Log("HitEnemy");
            }
        }
    }
}
