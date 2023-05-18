using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Vector3 start, end;
    public float speed;


    void Update()
    {
        if(transform.position.x != end.x)
        {
            transform.position
  = Vector3.MoveTowards(transform.position, new Vector3(end.x, transform.position.y, 0), speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(start.x,transform.position.y,0);
        }
    }
}
