﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // IF ALIVE, THEN DO THE PLAYER BEHAVIORS
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
    public Vector3 respawnVector ;
    //Bool used for acid
    public static bool inAcid = false;
    public static bool inLand = false;
    //Animation stuff
    public Animator playerAnim;
    //Prefabs
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

    public CircleCollider2D playerHitbox;
    public SpriteRenderer sR;
    public Animator animator;

    private PlayerStateBase currentState;
    public PlayerStateAlive stateAlive = new PlayerStateAlive();
    public PlayerStateDie stateDie = new PlayerStateDie();

    public AudioSource aS;
    public AudioClip aC;
    public AudioClip spawn;
    public AudioClip die;
    public AudioClip dash;
    private void Awake()
    {
        playerHitbox = player.GetComponent<CircleCollider2D>();
        sR = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        alive = true;
        aC = aS.clip;
    }

    void Start()
    {
        //Initialize prefabs
        //alive = true;
        canPress = true;
        isDash = false;
        respawnVector = new Vector3(0, 0, 0);
        ChangeState(stateAlive);
    }
    
    void Update()
    {
        debugPlace();
        currentState.Update(this);
    }

    public void debugPlace()
    {
        Debug.Log("Acid = " + inAcid);
        Debug.Log("Land = " + inLand);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDash == false)
        {
            if (other.tag == "Spike")
            {
                ChangeState(stateDie);
            }

            if (inLand == false && inAcid == true)
            {
                print("acid die");
                ChangeState(stateDie);
            }
        }
        
        if (other.tag == "Respawn")
        {
            respawnVector = other.transform.position;
        }
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            canPress = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            canPress = true;
        }
    }

    public void dieSpawnChunks()
    {
        alive = false;
        aC = die;
        aS.Play();
        //animator.enabled = false;
        sR.color = Color.red;
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
    }
    public void moveWithWASD()
    {
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
    public void dashWithX()
    {
        if (canPress && Input.GetKey(KeyCode.X))
        {
            aC = dash;
            aS.Play();
            counter += 1 * Time.deltaTime;
            if (counter < dashTime)
            {
                isDash = true;
                dashSpeed = dashSpeeds;
                playerHitbox.enabled = false;
                sR.color = Color.blue;
            }
            else
            {
                isDash = false;
                dashSpeed = 0f;
                playerHitbox.enabled = true;
                sR.color = Color.white;
            }
        }
        else
        {
            isDash = false;
            dashSpeed = 0;
            playerHitbox.enabled = true;
            sR.color = Color.white;
            canPress = true;
            counter = 0;
        }
    }
    public void ChangeState(PlayerStateBase newState)
    {
        if (currentState != null)
        {
            currentState.LeaveState(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    public IEnumerator BlowUp()
    {
        dieSpawnChunks();
        sR.enabled = false;
        yield return new WaitForSeconds(0f);
    }

    public void respawn()
    {
        alive = true;
        sR.enabled = true;
        ChangeState(stateAlive);
        this.transform.position = respawnVector;
    }
}
