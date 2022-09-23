using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEditor;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{

    private DoubleKeyPressDetection[] keys;

    public float maxSpeed;
    public float jumpPower;
    public float runSpeed;
    Rigidbody2D rigid;
    private Animator _animator;
    SkeletonMecanim skeleton;
    float h;
    float inputX;
    bool isRunning;
    

    
    private void Start()
    {
        keys = new[]
        {        
        new DoubleKeyPressDetection(KeyCode.LeftArrow),
        new DoubleKeyPressDetection(KeyCode.RightArrow),
    };
    }
    

    void Awake()//초기화
    {
        _animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<SkeletonMecanim>();
    }

    void Update()
    {
        //walk, run
        MoveStop();
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Update();
        }
        keys[0].UpdateAction(() => Move(), () => run());
        keys[1].UpdateAction(() => Move(), () => run());
        //jump
        if (Input.GetButtonDown("Jump") && !_animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            _animator.SetBool("isJumping", true);
            
        }

        Animation();

    }

    void FixedUpdate()
    {
        //RayCast
        //Landing Platform
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if(rayHit.collider != null)
            {
                if(rayHit.distance < 5f)
                {
                    Debug.Log(rayHit.collider.name);
                    _animator.SetBool("isJumping", false);
                }
            }
        }
    }

    public void Move()
    {
        //Move By Key Control
        var h = Input.GetAxisRaw("Horizontal"); // 0, 1, -1
        //int hNum = 0;
        if (h != 0) //moving, derection
        {
            _animator.SetBool("isWalking", true);
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            if (h > 0)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            else
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
        


        //Max Speed
        if (rigid.velocity.x > maxSpeed) //Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        isRunning = false;
        _animator.SetBool("isWalking", false);

    }
    public void run()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");
        //int hNum = 0;
        if (h != 0)
        {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            if (h > 0)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            else
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
        //run


        //Max Speed
        if (rigid.velocity.x > maxSpeed) //Right Max Speed
            rigid.velocity = new Vector2(maxSpeed*runSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //left Max Speed
            rigid.velocity = new Vector2((maxSpeed * (-1))*runSpeed, rigid.velocity.y);

        //run animation
        isRunning = true;

    }

    public void Animation()
    {
        //walk, idle animation
        float xx = Input.GetAxisRaw("Horizontal"); 
        if(isRunning == false) _animator.SetInteger("move 0", (int)(Mathf.Abs(xx))); 
        else _animator.SetInteger("move 0", (int)(Mathf.Abs(xx) * 2));
        //attack
        if (Input.GetKeyDown(KeyCode.Z) && _animator.GetBool("isJumping") == false)
        {
            if (_animator.GetInteger("move 0") == 0) _animator.SetTrigger("attack");
            else if (_animator.GetInteger("move 0") == 1) _animator.SetTrigger("walk_attack");
            else if (_animator.GetInteger("move 0") == 2) _animator.SetTrigger("run_attack");
        }
        //jump_attack
        if (Input.GetKeyDown(KeyCode.Z) && _animator.GetBool("isJumping") == true)
        {
            _animator.SetTrigger("jump_attack");
        }
         //ataack2
        if (Input.GetKeyDown(KeyCode.X))
        {
            _animator.SetTrigger("attack2");
        }
        
        
        //rungattack

    }
    public void MoveStop()
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            // rigid.velocity.normalized ...Vector--> 1 //단위 구할 때 getAxisRaw와 비슷함
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); //왼쪽 오른쪽 둘다 처리

        }
        //Freeze 
        inputX = Input.GetAxis("Horizontal"); // -1f ~1f 
        if (inputX == 0)
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


}


