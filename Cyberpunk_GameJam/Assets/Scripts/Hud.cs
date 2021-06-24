using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    public PlayerController playerController;
    public Boss boss;
    public GameObject[] lifeImg;
    public Text deathTxt;

    public GameObject bgEnding;

    
    void Start() {
        StartGame();
        bgEnding.SetActive(false);
        lifeImg[0].SetActive(true);
        lifeImg[1].SetActive(true);
        lifeImg[2].SetActive(true);
        lifeImg[3].SetActive(true);
        lifeImg[4].SetActive(true);
        lifeImg[5].SetActive(true);
    }

    void Update() {
        lifeHud();

        if (boss.Life == 0) {
            StartCoroutine(winHud());
        }
    }

    void lifeHud(){
        if (playerController.Life == 4) {
            lifeImg[0].SetActive(false);
        }else if(playerController.Life == 3){
            lifeImg[1].SetActive(false);
        }else if(playerController.Life == 2){
            lifeImg[2].SetActive(false);
        }else if(playerController.Life == 1){
            lifeImg[3].SetActive(false);
        }else if(playerController.Life <= 0){
            lifeImg[4].SetActive(false);
            StartCoroutine(deathHud());
        }
    }

    IEnumerator deathHud(){
        bgEnding.SetActive(true);
        yield return new WaitForSecondsRealtime(4.0f);
        PauseGame();
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene("cena");
    }   

    IEnumerator winHud(){
        yield return new WaitForSecondsRealtime(3.0f);
        bgEnding.SetActive(true);
        yield return new WaitForSecondsRealtime(4.0f);
        PauseGame();
        SceneManager.LoadScene("cena");
    }   

    void PauseGame(){
        Time.timeScale = 0;
    }
    void StartGame(){
        Time.timeScale = 1;
    }
}
