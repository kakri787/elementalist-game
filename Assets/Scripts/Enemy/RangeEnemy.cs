using UnityEngine;

public class RangeEnemy : Enemy
{
    // Projectile Variables
    private ProjectileController enemyProjectile;
    public float _projectileSpeed;
    private float projectileSpeed
    {
        get { return _projectileSpeed; }
        set { _projectileSpeed = value; }
    }
    public float _maxRange;
    private float maxRange
    {
        get { return _maxRange; }
        set{ _maxRange = value; }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody2D>();
        enemyProjectile = GetComponentInChildren<ProjectileController>();
    }

    public override void Move()
    {
        if (_playerDetected)
        {
            if (transform.position.x - maxRange > player.position.x) // Player on left
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                enemyBody.linearVelocity = new Vector2(-1 * _speed, enemyBody.linearVelocityY);
            }
            else if (transform.position.x + maxRange < player.position.x)// Player on right
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                enemyBody.linearVelocity = new Vector2(1 * _speed, enemyBody.linearVelocityY);
            }
            else
            {
                enemyBody.linearVelocity = new Vector2(0, 0);
            }
        }
    }

    public override void Attack()
    {
        if (_playerDetected)
        {
            anim.SetBool(ATTACKING, true);
            enemyProjectile.SpawnProjectile(_attackRate, projectileSpeed * -1, maxRange, transform.rotation);
        }
        else
        {
            anim.SetBool(ATTACKING, false);
        }
    }
}
