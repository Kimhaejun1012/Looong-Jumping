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
        //acceleration = 0.01f;
        //jumpingPower = 100f;
        acceleration = PlayerPrefs.GetFloat(accelerationKey, 0.1f);
        jumpingPower = PlayerPrefs.GetFloat(jumpingPowerKey, 100f);
        money = PlayerPrefs.GetInt(moneyKey, 0);
    }
}

public class ShopData
{
    public int jumpPowerPurchased = 0;
    public int accelerationPurchased = 0;
}

public class GameData
{
    public PlayerInfo playerInfo;
    public ShopData shopData;
}