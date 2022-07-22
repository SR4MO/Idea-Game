using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
    //������ �ִϸ��̼��� ���� ��
    public SkeletonAnimation skeletonAnimation; 
    public AnimationReferenceAsset[] AnimClip;

    //�ִϸ��̼ǿ� ���� Enum (����)
    public enum AnimState
    {
        Idle,Walk
    }

    //���� �ִϸ��̼� ó���� ���������� ���� ����
    private AnimState _AnimState;

    //���� � �ִϸ��̼��� ����ǰ� �ִ����� ���� ����
    private string CurrentAnimation;

    //����ó��
    private Rigidbody2D rig;
    private float xx;

    private void Awake() => rig = GetComponent<Rigidbody2D>();

    private void Update()
    {
        xx = Input.GetAxisRaw("Horizontal");
        if (xx == 0f)
            _AnimState = AnimState.Idle;
        else
        {
            _AnimState = AnimState.Walk;
            transform.localScale = new Vector2(xx, 1);
        }

        //�ִϸ��̼� ����
        //~~~~(�Լ� �־��)
    }

    private void FixedUpdate()=>
        rig.velocity = new Vector2(xx * 300 * Time.deltaTime, 
            rig.velocity.y);
    //Ŀ�����Լ�
    private void _AsyncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        //�ش� �ִϸ��̼����� �����Ѵ�.
        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
    }

    private void SetCurrentAnimation(AnimState _state)
    {
        switch (_state)
        {
            case AnimState.Idle:
                _AsyncAnimation(AnimClip[(int)AnimState.Idle],true,1f);
                break;
            case AnimState.Walk:
                _AsyncAnimation(AnimClip[(int)AnimState.Idle], true, 1f);
                break;
        }


        _AsyncAnimation(AnimClip[(int)_state], true, 1f);

    }
}
