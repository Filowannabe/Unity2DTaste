using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gruntscript : MonoBehaviour
{
    public GameObject Jhon;
    public GameObject BulletPrefab;
    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private double limit;
    private double negativelimit;
    public bool isBigger = false;
    public bool isWatching = true;
    private float LastShoot;
    private int Health = 3;
    public float Speed;

    void Start()
    {
        limit = transform.position.x + 0.2;
        negativelimit = limit - 0.4;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {

        if (Jhon == null)
        {
            return;
        }

        Animator.SetTrigger("idle");
        if (isWatching)
        {
            Vector3 direction = Jhon.transform.position - transform.position;


            //else Debug.Log("Estas lejos");

            if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }


        float distance = Mathf.Abs(Jhon.transform.position.x - transform.position.x);



        if (distance < 1.0f && Time.time > LastShoot + 0.55f)
        {
            Animator.SetTrigger("running");
            Shoot();
            LastShoot = Time.time;
        }
    }
    private void FixedUpdate()
    {

        if (Jhon == null)
        {
            return;
        }

        SetEnemyScale();
    }

    public void SetEnemyScale()
    {
        Animator.SetTrigger("running");
        Vector3 direction = Jhon.transform.position - transform.position;
        if (direction.x >= 0.0f)
        {

            if (!isBigger) goRight();
            
            if (transform.position.x > limit)
            {
                goLeft();
                isBigger = true;
                isWatching = false;
            }

            if (transform.position.x < negativelimit)
            {
                goRight();
                isBigger = true;
                isWatching = false;
            }

            if (isBigger)
            {
                goLeft();

                if (transform.position.x < (negativelimit))
                {
                    goRight();
                    isBigger = false;
                }
            }
        }
        else
        {

            if (!isBigger) goLeft();

            if (transform.position.x > limit)
            {
                goLeft();
                isBigger = true;
                isWatching = false;
            }

            if (transform.position.x < negativelimit)
            {

                goRight();
                isBigger = true;
                isWatching = false;
            }

            if (isBigger)
            {
                goRight();

                if (transform.position.x > limit)
                {
                    goLeft();
                    isBigger = false;

                }
            }
        }
    }
    private void Shoot()
    {

        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<bulletscript>().setDirection(direction);
    }
    public void Hit()
    {
        Health--;
        if (Health == 0) Destroy(gameObject);

    }
    private void goRight()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Rigidbody2D.velocity = new Vector2(1f * Speed, Rigidbody2D.velocity.y);
    }
    private void goLeft()
    {
        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        Rigidbody2D.velocity = new Vector2(1f * (Speed * -1), Rigidbody2D.velocity.y);
    }
}