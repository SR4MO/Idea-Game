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

    void Awake()//�ʱ�ȭ
    {
        _animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        skeleton = GetComponent<SkeletonMecanim>();
    }

    void Update() //�ܹ����� Ű �Է�
    {

        MoveStop();
        Animation();

    }

    void FixedUpdate() // �� �����ų�...
    {
        Move();
    }

    public void Move()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal"); //���� ��� ����
        //int hNum = 0;
        if (h != 0) //������ ��. ���� �ٲ�
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
        var xx = Input.GetAxisRaw("Horizontal"); //���� -1, ������ 1 , Ű�� ���� 0
        _animator.SetFloat("move", Mathf.Abs(xx));//mathf.abs�� ������ ����� ����
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
            // rigid.velocity.normalized ����ũ�⸦ 1�� �ٲ� //���� ���� �� getAxisRaw�� �����
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); //���� ������ �Ѵ� ó��

        }
        //Freeze �̲����� ����
        inputX = Input.GetAxis("Horizontal"); // -1f ~1f �ޱ�
        if (inputX == 0)
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


}
