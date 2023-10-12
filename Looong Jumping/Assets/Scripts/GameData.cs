using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerData
{
    public enum Active
    {
        None,
        RocketBoots,
        Portal,
        Reloading
    }
    public float acceleration = 0.01f;
    public float jumpingPower = 100f;
    public int gold = 0;
    public Active active = Active.RocketBoots;

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