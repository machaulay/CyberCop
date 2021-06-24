using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Transform SpriteTransform;
    public SpriteAnimator spriteAnimator;
    public CameraScript cameraScript;
    public Boss boss;
    public SpriteRenderer playerSprite;
 
    
    public bool facingRight;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jumpButton;
    public KeyCode weapon1;
    public KeyCode weapon1Alt;
    public KeyCode weapon2;
    public KeyCode hacking;

    public Vector3 delta;
    public float speed;
    public float jumpForce;
    public int Life;

    public bool onGround;
    public bool screenActivate1;
    public bool screenActivate2;
    public bool screenActivate3;
    public bool podeAtivar1;
    public bool podeAtivar2;
    public bool podeAtivar3;

    Color corPadrao;


    private void Start() {
        corPadrao = playerSprite.color;
    }
    void Update() {
       GetInput();

       DetectCollision();

       ApplyMovementAnimation();

       WeaponChange();

       if(!screenActivate1 && !screenActivate2 && !screenActivate3) {
           ApplyMovement();
       }

    }

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;
    public void DetectCollision() {
        if (delta.x != 0) {
            Vector3 rayDirection = new Vector3(Mathf.Sign(delta.x), 0, 0);
            Vector3 rayOrigin = transform.position + rayDirection * 1.0f;
            float rayLength = delta.x;

            Debug.DrawRay(rayOrigin, rayDirection);

            RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, groundLayer);
            if (rayHit.collider != null) {
                delta.x = 0;
            }
        }

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

    }

    

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
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
            if (weaponId == 1){
                currentAnimation = "IdleGun";
            }else if(weaponId == 2){
                currentAnimation = "IdleGlasses";
            }else{
                currentAnimation = "Idle";
            }
        }
        if (delta != Vector3.zero) {
            if(weaponId == 1){
                currentAnimation = "GunRun";
            }else if(weaponId == 2){
                currentAnimation = "GlassesRun";
            }else{
                currentAnimation = "Run";
            }
        }
        if (onGround == false) {
            if (weaponId == 1) {
                currentAnimation = "JumpGun";
            }else{
                currentAnimation = "Jump";
            }

        }

        if (currentAnimation != lastAnimation){
            lastAnimation = currentAnimation;
            spriteAnimator.StopAllCoroutines();
            spriteAnimator.StartCoroutine(spriteAnimator.PlayAnimation(currentAnimation, true));
        }

    }

    public bool podeParar = false;
    public void GetInput() {
        delta = Vector3.zero;
        // if (Input.GetKey(up)) {
        // }

        // if (Input.GetKey(down)) {
            
        // }

        if (Input.GetKey(left)) {
            delta.x = -speed * Time.deltaTime;
        }

        if (Input.GetKey(right)) {
            delta.x = speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(jumpButton) && !screenActivate1 && !screenActivate2 && !screenActivate3) {
            Jump();
        }

        if (Input.GetKeyDown(hacking) && podeAtivar1) {
            screenActivate1 = true;
        }
        if (Input.GetKeyDown(hacking) && podeAtivar2) {
            screenActivate2 = true;
        }
        if (Input.GetKeyDown(hacking) && podeAtivar3) {
            screenActivate3 = true;
        }

        if(Input.GetKeyDown(hacking) && podeParar){
            boss.paralizado = true;
        }
    }

    public void Jump() {
        if (onGround) {
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }
    
    public void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    public void ApplyMovement() {
        transform.position += delta;
    }

    public float weaponId;
    public void WeaponChange(){
        if (Input.GetKeyDown(weapon1) || Input.GetKeyDown(weapon1Alt) ) {
            weaponId = 1;
        }
        if (Input.GetKeyDown(weapon2)) {
            weaponId = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigg) {
        if (trigg.gameObject.CompareTag("heightOut")) {
            cameraScript.height = -1;
        }
        if (trigg.gameObject.CompareTag("heightIn")) {
            cameraScript.height = 1;
        }

        if (trigg.gameObject.CompareTag("console")) {
            podeAtivar1 = true;
            
        }
        if (trigg.gameObject.CompareTag("console2")) {
            podeAtivar2 = true;
            
        }
        if (trigg.gameObject.CompareTag("console3")) {
            podeAtivar3 = true;
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D trigg) {
        if (trigg.gameObject.CompareTag("console")) {
            podeAtivar1 = false;
        }
        if (trigg.gameObject.CompareTag("console2")) {
            podeAtivar2 = false;
        }
        if (trigg.gameObject.CompareTag("console3")) {
            podeAtivar3 = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("dano")) {
            StartCoroutine(Dano());
            Life--;
        }

        if (coll.gameObject.CompareTag("morteQueda")) {
            Life = 0;
        }
    }


    IEnumerator Dano(){
        playerSprite.color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = corPadrao;

    }
    
    

}
