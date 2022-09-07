using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEditor;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    public float maxSpeed;
    Rigidbody2D rigid;
    private Animator _animator;
    SkeletonMecanim skeleton;
    float h;
    float inputX;

    void Awake()//초기화
    {
        _animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<SkeletonMecanim>();
    }

    void Update() //단발적인 키 입력
    {

        MoveStop();
        Animation();

    }

    void FixedUpdate() // 꾹 누르거나...
    {
        Move();
    }

    public void Move()
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
        //Max Speed
        if (rigid.velocity.x > maxSpeed) //Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);



    }
    public void Animation()
    {
        var xx = Input.GetAxisRaw("Horizontal"); //왼쪽 -1, 오른쪽 1 , 키를 때면 0
        _animator.SetFloat("move", Mathf.Abs(xx));//mathf.abs는 음수를 양수로 리턴
        var xrun = Input.GetKeyDown(KeyCode.LeftControl);
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
