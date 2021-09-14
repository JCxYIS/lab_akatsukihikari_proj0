using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public GameObject shooter;    
    public float speed;

    new Rigidbody rigidbody;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(!rigidbody)
            rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(transform.position + transform.forward * speed);
    }
}