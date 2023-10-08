using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    public Camera subCamera; // ����ī�޶� ����

    void Start()
    {
        // ����ī�޶� �ʱ� ���� (��Ȱ��ȭ)
        subCamera.enabled = false;
    }

    public void SubCamOn()
    {
        subCamera.enabled = true;
        // ���� �� ��ġ�� ũ�� ���� (��: ���� �ϴ�)
        subCamera.rect = new Rect(0.4f, 0.0f, 0.6f, 0.6f);
    }
    public void SubCamNo()
    {
        subCamera.enabled = false;
    }
}
