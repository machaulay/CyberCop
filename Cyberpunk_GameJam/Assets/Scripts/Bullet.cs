using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    public GameObject impactoPrefab;

    public GameObject explosaoPrefab;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 5.0f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
            //Instantiate random impact prefab from array
			Instantiate (impactoPrefab, transform.position, 
				transform.rotation);
			//Destroy bullet object
			Destroy(gameObject);

            if (other.gameObject.CompareTag("Inimigo")) {
                //Destroi inimigo
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Player")) {
                //Dano no player
                
            }
            if (other.gameObject.CompareTag("boss")) {
                var boss = other.gameObject.GetComponent<Boss>();
                boss.Life--;
                
            }
            if (other.gameObject.CompareTag("bannerBloqueio")) {
                Instantiate(explosaoPrefab, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
            
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("limiteTiro")) {
            Destroy(gameObject);
        }
        
    }
}
