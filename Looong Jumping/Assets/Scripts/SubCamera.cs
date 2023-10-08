using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    public Camera subCamera; // 서브카메라 참조

    void Start()
    {
        // 서브카메라 초기 설정 (비활성화)
        subCamera.enabled = false;
    }

    public void SubCamOn()
    {
        subCamera.enabled = true;
        // 서브 뷰 위치와 크기 조절 (예: 우측 하단)
        subCamera.rect = new Rect(0.4f, 0.0f, 0.6f, 0.6f);
    }
    public void SubCamNo()
    {
        subCamera.enabled = false;
    }
}
