using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;


    void Update() {
        rb.velocity = transform.right * -speed;
        Destroy(gameObject, 5.0f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
		Destroy(gameObject);
		
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("barreiraBoss")) {
            Destroy(gameObject);
        }
        
    }

    
}
