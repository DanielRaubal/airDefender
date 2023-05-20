using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    ParticleSystem particle;
    public int hp;
    Animator animator;
    public float speed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(hp<=0)
        {
            particle.Play();
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y-5,0), speed * Time.deltaTime);

            //animator.SetBool("destroy", true);
        }
    }
}
