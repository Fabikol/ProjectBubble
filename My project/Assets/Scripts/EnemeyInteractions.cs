using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemeyInteractions : MonoBehaviour
{
    
    public float KnockbackForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = other.gameObject.transform.position - transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized*KnockbackForce, ForceMode.VelocityChange);
        }
    }
}
