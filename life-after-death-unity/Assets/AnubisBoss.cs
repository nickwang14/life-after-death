using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisBoss : MonoBehaviour {

    [SerializeField]
    GameObject projecileSprite;

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

	// Use this for initialization
	void Start ()
    {
        ProjectilePool = new List<GameObject>();
        for(int i = 0; i < 300; i++)
        {
            ProjectilePool.Add(Instantiate(projecileSprite, FireLocation.transform.position, Quaternion.Euler(0,0,-90)));
            ProjectilePool[i].SetActive(false);
        }

        fireParticleTimer = TimerToFire;
    }
	
	// Update is called once per frame
	void Update ()
    {
        fireParticleTimer -= 1.0f * Time.deltaTime;

        if(fireParticleTimer <= 0)
        {
            int attackChoice = Random.Range(0, 100);

            if (attackChoice >= 0 && attackChoice <= 50)
                Fire();

            else if (attackChoice > 50 && attackChoice <= 60)
                FireSpread(spreadBulletAmount);

            else
            {
                SpawnSkeleton();
            }

            fireParticleTimer = TimerToFire;
        }

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

    void SpawnSkeleton()
    {

    }
}
