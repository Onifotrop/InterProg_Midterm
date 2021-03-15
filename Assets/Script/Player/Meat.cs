using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Meat : MonoBehaviour
{

    public float forceSpeed;
    public SpriteRenderer sR;
    public float minTime;
    public float maxTime;
    
    float timer;

    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
        sR = this.GetComponent<SpriteRenderer>();
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-forceSpeed,forceSpeed),Random.Range(-forceSpeed,forceSpeed)),ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= duration * Time.deltaTime;
        sR.color = new Color(sR.color.r, sR.color.g, sR.color.b,timer);
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
