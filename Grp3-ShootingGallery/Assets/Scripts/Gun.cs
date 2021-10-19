using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Animator animator;
    public Transform barrelEnd;
    public GameObject bullet;
    ParticleSystem flash;
    AudioSource audioSource;
    public int range;
    public float fireInterval;

    void Start()
    {
        animator = GetComponent<Animator>();
        bullet = Resources.Load<GameObject>("Bullet");
        range = 80;
        fireInterval = 0.0f;
        flash = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        bool ready = Time.time > fireInterval;
        if(ready && Input.GetMouseButton(0)) {
            FireProjectile();
        }
    }

    void FireProjectile() {
        if(flash != null) flash.Play();
        audioSource.Play(0);
        GameObject projectileInstance;
        projectileInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
        projectileInstance.GetComponent<Projectile>().startPos = projectileInstance.transform.position;
        projectileInstance.GetComponent<Projectile>().range = range;
        fireInterval = Time.time +1f;
    }

    public void PerformAttack()
    {
        animator.SetBool("Fire", true);
        FireProjectile();
        // animator.SetBool("Fire", false);
    }
}
