using System.Collections.Generic;
using UnityEngine;

struct Bullet
{
    public float time;
    public Vector3 initialPosition;
    public Vector3 initialVelocity;
    public TrailRenderer tracerRenderer;
}

public class OtherWeapon : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem[] muzzleFlashParticle;
    [SerializeField] private ParticleSystem hitImpactParticle;
    [SerializeField] private TrailRenderer tracerTrailEffect;

    public bool isFiring = false;

    //Weapon Variables
    [SerializeField] private float fireRate = 10;

    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;

    [SerializeField] List<Bullet> bulletList = new List<Bullet>();

    private float accumulatedTime;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform raycastDestination;

    private Ray ray;
    RaycastHit hitInfo;

    float bulletMaxLifeTime = 3.0f;


    public void StartFiring()
    {
        isFiring = true;

        accumulatedTime = 0.0f;

        FireBullet();
    }


    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;

        float fireInterval = 1.0f / fireRate;

        while (accumulatedTime >= fireInterval) 
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime) 
    {
        Debug.Log(bulletList.Count);


        bulletList.ForEach(bullet => 
        {
            Vector3 p0 = GetBulletPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetBulletPosition(bullet);
            RaycastBulletSegment(p0, p1, bullet);
        });
    }


    private void DestroyBullets()
    {
        //bulletList.RemoveAll(bullet => bullet.time >= bulletMaxLifeTime);

        foreach (var bullet in bulletList)
        {
            if (bullet.time >= bulletMaxLifeTime)
            {
                bulletList.Remove(bullet);
            }

        }
    }

    private void RaycastBulletSegment(Vector3 start, Vector3 end, Bullet bullet)
    {

        Vector3 direction = end - start;
        float distance = direction.magnitude;

        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            hitImpactParticle.transform.position = hitInfo.point;
            hitImpactParticle.transform.forward = hitInfo.normal;
            hitImpactParticle.Emit(1);

            bullet.tracerRenderer.transform.position = hitInfo.point;
            bullet.time = bulletMaxLifeTime;
        }
        else
        {
            bullet.tracerRenderer.transform.position = end;   
        }
    }

    private void FireBullet()
    {
        foreach (var particle in muzzleFlashParticle)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bulletList.Add(bullet);   
    }

    private Vector3 GetBulletPosition(Bullet bullet)
    {
        //Equation: p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    private Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracerRenderer = Instantiate(tracerTrailEffect, position, Quaternion.identity);
        bullet.tracerRenderer.AddPosition(position);
        return bullet;
    }

    public void StopFiring()
    {
        isFiring= false;
    }
}
