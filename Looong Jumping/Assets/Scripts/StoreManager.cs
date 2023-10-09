using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public int jumpPowerCost = 100;
    public int accelerationCost = 100;
    public TextMeshProUGUI playerMoney;

    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;

    private void Start()
    {
        playerInfo.acceleration = PlayerPrefs.GetFloat(playerInfo.accelerationKey);
        playerInfo.jumpingPower = PlayerPrefs.GetFloat(playerInfo.jumpingPowerKey);
        playerInfo.money = PlayerPrefs.GetInt(playerInfo.moneyKey);

        playerMoney.text = $"Money : {playerInfo.money}";
    }

    public void UpgradeJumpPower()
    {
        if(playerInfo.money > jumpPowerCost)
        {
            playerInfo.money -= jumpPowerCost;
            jumpPowerCost += 100;
            playerInfo.jumpingPower += 10f;
            playerMoney.text = $"Money : {playerInfo.money}";

        }
    }
    public void UpgradeAcceleration()
    {
        if (playerInfo.money > accelerationCost)
        {
            playerInfo.money -= accelerationCost;
            accelerationCost += 100;
            playerInfo.acceleration += 0.1f;
            playerMoney.text = $"Money : {playerInfo.money}";
        }
    }

    public void TestDeductMoney()
    {
        playerInfo.money -= accelerationCost;
        playerMoney.text = $"Money : {playerInfo.money}";
    }

    public void LoadGameScene()
    {
        PlayerPrefs.SetFloat(playerInfo.jumpingPowerKey, playerInfo.jumpingPower);
        PlayerPrefs.SetFloat(playerInfo.accelerationKey, playerInfo.acceleration);
        PlayerPrefs.SetInt(playerInfo.moneyKey, playerInfo.money);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
