using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private TMP_Text coinLabel;

    private int coins = 0;

    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new(deltaX, body.velocity.y);
        body.velocity = movement;

        Vector3 min = boxCollider.bounds.min;
        Vector3 max = boxCollider.bounds.max;

        Vector2 corner1 = new(max.x, min.y - 0.1f);
        Vector2 corner2 = new(min.x, min.y - 0.1f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if (hit) grounded = true;

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Platform platform = null;

        if (hit)
        {
            platform = hit.GetComponent<Platform>();
        }

        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        animator.SetFloat("speed", Mathf.Abs(deltaX));

        Vector3 pScale = Vector3.one;

        if (platform)
        {
            pScale = platform.transform.localScale;
        }

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) * 2 / pScale.x, 2 / pScale.y, 2);
        }

        if (transform.position.y <= -10)
        {
            Kill();
        }
    }

    void Kill()
    {
        Debug.Log("player has died");
    }

    public void CollectCoin()
    {
        coins += 1;
        coinLabel.text = coins.ToString();
    }
}
