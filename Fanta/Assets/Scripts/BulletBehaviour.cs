using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private DamageScript damage;
    private Transform target;
    private Rigidbody bulletRb;
    private float speed = 10.0f;
    
    private bool homing;
    private float bulletStrength = 10.0f;
    private float aliveTimer = 2.0f;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();       
    }
    void Update()
    {
        if(homing & target != null)
        {
            Vector3 moveDirection = (new Vector3(target.position.x,target.position.y+0.8f,target.position.z) - transform.position).normalized;
            //transform.position += moveDirection * speed * Time.deltaTime;
            bulletRb.AddForce(moveDirection*speed,ForceMode.Impulse);
            transform.LookAt(target);
        }
        
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject,aliveTimer);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Crystal"))
        {
            Debug.Log("Crystal was hit");
            Destroy(gameObject);
            playerController = col.gameObject.GetComponent<PlayerController>();
            damage = col.gameObject.GetComponent<DamageScript>();
            if(playerController.isGlowing)
            {
                damage.health = damage.health - bulletStrength;
                Debug.Log(damage.health);
            }
            
        }
    }
}
