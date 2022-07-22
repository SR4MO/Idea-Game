using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
    //스파인 애니메이션을 위한 것
    public SkeletonAnimation skeletonAnimation; 
    public AnimationReferenceAsset[] AnimClip;

    //애니메이션에 대한 Enum (별명)
    public enum AnimState
    {
        Idle,Walk
    }

    //현재 애니메이션 처리가 무엇인지에 대한 변수
    private AnimState _AnimState;

    //현재 어떤 애니메이션이 재생되고 있는지에 대한 변수
    private string CurrentAnimation;

    //무브처리
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

        //애니메이션 적용
        //~~~~(함수 넣어라)
    }

    private void FixedUpdate()=>
        rig.velocity = new Vector2(xx * 300 * Time.deltaTime, 
            rig.velocity.y);
    //커스텀함수
    private void _AsyncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        //해당 애니메이션으로 변경한다.
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
