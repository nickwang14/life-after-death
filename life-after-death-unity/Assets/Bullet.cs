using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float Speed;

    float bulletLifeTime = 10.0f;
    float maxLifeTime = 10.0f;

    [SerializeField]
    int DamageAmount = 10;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += new Vector3(Speed * transform.up.x, Speed * transform.up.y, 0) * Time.deltaTime;

        bulletLifeTime -= 1.0f * Time.deltaTime;
        if (bulletLifeTime <= 0)
            Disable();

    }

    public void Fire(float speed)
    {
        Speed = speed;
    }

    void Disable()
    {
        bulletLifeTime = maxLifeTime;
        this.gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
       
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.PlayerStats.DamagePlayer(DamageAmount);
            Disable();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player == null)
        {
            Disable();
        }
    }
}
