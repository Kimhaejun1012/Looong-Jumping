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

    private void Awake()
    {
        acceleration = 0.1f;
        jumpingPower = 100f;
        money = PlayerPrefs.GetInt(moneyKey, 0);
    }
}
