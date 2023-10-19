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
    public float gold = 0;
    public float speedReduction = -30f;
    public float speedIncrease = 10f;
    public float perfectJumpPowerIncrease = 1.1f;
    public float airResist = 0.5f;
    public Active active = Active.None;
}
public class ShopData
{
    public int jumpPowerPurchased = 0;
    public int accelerationPurchased = 0;
    public float rocketSpeed = 3f;
    public int rocketUsageCount = 3;
    public bool rocketParchase = false;
    public int rocketPartsCount;

    public int portalUsageCount = 1;
    public float portalIncreaseSpeed = 1.2f;
    public bool portalParchase = false;

    public float accelPrice = 200f;
    public float jumpPrice = 200f;
    public float perfectPrice = 200f;
    public float airPrice = 200f;
    public float colliderReductionPrice = 200f;
    public float colliderIncreasePrice = 200f;

    //public bool[] rocketParts = new bool[6];
    //public bool[] portalParts = new bool[6];
    //public bool[] cyborgParts = new bool[6];
}

public class GameData
{
    public float bestScore = 0;

    public int rocketParts1 = 0;
    public int rocketParts2 = 0;
    public int rocketParts3 = 0;

    public int portalParts1 = 0;
    public int portalParts2 = 0;
    public int portalParts3 = 0;

}