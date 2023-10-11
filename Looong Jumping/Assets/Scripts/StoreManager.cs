using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    private int jumpPowerCost = 2500;
    private int accelerationCost = 2500;
    public TextMeshProUGUI playerMoney;
    ShopTable shopTable;
    public static StoreManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<StoreManager>();
            }
            return m_instance;
        }
    }
    private static StoreManager m_instance;
    private void Start()
    {
        shopTable = DataTableManager.GetTable<ShopTable>();
        shopTable.GetValue(1001);
    }
    private void Update()
    {
        playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";

        //playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
    }

    private void OnEnable()
    {
        Debug.Log("»óÁ¡ ¾À ÀüÈ¯");
    }

    public void UpgradeJumpPower()
    {
        if(GameManager.instance.saveData.playerData.gold > jumpPowerCost)
        {
            GameManager.instance.saveData.playerData.gold -= jumpPowerCost;
            //jumpPowerCost += 100;
            GameManager.instance.saveData.playerData.jumpingPower += 1f;
            playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void UpgradeAcceleration()
    {
        if (GameManager.instance.saveData.playerData.gold > accelerationCost)
        {
            GameManager.instance.saveData.playerData.gold -= accelerationCost;
            GameManager.instance.saveData.playerData.acceleration += 0.001f;
            playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }

    public void TestDeductMoney()
    {
        GameManager.instance.saveData.playerData.gold -= accelerationCost;
        playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
    }

    public void ChangeScene()
    {
        playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
    }

    public void LoadGameScene()
    {
        //PlayerPrefs.SetFloat(playerInfo.jumpingPowerKey, playerInfo.jumpingPower);
        //PlayerPrefs.SetFloat(playerInfo.accelerationKey, playerInfo.acceleration);
        //PlayerPrefs.SetInt(playerInfo.moneyKey, playerInfo.money);
        //PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
