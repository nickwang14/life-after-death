using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisBoss : MonoBehaviour {

    [SerializeField]
    GameObject projecileSprite;

    [SerializeField]
    GameObject Zombie;

    List<GameObject> ZombiePool;

    List<GameObject> ProjectilePool;

    [SerializeField]
    float TimerToFire = 2.0f;

    [SerializeField]
    float bulletSpeed = 1.0f;

    [SerializeField]
    int spreadBulletAmount = 10;

    float fireParticleTimer;

    [SerializeField]
    GameObject FireLocation;

    [SerializeField]
    GameObject ZombieSpawnLocation;

    [SerializeField]
    int AnubisHealth = 100;

    [SerializeField]
    float invulTime = 1.5f;

    float invulTimer = 0f;

    [SerializeField]
    SpriteRenderer AnubisSprite;

    bool isNowAttacking = false;

    [SerializeField]
    Collider2D playerDetectHitBox;

    [SerializeField]
    Collider2D playerHitBox;

    public bool IsInvulnerable
    {
        get { return invulTimer > 0f; }
    }


    Color baseColor = Color.white;

    // Use this for initialization
    void Start ()
    {
        ProjectilePool = new List<GameObject>();
        for(int i = 0; i < 300; i++)
        {
            ProjectilePool.Add(Instantiate(projecileSprite, FireLocation.transform.position, Quaternion.Euler(0,0,-90)));
            ProjectilePool[i].SetActive(false);
        }

        ZombiePool = new List<GameObject>();
        for (int i = 0; i < 25; i++)
        {
            ZombiePool.Add(Instantiate(Zombie, ZombieSpawnLocation.transform.position, Quaternion.identity));
            ZombiePool[i].SetActive(false);
        }

        fireParticleTimer = TimerToFire;
    }
	
	// Update is called once per frame
	void Update ()
    {
        invulTimer = Mathf.MoveTowards(invulTimer, 0f, Time.deltaTime);

        if (IsInvulnerable)
        {
            float emission = Mathf.PingPong(Time.time * 7.5f, 1.0f);
            SetSpriteColor(new Color(baseColor.r, emission, emission));
        }
        else
        {
            SetSpriteColor(baseColor);
        }

        if (isNowAttacking)
        {
            fireParticleTimer -= 1.0f * Time.deltaTime;

            if (fireParticleTimer <= 0)
            {
                int attackChoice = Random.Range(0, 100);

                if (attackChoice >= 0 && attackChoice <= 60)
                    Fire();

                else if (attackChoice > 60 && attackChoice <= 70)
                    FireSpread(spreadBulletAmount);

                else
                {
                    SpawnZombie();
                }

                fireParticleTimer = TimerToFire;
            }
        }

        if(playerDetectHitBox.IsTouching(playerHitBox))
        {
            isNowAttacking = true;
        }
            

    }

    void SetSpriteColor(Color newColor)
    {
        Color liveColor = newColor;
        liveColor.a = AnubisSprite.color.a;
        AnubisSprite.color = liveColor;

    }

    void Fire()
    {
        GameObject tempProjectile;

        for(int i = 0; i < 300; i++)
        {
            if(!ProjectilePool[i].activeInHierarchy)
            {
                tempProjectile = ProjectilePool[i];
                tempProjectile.transform.position = FireLocation.transform.position;
                tempProjectile.transform.rotation = Quaternion.Euler(0, 0, -90);

                float randomRotation = Random.Range(-30, 30);
                tempProjectile.transform.Rotate(new Vector3(0, 0, randomRotation));
                tempProjectile.SetActive(true);
                tempProjectile.GetComponent<Bullet>().Fire(bulletSpeed);
                break;
            }
        }
    }

    void FireSpread(int amountOfBullets)
    {
        float degreeIncrement = 170 / amountOfBullets;

        GameObject tempProjectile;

        for (int a = 0; a < amountOfBullets; a++)
        {
            for (int i = 0; i < 300; i++)
            {
                if (!ProjectilePool[i].activeInHierarchy)
                {
                    tempProjectile = ProjectilePool[i];
                    tempProjectile.transform.position = FireLocation.transform.position;
                    tempProjectile.transform.rotation = Quaternion.Euler(0, 0, -90);
                    tempProjectile.transform.Rotate(new Vector3(0, 0, -90 + (degreeIncrement * a)));
                    tempProjectile.SetActive(true);
                    tempProjectile.GetComponent<Bullet>().Fire(bulletSpeed);
                    break;
                }
            }
        }
    }

    void SpawnZombie()
    {
        GameObject tempZombie;

        for (int i = 0; i < 25; i++)
        {
            if (!ZombiePool[i].activeInHierarchy)
            {
                tempZombie = ZombiePool[i];
                tempZombie.transform.position = ZombieSpawnLocation.transform.position;

                tempZombie.GetComponent<ZombieMovement>().TurnArround();

                int stateRandom = Random.Range(0, 101);
                Debug.Log(stateRandom);
                if (stateRandom <= 40)
                {
                    tempZombie.GetComponent<Enemy>().startingEnemyState = Enemy.EnemyState.Alive;
                    tempZombie.GetComponent<Enemy>().SwitchToAlive();
                }
                else
                {
                    tempZombie.GetComponent<Enemy>().startingEnemyState = Enemy.EnemyState.Dead; 
                    tempZombie.GetComponent<Enemy>().SwitchToDeath();
                }


                tempZombie.SetActive(true);
                break;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Dead)
            return;
        if (IsInvulnerable)
            return;

        AnubisHealth -= dmg;
        AnubisHealth = Mathf.Clamp(AnubisHealth, 0, 100);
        invulTimer = invulTime;
        if (AnubisHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        foreach (GameObject item in ZombiePool)
        {
            Destroy(item.gameObject);
        }

        foreach (GameObject item in ProjectilePool)
        {
            Destroy(item.gameObject);
        }

        GameSceneManager.ActivePlayer.PlayerStats.AddKeys(1);

        Destroy(this.gameObject);
    }
}
