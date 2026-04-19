using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    private void Update()
    {
        var moveDirection = new Vector3(target.position.x, target.position.y, -10f);

        transform.position = Vector3.MoveTowards(transform.position, moveDirection, 0.1f);
    }
}
