using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected Transform _firePos;


    [Header("HP")]
    public int hp;


    [Header("Bullets")]
    [SerializeField] protected float bulletSpeed = 1;
    [SerializeField] protected float fireCd = 0.5f;

    protected float lastFire = 0;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        lastFire = Time.time;
    }


    protected void Fire()
    {
        if(Time.time - lastFire < fireCd)
            return;        
        lastFire = Time.time;

        Bullet bullet = Instantiate(_bulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = _firePos.position;
        bullet.transform.forward = transform.forward;
        bullet.shooter = gameObject;
        bullet.speed = bulletSpeed;
        Destroy(bullet.gameObject, 3);
    }


    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    protected virtual void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Bullet"))
        {
            Bullet b = other.gameObject.GetComponent<Bullet>();
            if(b == null)
            {
                Debug.LogError("Bullet has no bullet script :(");
                return;
            }

            if(b.shooter != gameObject)
            {
                hp -= 1;
                Destroy(other.gameObject);
            }
        }
    }
}
