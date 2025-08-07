using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Player : MonoBehaviour
{
    public ContactFilter2D ContactFilter;

    public int health = 5;
    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath onPlayerDeath;
    public delegate void OnPlayerHit();
    public static event OnPlayerHit onPlayerHit;

    // Movement Variables
    private float speed = 3f;
    private float jumpForce = 11f;
    private float movementX;
    private bool jump = false;

    // Object Components
    private Rigidbody2D playerBody;
    private SpriteRenderer sr;
    
    // Animation
    private Animator anim;
    private string WALKING = "Walking";
    private string ATTACKING = "Attacking";

    private string ENEMY_TAG = "Enemy";
    private string ENEMY_PROJECTILE = "EnemyProjectile";

    // Projectiles
    private ProjectileController playerProjectile;
    private float fireRate = 0.3f;
    private float projectileSpeed = 5f;
    private float maxRange = 5f;

    private bool isGrounded => playerBody.IsTouching(ContactFilter);

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerProjectile = GetComponentInChildren<ProjectileController>();

    }

    void Start(){}

    void Update()
    {
        CheckAttack();
        CheckJump();
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
        PlayerJump();
        PlayerMove();
    }

    void PlayerMove()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        playerBody.linearVelocity = new Vector2(movementX * speed, playerBody.linearVelocityY);
    }

    // Player input to attack and bool return for animation transitions
    bool CheckAttack()
    {
        if (Input.GetKey("e"))
        {
            playerProjectile.SpawnProjectile(fireRate, projectileSpeed, maxRange, transform.rotation);
            return true;
        }
        return false;
    }

    // Check for jump input and if player is allowed to jump
    void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    // When CheckJump allows player to jump function applies upward force
    void PlayerJump()
    {
        if (jump && isGrounded)
        {
            playerBody.AddForce(new Vector2(movementX, jumpForce), ForceMode2D.Impulse);
        }
        jump = false;
    }

    void AnimatePlayer()
    {
        // Moving player
        if (movementX != 0)
        {
            anim.SetBool(WALKING, true);

            if (movementX > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            // Attacking while moving
            if (CheckAttack())
            {
                anim.SetBool(ATTACKING, true);
            }
            else
            {
                anim.SetBool(ATTACKING, false);
            }
        }
        // Attacking while idle
        else if (movementX == 0 && CheckAttack())
        {
            anim.SetBool(WALKING, false);
            anim.SetBool(ATTACKING, true);
        }
        // Completely idle
        else
        {
            anim.SetBool(WALKING, false);
            anim.SetBool(ATTACKING, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_PROJECTILE) || collision.gameObject.CompareTag(ENEMY_TAG))
        {
            health -= 1;
            onPlayerHit?.Invoke();
            if (health <= 0)
            {
                Destroy(gameObject);
                onPlayerDeath?.Invoke();
            }
        }
    }
}

