using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEditor;
using UnityEngine;
using System;

//코드 최적화하자....
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

    void Update() //단발적인 키 입력
    {
        MoveStop();
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Update();
        }
        keys[0].UpdateAction(() => Move(), () => run());
        keys[1].UpdateAction(() => Move(), () => run());
        /*
        keys[0].UpdateAction(() => Debug.Log("W"), () => Debug.Log("WW"));
        keys[1].UpdateAction(() => Debug.Log("A"), () => Debug.Log("AA"));
        keys[2].UpdateAction(() => Debug.Log("S"), () => Debug.Log("SS"));
        keys[3].UpdateAction(() => Debug.Log("D"), () => Debug.Log("DD"));
        */

        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        }


        Animation();

    }

    void FixedUpdate() // 꾹 누르거나...
    {
        

        //Move();
    }

    public void Move()
    {
        //Move By Key Control
        var h = Input.GetAxisRaw("Horizontal"); //음수 양수 받음
        //int hNum = 0;
        if (h != 0) //움직일 때. 방향 바꿈
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
        //walk
        //_animator.SetInteger("move 0", (int)(Mathf.Abs(h)));


        //Max Speed
        if (rigid.velocity.x > maxSpeed) //Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        isRunning = false;

    }
    public void run()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal"); //음수 양수 받음
        //int hNum = 0;
        if (h != 0) //움직일 때. 방향 바꿈
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
    public void Jump()
    {
        
    }

    public void Animation()
    {
        //walk, idle animation
        float xx = Input.GetAxisRaw("Horizontal"); //왼쪽 -1, 오른쪽 1 , 키를 때면 0
        //_animator.SetFloat("move", Mathf.Abs(xx));//mathf.abs는 음수를 양수로 리턴
        if(isRunning == false) _animator.SetInteger("move 0", (int)(Mathf.Abs(xx))); 
        else _animator.SetInteger("move 0", (int)(Mathf.Abs(xx) * 2));
        //attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _animator.SetTrigger("attack");

        }
        //ataack2
        if (Input.GetKeyDown(KeyCode.X))
        {
            _animator.SetTrigger("attack2");
        }
        //run
        //if (rigid.velocity.x < (maxSpeed * 2) || rigid.velocity.x < (maxSpeed * (-1)) * 2)
        //{
        //    _animator.SetInteger("move 0", (int)(Mathf.Abs(xx)*2));
        //}



    }
    public void MoveStop()
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            // rigid.velocity.normalized 백터크기를 1로 바꿈 //단위 구할 때 getAxisRaw와 비슷함
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); //왼쪽 오른쪽 둘다 처리

        }
        //Freeze 미끄러짐 방지
        inputX = Input.GetAxis("Horizontal"); // -1f ~1f 받기
        if (inputX == 0)
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


}


