using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject switchObj;

    public bool spawn = true;

    // Update is called once per frame
    void Update() {
        if(spawn){
            StartCoroutine(SpawnSwitch());
            spawn = false;
        }
        

    }

    IEnumerator SpawnSwitch(){
        yield return new WaitForSeconds(10f);
        Instantiate(switchObj, spawnPoints[Random.Range(0,8)].transform.position, spawnPoints[0].transform.rotation);   
        spawn = true;
    }



}
