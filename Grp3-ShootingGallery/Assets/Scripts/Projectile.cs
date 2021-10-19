using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int range;
    public float speed = 50.0f;
    public Vector3 startPos;

    void Update() {
        if(range > Vector3.Distance(transform.position, startPos)) {
            transform.position += transform.up * speed * Time.deltaTime;
        } 
        else {
            Destroy(this.gameObject); 
        }
    }

    void OnTriggerEnter(Collider col)
    {
        var tag = col.gameObject.tag;
        Debug.Log(tag);
        if(tag == "Target") {
            var target = col.gameObject.transform.GetComponent<Target>();
            if(target != null) {
                target.Process(col);
            }
        }
        Destroy(this.gameObject);
    }

}
