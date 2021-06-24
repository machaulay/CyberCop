using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerController playerController;

    public Transform firePoint;
    public GameObject bulletPreFab;

    public KeyCode fireButton;


    void Update(){
        if (Input.GetKeyDown(fireButton) && playerController.weaponId == 1){
            Shoot();
        }
    }

    public void Shoot(){
        Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
    }
}
