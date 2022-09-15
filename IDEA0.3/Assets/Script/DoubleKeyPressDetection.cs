using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class DoubleKeyPressDetection : MonoBehaviour
{

    public KeyCode Key { get; private set; }

    // 한 번 눌러서 유지한 상태
    public bool SinglePressed { get; private set; }

    // 두 번 눌러서 유지한 상태
    public bool DoublePressed { get; private set; }

    private bool doublePressDetected;
    private float doublePressThreshold; //한계점?
    private float lastKeyDownTime; //제일 최근(마지막) 키누른 시간

    public DoubleKeyPressDetection(KeyCode key, float threshold = 0.3f)
    {
        this.Key = key;
        SinglePressed = false;
        DoublePressed = false;
        doublePressDetected = false;
        doublePressThreshold = threshold;
        lastKeyDownTime = 0f;
    }
    public void ChangeKey(KeyCode key)
    {
        this.Key = key;
    }
    public void ChangeThreshold(float seconds)
    {
        doublePressThreshold = seconds > 0f ? seconds : 0f; // 들어오는 값이 0f보다 크면 들어오는값을 넣고 아니면 0f
    }



    // MonoBehaviour.Update()에서 호출 : 키 정보 업데이트
    public void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            doublePressDetected =
                (Time.time - lastKeyDownTime < doublePressThreshold);
            lastKeyDownTime = Time.time;
        }

        if (Input.GetKey(Key))
        {
            if (doublePressDetected)
                DoublePressed = true;
            else
                SinglePressed = true;
        }
        else
        {
            doublePressDetected = false;
            DoublePressed = false;
            SinglePressed = false;
        } 
    }

    // MonoBehaviour.Update()에서 호출 : 키 입력에 따른 동작
    public void UpdateAction(Action singlePressAction, Action doublePressAction)
    {
        if (SinglePressed) singlePressAction?.Invoke();
        if (DoublePressed) doublePressAction?.Invoke();
    }




    /*
    private DoubleKeyPressDetection[] keys;

    private void Start()
    {
        keys = new[]
        {
        new DoubleKeyPressDetection(KeyCode.W),
        new DoubleKeyPressDetection(KeyCode.A),
        new DoubleKeyPressDetection(KeyCode.S),
        new DoubleKeyPressDetection(KeyCode.D),
    };
    }

    private void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Update();
        }

        keys[0].UpdateAction(() => Debug.Log("W"), () => Debug.Log("WW"));
        keys[1].UpdateAction(() => Debug.Log("A"), () => Debug.Log("AA"));
        keys[2].UpdateAction(() => Debug.Log("S"), () => Debug.Log("SS"));
        keys[3].UpdateAction(() => Debug.Log("D"), () => Debug.Log("DD"));
    }
    */
}


