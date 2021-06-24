using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour
{
    public GameObject consoleScreen1;
    public GameObject consoleScreen2;
    public GameObject consoleScreen3;

    public GameObject bloqueio01;
    public GameObject bloqueio02;
    public GameObject bloqueio03;

    public GameObject switchConsole;

    public GameObject playerHacking;

    public Console01 console01;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        playerHacking.SetActive(false);
        bloqueio01.SetActive(true);
        bloqueio02.SetActive(true);
        bloqueio03.SetActive(true);
    }

    public IEnumerator PlayerStart(){
        yield return new WaitForSeconds(1.1f);
        playerHacking.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        AtivaScreen();

    }

   

    void AtivaScreen(){
        if (playerController.screenActivate1) {
            consoleScreen1.SetActive(true);
            StartCoroutine(PlayerStart());
        }else if(!playerController.screenActivate1){
            consoleScreen1.SetActive(false);
        }
        if (playerController.screenActivate2) {
            consoleScreen2.SetActive(true);
            StartCoroutine(PlayerStart());
        }else if(!playerController.screenActivate2){
            consoleScreen2.SetActive(false);
        }
        if (playerController.screenActivate3) {
            consoleScreen3.SetActive(true);
            StartCoroutine(PlayerStart());
        }else if(!playerController.screenActivate3){
            consoleScreen3.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigg) {
        if (trigg.gameObject.CompareTag("Player")) {
            switchConsole.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("Player")) {
            switchConsole.SetActive(false);
        }
    }

    public void desativar(){
        // transform.gameObject.SetActive(false);
    }
}
