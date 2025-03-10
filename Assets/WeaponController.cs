
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool isFiring = false;
    public ParticleSystem[] muzzleFlashParticles;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    public Transform raycastOrigin;
    public Transform raycastDestination;

    public AnimationClip weaponAnimation;

    Ray ray;
    RaycastHit hitInfo;

    public void StartFiring()
    {
        isFiring = true;

        foreach(var particle  in muzzleFlashParticles) 
        {
            particle.Emit(1);
        }

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
