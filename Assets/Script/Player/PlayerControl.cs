using System;
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
    //Dash use and basic movement
    public float runSpeed;
    public float dashSpeed;
    public float counter;
    public float dashSpeeds;
    public float dashTime;
    public bool isDash;
    //Used to get the Respawner Place for Checkpoint.
    public Vector3 respawnVector;
    //Bool used for acid and land detect
    public static bool inAcid = false;
    public static bool inLand = false;
    //Animation stuff
    public Animator playerAnim;
    //Prefabs for blow up effect
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
    
    private CircleCollider2D playerHitbox;
    private SpriteRenderer sR;
    public Animator animator;
    //FSM
    private PlayerStateBase currentState;
    public PlayerStateAlive stateAlive = new PlayerStateAlive();
    public PlayerStateDie stateDie = new PlayerStateDie();

    //Audio
    public AudioSource spawn;
    public AudioSource die;
    public AudioSource dash;
    private void Awake()
    {
        //Get Stuff
        playerHitbox = player.GetComponent<CircleCollider2D>();
        sR = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        alive = true;
    }

    void Start()
    {
        //Set up basics
        
        canPress = true;
        isDash = false;
        respawnVector = new Vector3(0, 0, 0);
        ChangeState(stateAlive);
    }
    
    void Update()
    {
        currentState.Update(this);
    }

    //Used for debug
    public void debugPlace()
    {
        //Debug.Log("Acid = " + inAcid);
        //Debug.Log("Land = " + inLand);
        Debug.Log("Vector = " + respawnVector);
        //Debug.Log("Speed = " + dashSpeed);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Detecting different ground, but not when you are dashing
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
        //When you touch the respawn, make it a checkpoint
        if (other.tag == "Respawn")
        {
            respawnVector = other.transform.position;
        }
        
    }
    //for limiting the wall
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

    //Spawning the meat chunks
    public void dieSpawnChunks()
    {
        alive = false;
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
    //Dash to avoid toxic and spikes
    public void dashWithX()
    {
        if (canPress && Input.GetKey(KeyCode.X))
        {
            dash.Play();
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
        die.Play();
        dieSpawnChunks();
        sR.enabled = false;
        yield return new WaitForSeconds(0f);
    }

    public void respawn()
    {
        spawn.Play();
        alive = true;
        sR.enabled = true;
        ChangeState(stateAlive);
        this.transform.position = respawnVector;
    }
}
