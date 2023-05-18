using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    public Sprite boom;
    public SpriteRenderer sr;
    public double angle, turnRadians, turnAngle;
    ParticleSystem particles;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        float diagonal = Mathf.Sqrt(Mathf.Pow(target.x, 2) + Mathf.Pow(target.y, 2));

        particles = GetComponent<ParticleSystem>();
        angle = Mathf.Atan(target.x/ target.y);
        //transform.eulerAngles = new Vector3(0, 0, (float)(-angle / 2)*100);
        //transform.up = Vector3.MoveTowards(transform.up, target, 1);




        //turnRadians = Mathf.Asin(target.y / transform.position.x);
        //angle = Mathf.Rad2Deg * turnRadians;
        /*
        turnAngle = angleBetweenTurretAndTarget - angleBetweenTurretAndGun;
        transform.Rotate(Vector3.up, turnAngle);*/

        /*
        Vector3 targ = target;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/
    }







    // Update is called once per frame

    [Header("Scale attributes")]
    bool scale;
    public float scaleWith,scaleSpeed,opacityDecrease;
    public Vector3 localScale;
    IEnumerator scaler()
    {
        localScale = transform.localScale;

        audio.Play();
        yield return new WaitForSeconds(0.15f);
        Color color;
        ColorUtility.TryParseHtmlString("#61001d", out color);
        sr.color = color;
        yield return new WaitForSeconds(0.1f);

        ColorUtility.TryParseHtmlString("#b9937c", out color);
        sr.color = color;

        while (transform.localScale.x < 1)
        {
            float scaleTo = transform.localScale.x + scaleWith;

            transform.localScale = new Vector3(scaleTo, scaleTo, 0);
            yield return new WaitForSeconds(scaleSpeed);

        }

        CircleCollider2D coll = GetComponent<CircleCollider2D>();
        coll.radius = 1.5f;
        yield return new WaitForSeconds(0.2f);
        coll.enabled = false;


        /*
        int A = 255;
        while (A > 0)
        {
            yield return new WaitForSeconds(0.1f);

            sr.color = new Color32(System.Convert.ToByte((int)color.r), System.Convert.ToByte((int)color.g), System.Convert.ToByte(color.b), (byte)A);
            //sr.color = new Color32((byte)(int)color.r, (byte)(int)color.g, (byte)(int)color.b, (byte)A);
            //sr.color = new Color(color.r, color.g, color.b, A);

            //sr.color = ConverColor((int)sr.color.r, (int)sr.color.g, (int)sr.color.b, A);
            //Debug.Log($"{sr.color.a}|{A}");
            A -= 15;
        }*/

        while (transform.localScale.x > 0)
        {
            float scaleTo = transform.localScale.x - scaleWith;

            transform.localScale = new Vector3(scaleTo, scaleTo, 0);
            yield return new WaitForSeconds(scaleSpeed);

        }

        Destroy(gameObject);
        
    }

    Color ConverColor(int r,int g,int b,int a)
    {
        return new Color(r/ 255, g / 255, b / 255,a/255);
    }


void Update()
    {
        transform.up = Vector3.MoveTowards(transform.up, target,1);
        //


        if (!scale)
        {
            if (transform.position == target)
            {
                sr.sprite = boom;
                scale = true;
                CameraShake.shake = true;
                particles.Stop();
                StartCoroutine(scaler());
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }


    }
}
