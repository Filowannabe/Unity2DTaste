using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingscript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float Speed;
    Vector3 nextPlace;
    public Transform start, end;
    private Vector3 MoveTo;

    void Start()
    {

        MoveTo = end.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, MoveTo, Speed * Time.deltaTime);
        if (transform.position == end.position)
        {
            MoveTo = start.position;
        }
        else if (transform.position == start.position)
        {
            MoveTo = end.position;
        }
    }
}
