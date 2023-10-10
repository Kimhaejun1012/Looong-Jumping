using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerData
{
    public float acceleration = 0.01f;
    public float jumpingPower = 100f;
    public int gold = 0;

    //public float acceleration;
    //public float jumpingPower;
    //public int money;
    //public string moneyKey = "Money";
    //public string jumpingPowerKey = "Jumping";
    //public string accelerationKey = "Accel";

    //private void Awake()
    //{
    //    //acceleration = 0.01f;
    //    //jumpingPower = 100f;
    //    //acceleration = PlayerPrefs.GetFloat(accelerationKey, 0.001f);
    //    //jumpingPower = PlayerPrefs.GetFloat(jumpingPowerKey, 100f);
    //    //jumpingPower = 100f;
    //    //acceleration = 0.001f;
    //    //PlayerPrefs.SetFloat(accelerationKey, acceleration);
    //    //PlayerPrefs.SetFloat(jumpingPowerKey, jumpingPower);
    //    //PlayerPrefs.Save();
    //    //money = PlayerPrefs.GetInt(moneyKey, 0);
    //}
}
public class ShopData
{
    public int jumpPowerPurchased = 0;
    public int accelerationPurchased = 0;
}

public class GameData
{
    public float bestScore = 0;
}