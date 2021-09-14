using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public static Player Instance;

    [Header("Player")]
    [SerializeField] float move_speed = 0.09f;
    Vector3 inputVelocity = Vector3.zero;
    Camera mainCam;
    

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        mainCam = Camera.main;
        Instance = this;
        base.Awake();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // movement
        inputVelocity.x = Input.GetAxisRaw("Horizontal");
        inputVelocity.z = Input.GetAxisRaw("Vertical");

        // shooter
        if(Input.GetMouseButton(0))
        {
            Fire();
        }
        
        // look at mouse
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 999, 1<<6)) // only detect "Ground" Layer
        {
            Vector3 targetDirection = hit.point - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = targetRotation;
        }
        else
        {
            Debug.LogWarning("Look at mouse fail: Cannot raycast at ground");
        }

        // lose condition
        if(hp <= 0)
        {
            enabled = false;
            SceneManager.LoadScene("Result");
        }

        // buffing
        fireCd = 0.5f * Mathf.Pow(0.9f, GameManager.Score);
        if(fireCd < 0.1f)
            fireCd = 0.1f;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + inputVelocity * move_speed);        
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    protected override void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Foe"))
        {
            print("OUCH");
            hp -= 1;
        }
        else if(other.collider.CompareTag("Supply"))
        {
            hp += 1;
            Destroy(other.gameObject);
        }

        base.OnCollisionEnter(other);
    }
}