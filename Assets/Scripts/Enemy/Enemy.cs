using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Generic Info
    public float _health;
    private float health
    {
        get { return _health; }
        set{ _health = value; }
    }
    public float _speed;
    private float speed
    {
        get { return _speed; }
        set{ _speed = value; }
    }
    public Rigidbody2D enemyBody;

    // Attacking Variables
    public float _attackRate;
    private float attackRate
    {
        get { return _attackRate; }
        set{ _attackRate = value; }
    }

    // Player Detection Variables
    public Transform player;
    public bool _playerDetected;
    private bool playerDetected
    {
        get { return _playerDetected; }
        set{ _playerDetected = value; }
    }
    public string PLAYER_TAG = "Player";
    public string PLAYER_ATTACK = "PlayerProjectile";

    // Animation
    public Animator anim;
    public string WALKING = "Walking";
    public string ATTACKING = "Attacking";

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody2D>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Die();
    }

    public virtual void Move()
    {
        if (playerDetected)
        {

            if (transform.position.x > player.position.x) // Player on left
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                enemyBody.linearVelocity = new Vector2(-1 * speed, enemyBody.linearVelocityY);
            }
            else // Player on right
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                enemyBody.linearVelocity = new Vector2(1 * speed, enemyBody.linearVelocityY);
            }
        }
        else
        {
            enemyBody.linearVelocity = new Vector2(0, 0);
        }
    }

    public virtual void Attack()
    {

    }

    public void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Is hit by player attack
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_ATTACK))
        {
            health -= 1;
        }
    }

    // Player enters or leaves detection range
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            playerDetected = true;
            anim.SetBool(WALKING, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            playerDetected = false;
            anim.SetBool(WALKING, false);
        }

    }
}
