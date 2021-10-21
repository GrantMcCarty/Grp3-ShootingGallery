using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
public class ExplodingTarget : Target
{
    public GameObject explosion;
    public AudioClip explosionSound;

    AudioSource actionSound;


    private static Vector3 pos;
    void Start(){
        actionSound = GetComponent<AudioSource>();
        explosion.GetComponent<UnityStandardAssets.Effects.ExplosionPhysicsForce>().explosionForce = 2;
        pos = gameObject.transform.position;
    }


    public override void Process(Collider collider)
    {
        Debug.Log("EXPLODE");
        
       Instantiate(explosion, pos, new Quaternion(0,0,0,0));
       
       actionSound.PlayOneShot(explosionSound, 1.0f);
       GetComponent<MeshRenderer>().enabled = false;
       
       Invoke("Delete",3.0f);
    }

    void Delete(){
        Destroy(gameObject);
    }
}
