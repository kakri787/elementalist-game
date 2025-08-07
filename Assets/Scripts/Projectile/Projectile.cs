using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float maxRange;

    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile
        transform.position += speed * Time.deltaTime * transform.right;

        // Projectile moves a set distance based on maximum range of projectile
        float distanceTravelled = Vector3.Distance(startPos, transform.position);
        if (distanceTravelled > maxRange)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
