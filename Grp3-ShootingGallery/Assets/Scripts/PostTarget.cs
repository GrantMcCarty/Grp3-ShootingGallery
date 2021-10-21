using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PostTarget : Target
{
    // Start is called before the first frame update
    bool rotationOn;
    const float FinalXRotation = 90f;

    const float rotateInterval = 1f;
    Vector3 v;
    void Start()
    {
        rotationOn = false;
        v = transform.parent.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        float xRotation = transform.parent.rotation.eulerAngles.x;
        if(rotationOn && transform.parent.rotation != Quaternion.Euler(90,0,0)){
            transform.parent.Rotate(rotateInterval, 0f, 0f, Space.World);
             transform.parent.Translate(new Vector3(0,2.3f,4) * Time.deltaTime);
            
        }
        if(xRotation >= FinalXRotation){
            Debug.Log(xRotation);
            transform.parent.rotation = Quaternion.Euler(90,0,0);
            rotationOn = false;

            } 
        
    }

    public override void Process(Collider collider){
        Debug.Log("POST TARGET HIT");
        rotationOn = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.transform.parent == transform.parent){
            Debug.Log(collision.collider, GetComponent<Collider>());
             Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        //Debug.Log("COLLISION");
    }
}