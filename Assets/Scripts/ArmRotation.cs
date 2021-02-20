using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 0;

    // Update is called once per frame
    void Update()
    {
        // substracting the position of the arm from the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;  
        difference.Normalize();     // normalizing the vector. Meaning that all the sum of the vector will be equal to 1.

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in 
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
