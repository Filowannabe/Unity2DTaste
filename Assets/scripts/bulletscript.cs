using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    public AudioClip Sound;
    private Vector2 Direction;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    void Start()
    {
        Rigidbody2D  = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    
    void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction *Speed;
    }

    public void setDirection(Vector3 direction){
        Direction = direction;

    }
    public void DestroyBullet(){
        
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jhonwalking jhon =collision.GetComponent<Jhonwalking>();
        gruntscript grunt =collision.GetComponent<gruntscript>();

        if (jhon != null)
        {
            jhon.Hit();
        }

        if (grunt != null)
        {
            grunt.Hit();
        }

        DestroyBullet();
    }
   
}
