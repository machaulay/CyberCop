using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public SpriteAnimator spriteAnimator;

    public Vector3 delta;

    public bool facingRight;

    public Transform firePoint;
    public GameObject bulletPreFab;

    void Update() {
        UpdateTarget();
        
        Atacar();

        ApplyMovement();

        ApplyMovementAnimation();
    }

    string lastAnimation;
    string currentAnimation;
    public void ApplyMovementAnimation() {

        if (delta.x != 0) {
            if (delta.x < 0 && !facingRight){
                // spriteAnimator.GetComponent<SpriteRenderer>().flipX = true;
                Flip();
            }else if(delta.x > 0 && facingRight){
                // spriteAnimator.GetComponent<SpriteRenderer>().flipX = false;
                Flip();
            }
        }

        if (delta == Vector3.zero) {
            currentAnimation = "Idle";
            
        }
        if (delta != Vector3.zero) {
            currentAnimation = "Run";
          
        }

        if (currentAnimation != lastAnimation){
            lastAnimation = currentAnimation;
            spriteAnimator.StopAllCoroutines();
            spriteAnimator.StartCoroutine(spriteAnimator.PlayAnimation(currentAnimation, true));
        }

    }

    private bool follow;
    public float speed;
    private bool podeAtirar = true;
    void Atacar(){
        if (follow) {
           //Segue o personagem
        // transform.position = Vector2.MoveTowards(transform.position, 
        //     new Vector2(playerPosition.x, transform.position.y) , 
        //     Time.deltaTime * velocidade); 

            if(playerPosition.x < transform.position.x){
                delta.x = -speed * Time.deltaTime;
                if(podeAtirar) {
                    Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
                    StartCoroutine(Shoot());
                }
            }else{
                delta.x = speed * Time.deltaTime;
                if(podeAtirar) {
                    Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
                    StartCoroutine(Shoot());
                }
            }
            
            
        }

        
        
        
        
    }

    public void ApplyMovement() {
        transform.position += delta;
    }

    
    public IEnumerator Shoot(){
        podeAtirar = false;
        yield return new WaitForSeconds (1.0f);
        podeAtirar = true;
        
    }

    public LayerMask playerLayer;
    public float collisionRadius;
    public Vector2 bottomOffset;
    private Vector3 playerPosition = Vector3.zero;
    void UpdateTarget() {
            follow = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, 
        collisionRadius, 
        playerLayer);

        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in Player) {
            playerPosition = player.transform.position;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }

    public void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }
}
