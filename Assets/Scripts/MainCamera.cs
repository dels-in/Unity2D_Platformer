using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform trackingObject;

    private void Update()
    {
        transform.position = new Vector3(trackingObject.position.x, transform.position.y, transform.position.z);
    }
}
