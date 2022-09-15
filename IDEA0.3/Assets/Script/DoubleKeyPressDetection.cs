using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class DoubleKeyPressDetection : MonoBehaviour
{

    public KeyCode Key { get; private set; }

    // �� �� ������ ������ ����
    public bool SinglePressed { get; private set; }

    // �� �� ������ ������ ����
    public bool DoublePressed { get; private set; }

    private bool doublePressDetected;
    private float doublePressThreshold; //�Ѱ���?
    private float lastKeyDownTime; //���� �ֱ�(������) Ű���� �ð�

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
        doublePressThreshold = seconds > 0f ? seconds : 0f; // ������ ���� 0f���� ũ�� �����°��� �ְ� �ƴϸ� 0f
    }



    // MonoBehaviour.Update()���� ȣ�� : Ű ���� ������Ʈ
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

    // MonoBehaviour.Update()���� ȣ�� : Ű �Է¿� ���� ����
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


