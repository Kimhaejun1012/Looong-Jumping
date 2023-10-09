using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float acceleration;
    public float jumpingPower;
    public int money;
    public string moneyKey = "Money";
    public string jumpingPowerKey = "Jumping";
    public string accelerationKey = "Accel";

    private void Awake()
    {
        acceleration = PlayerPrefs.GetFloat(accelerationKey, 0.1f);
        jumpingPower = PlayerPrefs.GetFloat(jumpingPowerKey, 100f);
        money = PlayerPrefs.GetInt(moneyKey, 0);
    }
}

public class SaveShopData
{
    public int jumpPowerPurchased = 0;
    public int accelerationPurchased = 0;
}