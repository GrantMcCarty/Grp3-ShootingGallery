using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCube : Target
{
    bool rotationOn = false;
    float rotationAmount = 360f;
    float currentRotation = 0f;
    Vector3 rotation;
    float time;

    void Update() {
        if(rotationOn) {
            if(currentRotation < rotationAmount) {
                time= Time.deltaTime;
                transform.Rotate(rotation * time);
                currentRotation += rotationAmount * time * 2f;
            } else {
                rotationOn = false;
                transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
    }

    void Rotate(float degrees) {
        if(!rotationOn) {
            rotationOn = true;
            rotationAmount = degrees;
            currentRotation = 0f;
            rotation = new Vector3(0, degrees, 0);
        }
    }

    public override void Process(Collider col) {
        Debug.Log("Here");
        Rotate(rotationAmount);
    }
}
