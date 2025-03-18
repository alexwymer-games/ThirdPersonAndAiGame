using System;
using Unity.Cinemachine;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    //Cinemachine 
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

    [SerializeField] private Vector2[] recoilPatterns;

    public Animator playerRigAnimator;

    public float verticalRecoil;
    public float horizontalRecoil;

    private int recoilPatternIndex;

    public float recoilDuration;
    public float recoilTime;

    private void Awake()
    {
        //Get Components
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        recoilTime = recoilDuration;
    }

    public void GenerateRecoil(string weaponName)
    {
        cinemachineImpulseSource.GenerateImpulse();

        horizontalRecoil = recoilPatterns[recoilPatternIndex].x;
        verticalRecoil = recoilPatterns[recoilPatternIndex].y;

        recoilPatternIndex = GetNextRecoilIndex();

        playerRigAnimator.Play("WeaponRecoil_" + weaponName, 1, 0.0f);
    }

    private int GetNextRecoilIndex()
    {
        return (recoilPatternIndex + 1) % recoilPatterns.Length;
    }

}
