using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console01 : MonoBehaviour
{

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public GameObject playerHacking;

    public Vector3 delta;

    public float speed;

    private void Start() {
        
    }

    

    void Update() {
        GetInput();

        ApplyMovement();

        // DetectCollision();
    }

public void GetInput() {
        delta = Vector3.zero;

        if (Input.GetKey(up)) {
            delta.y = speed * Time.deltaTime;
        }

        if (Input.GetKey(down)) {
            delta.y = -speed * Time.deltaTime;
        }

        if (Input.GetKey(left)) {
            delta.x = -speed * Time.deltaTime;
        }

        if (Input.GetKey(right)) {
            delta.x = speed * Time.deltaTime;
        }
    }

    public void ApplyMovement() {
        playerHacking.transform.position += delta;
    }


    public LayerMask wallLayer;
    public void DetectCollision() {
        if (delta.x != 0) {
            Vector3 rayDirection = new Vector3(Mathf.Sign(delta.x), 0, 0);
            Vector3 rayOrigin = playerHacking.transform.position + rayDirection * 1.0f;
            float rayLength = delta.x;

            Debug.DrawRay(rayOrigin, rayDirection);

            RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength);
            if (rayHit.collider != null) {
                delta.x = 0;
                print(delta.x);
            }
        }
        if (delta.y != 0) {
            Vector3 rayDirection = new Vector3(0, Mathf.Sign(delta.y), 0);
            Vector3 rayOrigin = playerHacking.transform.position + rayDirection * 1.0f;
            float rayLength = delta.y;

            Debug.DrawRay(rayOrigin, rayDirection);

            RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength);
            if (rayHit.collider != null) {
                delta.y = 0;
            }
        }

    }
}
