using UnityEngine;

public class Meteor : Enemy
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float speed = Random.Range(minSpeed, maxSpeed);
        rb.velocity = Vector2.down * speed;
    }

    public override void HurtSequence()
    {
        // do something when hurt
        Debug.Log("Meteor is hurt");
    }

    public override void DieSequence()
    {
        // do something when die
        Debug.Log("Meteor is dead");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            Destroy(collision.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
