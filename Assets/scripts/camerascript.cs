using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    public GameObject Jhon;
   
    void Update()
    {
        if (Jhon == null)
        {
            return;
        }
        Vector3 poistion = transform.position;
        poistion.x = Jhon.transform.position.x;
        transform.position = poistion;
    }
}
