using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public KeyCode dash;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteAnimator spriteAnimator;
    private Vector3 playerPosition = Vector3.zero;
    public Transform boss_Start;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D bossCol;

    public float velocidade;

    private bool moving;
    private bool atacking;
    public bool follow = false;

    private bool dashButton;
    public bool paralizado;


    public float velocidadeDash;
    public float tempoDash;
    public float Life = 100;
    public bool darDash;

    //Arma
    public GameObject bulletPreFab;
    public Transform firePoint;

    public GameObject explosaoPrefab;

    void Start() {
        transform.position = boss_Start.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // InvokeRepeating("UpdateTarget", 0.0f, 0.0f);
        paralizado = false;
        bossCol = GetComponent<BoxCollider2D>();
        bossCol.enabled = false;
        StartCoroutine(Attack());
    }

    void Update() {
        UpdateTarget();

        Following();

        ApplyAnimation();
        
        // if (darDash) {
        //     StartCoroutine(Dash());
            
        // }

        if (transform.position.x < 0.0f) {
            transform.Rotate(0f, 180f, 0f);
            spriteRenderer.flipX = true;
        }else if(transform.position.x > 0.0f){
            spriteRenderer.flipX = false;
        }

        if (paralizado){
            animator.SetBool("parado", true);
            animator.SetBool("attack", false);
            transform.position = boss_Start.position;
            bossCol.enabled = true;
            StartCoroutine(voltar());
        }

        if (Life == 0) {
            Instantiate(explosaoPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator voltar(){
        yield return new WaitForSeconds(5.0f);
        paralizado = false;
        bossCol.enabled = false;
        animator.SetBool("parado", false);
        StartCoroutine(Attack());
    }


    public IEnumerator Attack(){
        if (!paralizado) {
            yield return new WaitForSeconds(2.0f);
            animator.SetBool("attack", true);
        }
        
        
        
    }

    public void paraIdle(int aux) {
        switch (aux) {
            case 1:
                animator.SetBool("attack", false);
                break;
        }
    }

    public void Atirar(int aux) {
        switch (aux) {
            case 1:
                Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
                break;
        }
    }

    public void Flipa(int aux) {
        switch (aux) {
            case 1:
                transform.Rotate(0f, 180f, 0f);
                break;
        }
    }

    string lastAnimation;
    string currentAnimation;
    public void ApplyAnimation() {
        
        if (paralizado) {
            currentAnimation = "Parado";
        }else{
            currentAnimation = "Shooting";
        }
        
        if (transform.position == new Vector3(0f, 0f, 0f)) {
           currentAnimation = "IdleBoss"; 
        }

        
        
        if (currentAnimation != lastAnimation) {
            lastAnimation = currentAnimation;
            spriteAnimator.StopAllCoroutines();
            spriteAnimator.StartCoroutine(spriteAnimator.PlayAnimation(currentAnimation, true));
        }
    }

    public void Flip() {
        transform.Rotate(0f, 180f, 0f);

    }

    public LayerMask playerLayer;
    public float collisionRadius;
    public Vector2 bottomOffset;
    public void Following() {

        follow = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, 
        collisionRadius, 
        playerLayer);

        
        
    }

    // public IEnumerator Dash() {

    //         // //Dash teleporte
    //         // float dashAmount = 2f;
    //         // rb.MovePosition(playerPosition * dashAmount);
            
    //         // //Segue o personagem
    //         // transform.position = Vector2.MoveTowards(transform.position, 
    //         //     new Vector2(playerPosition.x, playerPosition.y) , 
    //         //     Time.deltaTime * velocidade);
        
    // }

    void UpdateTarget() {
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in Player) {
            playerPosition = player.transform.position;
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
    }

}
