using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // IF ALIVE, THEN DO THE PLAYER BEHAVIORS
    //IF DEAD, CREATE THE BLOOD PREFAB, AND DESTROY CURRENT PLAYER PREFAB
    public bool alive;
    //this is to restrict the use of x, once pressed x for dash, you have to press again for the next time,
    public bool canPress;
    //Customizable player vars
    public float runSpeed;
    float dashSpeed;
    public float counter;
    public float dashSpeeds;
    public float dashTime;

    public bool isDash;
    //public GameManager gameManager;
    //Animation stuff
    public Animator playerAnim;
    //which is myself
    public GameObject player;

    public GameObject bloodPrefab;
    public GameObject splatter;
    public GameObject skullPrefab;
    public GameObject bonePrefab;
    public GameObject brainPrefab;
    public GameObject heartPrefab;
    public GameObject ribsPrefab;
    public GameObject meatPrefab;
    public GameObject veinPrefab;
    public GameObject toothPrefab;
    
    
    //public ParticleSystem bloodBurst;
    void Start()
    {
        //Initialize prefabs
        alive = true;
        canPress = true;
        isDash = false;
    }
    
    void Update()
    {



        
        if (alive)
        {
            if (canPress && Input.GetKey(KeyCode.X))
            {
                counter += 1 * Time.deltaTime;
                if (counter < dashTime)
                {
                    isDash = true;
                    dashSpeed = dashSpeeds;
                    player.GetComponent<CircleCollider2D>().enabled = false;
                    this.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                else
                {
                    isDash = false;
                    dashSpeed = 0f;
                    player.GetComponent<CircleCollider2D>().enabled = true;
                    this.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            else
            {
                isDash = false;
                dashSpeed = 0;
                player.GetComponent<CircleCollider2D>().enabled = true;
                this.GetComponent<SpriteRenderer>().color = Color.white;
                canPress = true;
                counter = 0;
            }
            //Basic player movements, under alive condition
            //UP
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //print("UP");
                playerAnim.SetBool("isUp",true);
                playerAnim.SetBool("faceUp",false);
                transform.Translate(0,(runSpeed + dashSpeed)* Time.deltaTime,0);
            }
            
            //DOWN
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //print("DOWN");
                playerAnim.SetBool("isDown",true);
                playerAnim.SetBool("faceDown",false);
                transform.Translate(0,-(runSpeed + dashSpeed)* Time.deltaTime,0);
            }
            
            //LEFT
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //print("LEFT");
                playerAnim.SetBool("isLeft",true);
                playerAnim.SetBool("faceLeft",false);
                transform.Translate(-(runSpeed + dashSpeed)* Time.deltaTime,0,0);
            }
            
            //RIGHT
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //print("RIGHT");
                playerAnim.SetBool("isRight",true);
                playerAnim.SetBool("faceRight",false);
                transform.Translate((runSpeed + dashSpeed)* Time.deltaTime,0,0);
            }
            
            
            
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                playerAnim.SetBool("isUp",false);
                playerAnim.SetBool("faceUp",true);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                playerAnim.SetBool("isDown",false);
                playerAnim.SetBool("faceDown",true);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerAnim.SetBool("isLeft",false);
                playerAnim.SetBool("faceLeft",true);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                playerAnim.SetBool("isRight",false);
                playerAnim.SetBool("faceRight",true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDash == false)
        {
            if (other.tag == "Disc")
            {
                alive = false;
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<SpriteRenderer>().color = Color.red;
                Instantiate(bloodPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(splatter, this.transform.position, Quaternion.Euler(0, 0, 0));
            
                Instantiate(skullPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(veinPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(brainPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(ribsPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(meatPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(bonePrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(heartPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(toothPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(player);
                //bloodBurst.Play();
            }
        }
    }


//    void OnCollisionEnter2D(Collision2D other)
//    {
//        if (other.gameObject.tag == "Disc")
//        {
//            alive = false;
//            this.GetComponent<Animator>().enabled = false;
//            this.GetComponent<SpriteRenderer>().color = Color.red;
//            Instantiate(bloodPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
//            Destroy(player);
//        }
//    }


    //All physics is here, currently I'm not sure if we need to use fixed update for collision detect or etc, so I'm commenting it out.
//    void FixedUpdate()
//    {
//        throw new NotImplementedException();
//    }
}
