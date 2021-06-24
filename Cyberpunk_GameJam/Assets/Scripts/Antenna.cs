using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D trigg) {
         if (trigg.gameObject.CompareTag("Player")) {
            var player = trigg.GetComponent<PlayerController>();
            player.podeParar = true;
            print(player.podeParar);
            
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
       
        
    }

    private void OnTriggerExit2D(Collider2D coll) {
         if (coll.gameObject.CompareTag("Player")) {
            var player = coll.GetComponent<PlayerController>();
            player.podeParar = false;
        }
        
    }


}
