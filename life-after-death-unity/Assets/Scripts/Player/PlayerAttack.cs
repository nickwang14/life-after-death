using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer HighAttackSprite;
    [SerializeField]
    BoxCollider2D HighAttackTrigger;

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

        //HighAttackXValue = HighAttackTrigger.transform.position.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(!isAttacking)
            {
                Attack();
            }
        }
    }

    void Attack()
    {

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("HitEnemy");
        }
    }
}
