using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerData
{
    public enum Active
    {
        None,
        RocketBoots,
        Portal,
        Reloading
    }
    public float acceleration = 1f;
    public float jumpingPower = 100f;
    public int gold = 0;
    public float speedReduction = 10f;
    public float speedIncrease = -20f;
    public Active active = Active.None;

}
public class ShopData
{
    public int jumpPowerPurchased = 0;
    public int accelerationPurchased = 0;
    public float rocketSpeed = 3f;
    public int rocketUsageCount = 3;
    public bool rocketParchase = false;
    public float portalIncreaseSpeed = 1.2f;

    public bool portalParchase = false;
    public int portalUsageCount = 1;
    public int rocketPartsCount;
    public bool[] rocketParts = new bool[6];
    public bool[] portalParts = new bool[6];
    public bool[] cyborgParts = new bool[6];
}

public class GameData
{
    public float bestScore = 0;

}