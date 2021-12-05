using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jhonwalking : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private float horizontal;
    private Animator Animator;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5;

    void Start()
    {

        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0.0f)
        {
            Animator.SetTrigger("Running");
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontal > 0.0f)
        {
            Animator.SetTrigger("Running");
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            Animator.SetTrigger("Idle");
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.15f)) Grounded = true;
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.Space) && Grounded) Jump();
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }
    
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<bulletscript>().setDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        Health--;
        Debug.Log("Your health: " + Health);
        if (Health <= 0) Animator.SetTrigger("Dying");

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "moveplat")
        {

            transform.parent = collision.transform;
        }
    }
    
    public void getDyingMode()
    {
        Destroy(gameObject);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "moveplat")
        {
            transform.parent = null;
            Speed = 1;
        }

    }
}
