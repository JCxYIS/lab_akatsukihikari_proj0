using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe : Character
{
    [Header("Foe")]
    [SerializeField] float moveSpeed = 0.02f;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        tag = "Foe";
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(hp <= 0)
        {
            enabled = false;
            GameManager.Score++;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        // lookat
        transform.LookAt(Player.Instance.transform);

        // move to player
        if(moveSpeed != 0)
        {
            Vector3 nextDirection = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, moveSpeed);
            _rigidbody.MovePosition(nextDirection);
        }

        // fire
        if(fireCd > 0)
        {
            Fire();
        }
    }
}