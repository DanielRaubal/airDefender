using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingProjectile : MonoBehaviour
{
    public List<Building> buildings;
    Building fallOnTo;
    public float speed;
    public GameObject fallingProjectile;
    public float splitTo = 0,splitTimer=0.45f;
    bool destroy;
    public float rotationModifier;
    public FloatTextScript floatText;

    void Start()
    {
        fallOnTo = buildings[Random.Range(0, buildings.Count)];


        /*
        if(splitTo > 0)
        {
            StartCoroutine(split());
        }*/
    }

    IEnumerator split()
    {
        yield return new WaitForSeconds(splitTimer);
        for (int i = 0; i < splitTo; i++)
        {
            FallingProjectile projectile
             = Instantiate(fallingProjectile, transform.position, fallingProjectile.transform.rotation).GetComponent<FallingProjectile>();

            projectile.buildings = buildings;

        }
    }


    void Update()
    {
        //transform.up = Vector3.MoveTowards(transform.up, fallOnTo.transform.position, 1);

        //float angle = Mathf.Atan2(fallOnTo.transform.position.y - transform.position.y, fallOnTo.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1 * Time.deltaTime);

        if(!destroy)
        {
            Vector3 direction = fallOnTo.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 11 * Time.deltaTime);


            if(transform.position == fallOnTo.transform.position || !fallOnTo || buildings.Count < 1)
            {

                GetComponent<ParticleSystem>().Play();
                GetComponent<SpriteRenderer>().sprite = null;
                fallOnTo.GetComponent<Building>().hp--;
                destroy = true;
                StartCoroutine(destroyMe());

            }
            transform.position = Vector3.MoveTowards(transform.position, fallOnTo.transform.position, speed * Time.deltaTime);
        }
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name);
        if(!collision.gameObject.name.Contains("Falling") && !destroy)
        {
            if(collision.gameObject.name.Contains("Building"))
            {
                collision.gameObject.GetComponent<Building>().hp--;
            }

            GetComponent<ParticleSystem>().Play();
            GetComponent<SpriteRenderer>().sprite = null;

            if(collision.gameObject.name.Contains("Projectile"))
            {
                int scoreAdd = 100;
                Main.score += scoreAdd;
                floatText.score = scoreAdd;

            }
            floatText.score = -1;

            destroy = true;
            StartCoroutine(destroyMe());
        }
    }

    IEnumerator destroyMe()
    {

        floatText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.95f);
        Destroy(gameObject);

    }


}
