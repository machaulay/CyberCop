using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAux : MonoBehaviour
{
    
    public PlayerController playerController;
    public HackController hackController;
    public Transform startPosition;
    public int vida = 3;
    private SpriteRenderer playerSprite;
    Color corPadrao;

    void Start() {
        playerSprite = GetComponent<SpriteRenderer>();
        corPadrao = playerSprite.color;
    }
    void Update() {
        if(vida == 0){
            transform.position = startPosition.position;
            playerController.screenActivate1 = false;
            playerController.screenActivate2 = false;
            playerController.screenActivate3 = false;
            vida = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigg) {
        if(trigg.gameObject.CompareTag("finish")) {
            playerController.screenActivate1 = false;
            transform.position = startPosition.position;
            hackController.desativar();
            hackController.bloqueio01.SetActive(false);
        }
        if(trigg.gameObject.CompareTag("finish2")) {
            playerController.screenActivate2 = false;
            transform.position = startPosition.position;
            hackController.desativar();
            hackController.bloqueio02.SetActive(false);
        }
        if(trigg.gameObject.CompareTag("finish3")) {
            playerController.screenActivate3 = false;
            transform.position = startPosition.position;
            hackController.desativar();
            hackController.bloqueio03.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("barreira")) {
          StartCoroutine(Dano());
          vida--;  
        }
    }

    IEnumerator Dano(){
        playerSprite.color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = corPadrao;

    }
}
