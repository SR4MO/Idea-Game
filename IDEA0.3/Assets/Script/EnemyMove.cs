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

    //재귀함수. 자기자신을 또 호출함 //딜레이 없이 재귀함수를 사용하는 것은 아주 위험.
    void Think()
    {
        nextMove = Random.Range(-1, 2); //(-1 0 1)
        Invoke("Think", 5); //delay
    }
}
