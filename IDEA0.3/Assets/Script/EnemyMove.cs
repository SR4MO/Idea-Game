using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); //delay
    }

    void FixedUpdate() //(1/50)
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    //����Լ�. �ڱ��ڽ��� �� ȣ���� //������ ���� ����Լ��� ����ϴ� ���� ���� ����.
    void Think()
    {
        nextMove = Random.Range(-1, 2); //(-1 0 1)
        Invoke("Think", 5); //delay
    }
}
