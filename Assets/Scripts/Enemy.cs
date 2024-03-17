using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxMove = 10f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float health = 1f;

    private Vector3 startPos;
    private float direction = 1f;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private bool alive = true;
    private Animator animator;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (alive)
        {
            Vector3 min = boxCollider.bounds.min;
            Vector3 max = boxCollider.bounds.max;

            Vector2 corner1 = new(max.x, min.y - 0.1f);
            Vector2 corner2 = new(min.x, min.y - 0.1f);

            Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

            bool grounded = false;
            if (hit) grounded = true;

            if (Random.Range(0, 1000) == 1)
            {
                direction *= -1;
            }
            if (Random.Range(0, 1000) == 1 && grounded)
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            Vector2 movement = new(speed * direction, body.velocity.y);
            body.velocity = movement;
            float difference = startPos.x - transform.position.x;
            if (Mathf.Abs(difference) >= maxMove)
            {

                direction = Mathf.Sign(difference);
            }

            transform.localScale = new Vector3(-2 * direction, 2, 2);
        }

        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }
    private void Kill()
    {
        alive = false;
        transform.eulerAngles = new Vector3(0, 0, 180);
        Destroy(boxCollider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            Vector2 contact = collision.GetContact(0).normal;
            if (contact.x == 0) {
                Hurt(1);
            } else
            {
                player.Kill();
            }
        }
    }

    private void Hurt(int amount)
    {
        animator.Play("boss_hurt");
        health -= amount;
        if (health <= 0)
        {
            Kill();
        }
    }
}
