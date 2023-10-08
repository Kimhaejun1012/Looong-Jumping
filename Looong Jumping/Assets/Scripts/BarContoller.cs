using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarContoller : MonoBehaviour
{
    public Image bar;
    public RectTransform button;

    public float angleValue = 0f;

    // Update is called once per frame
    void Update()
    {
        HealthChange(angleValue);
    }

    void HealthChange(float angleValue)
    {
        float amount = (angleValue / 100f) * 90.0f / 360;
        bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3 (0,0, -buttonAngle);
    }
}
