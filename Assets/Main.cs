using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject pointer,projectile,gun,gunPosition;
    public Text scoreUi;
    public static int score;
    int scoreShow;
    AudioSource audio;


    void Start()
    {
        Application.targetFrameRate = 300;
        audio = GetComponent<AudioSource>();
        //StartCoroutine(scoreUpdate());
    }

    IEnumerator scoreUpdate()
    {
        while (true)
        {
            if(score > 0)
            {
                for (int i = 0; i < score; i += 10)
                {
                    scoreShow += i;
                    yield return new WaitForSeconds(0.01f);
                    scoreUi.text = scoreShow.ToString();
                }
            }
           
        }
       


    }

    bool shot;
    float shotY = -0.1f, speed = 0.1f;


    Projectile createdProjectile;
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pointer.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);


        if (createdProjectile)
        {
            createdProjectile = null;
            gun.transform.up = Vector3.MoveTowards(transform.up, pointer.transform.position, 1);

        }

        if(shot)
        {
            if(gun.transform.position.y != shotY)
            {
                gun.transform.localPosition = Vector3.MoveTowards(gun.transform.localPosition, new Vector3(gun.transform.localPosition.x, shotY,0), speed * Time.deltaTime);
            }
            else
            {
                gun.transform.localPosition = Vector3.MoveTowards(gun.transform.localPosition, new Vector3(gun.transform.localPosition.x, 0, 0), speed * Time.deltaTime);
            }
        }



        if (Input.GetMouseButtonDown(0))
        {
            shot = true;
                    /*
          Vector3 current = gun.transform.forward;
          Vector3 to = pointer.transform.position - gun.transform.position;
          float speed = 12 * Time.deltaTime; 
          gun.transform.forward = Vector3.RotateTowards(current, to, speed, speed);
          */

            CreateProjectile();
        }
      

        scoreUi.text = score.ToString();
    }

    public void CreateProjectile()
    {
        audio.Play();

        createdProjectile = Instantiate(projectile, gunPosition.transform.position, gun.transform.localRotation).GetComponent<Projectile>();

        createdProjectile.target = pointer.transform.position;
    }
}
