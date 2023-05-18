using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public ParticleSystem particle;
    public int hp;


    void Update()
    {
        if(hp<=0)
        {
            particle.Play();
        }
    }
}
