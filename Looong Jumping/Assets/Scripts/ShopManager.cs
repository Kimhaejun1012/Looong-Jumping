using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerData;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI playerMoney;
    ShopTable shopTable;
    public Button rocketbootsPurChaseButton;
    public Button rocketbootsEquipButton;
    public Button portalPurChaseButton;
    public Button portalEquipButton;

    public TextMeshProUGUI jumpPirceText;
    public TextMeshProUGUI accelPirceText;
    public TextMeshProUGUI airPirceText;
    public TextMeshProUGUI perfectPirceText;
    public TextMeshProUGUI colliderReductionPricePirceText;
    public TextMeshProUGUI colliderIncreasePrice;

    public TextMeshProUGUI rocketbootsEquip;
    public TextMeshProUGUI portalEquip;


    public int specialPrice = 2500;

    public GameObject[] rocketIcons;
    public GameObject[] portalIcons;


    public static ShopManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ShopManager>();
            }
            return m_instance;
        }
    }
    private static ShopManager m_instance;
    private void Start()
    {
        shopTable = DataTableManager.GetTable<ShopTable>();
        if (GameManager.instance.saveData.shopData.rocketParchase)
        {
            rocketbootsPurChaseButton.gameObject.SetActive(false);
            rocketbootsEquipButton.gameObject.SetActive(true);
        }
        if (GameManager.instance.saveData.shopData.portalParchase)
        {
            portalPurChaseButton.gameObject.SetActive(false);
            portalEquipButton.gameObject.SetActive(true);
        }
        for (int i = 0; i < GameManager.instance.saveData.gameData.rocketParts1; i++)
        {
            rocketIcons[i].SetActive(true);
        }
        for (int i = 0; i < GameManager.instance.saveData.gameData.rocketParts2; i++)
        {
            rocketIcons[i + 2].SetActive(true);
        }
        for (int i = 0; i < GameManager.instance.saveData.gameData.rocketParts3; i++)
        {
            rocketIcons[i + 4].SetActive(true);
        }
        for (int i = 0; i < GameManager.instance.saveData.gameData.portalParts1; i++)
        {
            portalIcons[i].SetActive(true);
        }
        for (int i = 0; i < GameManager.instance.saveData.gameData.portalParts2; i++)
        {
            portalIcons[i + 2].SetActive(true);
        }
        for (int i = 0; i < GameManager.instance.saveData.gameData.portalParts3; i++)
        {
            portalIcons[i + 4].SetActive(true);
        }
    }

    private void OnEnable()
    {
        playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
        jumpPirceText.text = $"{(int)GameManager.instance.saveData.shopData.jumpPrice}";
        accelPirceText.text = $"{(int)GameManager.instance.saveData.shopData.accelPrice}";
        airPirceText.text = $"{(int)GameManager.instance.saveData.shopData.airPrice}";
        perfectPirceText.text = $"{(int)GameManager.instance.saveData.shopData.perfectPrice}";
        colliderReductionPricePirceText.text = $"{(int)GameManager.instance.saveData.shopData.colliderReductionPrice}";
        colliderIncreasePrice.text = $"{(int)GameManager.instance.saveData.shopData.colliderIncreasePrice}";

        switch (GameManager.instance.saveData.playerData.active)
        {
            case Active.None:
                break;
            case Active.RocketBoots:
                rocketbootsEquip.text = "EQUIPPED";
                break;
            case Active.Portal:
                portalEquip.text = "EQUIPPED";
                break;
            default:
                break;
        }
    }
    public void UpgradeAcceleration()
    {
        if (GameManager.instance.saveData.playerData.gold >= GameManager.instance.saveData.shopData.accelPrice)
        {
            GameManager.instance.saveData.playerData.gold -= GameManager.instance.saveData.shopData.accelPrice;
            GameManager.instance.saveData.shopData.accelPrice *= DataTableManager.GetTable<ShopTable>().GetValue(1).GoldIncrement;
            GameManager.instance.saveData.playerData.acceleration *= DataTableManager.GetTable<ShopTable>().GetValue(1).NumericChange;

            accelPirceText.text = $"{(int)GameManager.instance.saveData.shopData.accelPrice}";
            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";

            SoundManager.instance.SoundPlay("Buy");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }

    public void UpgradeJumpPower()
    {
        if (GameManager.instance.saveData.playerData.gold >= GameManager.instance.saveData.shopData.jumpPrice)
        {
            GameManager.instance.saveData.playerData.gold -= GameManager.instance.saveData.shopData.jumpPrice;
            GameManager.instance.saveData.shopData.jumpPrice *= DataTableManager.GetTable<ShopTable>().GetValue(2).GoldIncrement;
            GameManager.instance.saveData.playerData.jumpingPower += DataTableManager.GetTable<ShopTable>().GetValue(2).NumericChange;
            jumpPirceText.text = $"{(int)GameManager.instance.saveData.shopData.jumpPrice}";
            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            SoundManager.instance.SoundPlay("Buy");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void IncreasePerfectJumpPower()
    {
        if (GameManager.instance.saveData.playerData.gold >= GameManager.instance.saveData.shopData.perfectPrice)
        {
            GameManager.instance.saveData.playerData.gold -= GameManager.instance.saveData.shopData.perfectPrice;
            GameManager.instance.saveData.playerData.perfectJumpPowerIncrease += DataTableManager.GetTable<ShopTable>().GetValue(3).NumericChange;
            GameManager.instance.saveData.shopData.perfectPrice *= DataTableManager.GetTable<ShopTable>().GetValue(3).GoldIncrement;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            perfectPirceText.text = $"{(int)GameManager.instance.saveData.shopData.perfectPrice}";

            SoundManager.instance.SoundPlay("Buy");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }

    public void AirResistance()
    {
        if (GameManager.instance.saveData.playerData.gold >= GameManager.instance.saveData.shopData.airPrice)
        {
            GameManager.instance.saveData.playerData.gold -= GameManager.instance.saveData.shopData.airPrice;
            GameManager.instance.saveData.playerData.airResist -= DataTableManager.GetTable<ShopTable>().GetValue(4).NumericChange;
            GameManager.instance.saveData.shopData.airPrice *= DataTableManager.GetTable<ShopTable>().GetValue(4).GoldIncrement;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            airPirceText.text = $"{(int)GameManager.instance.saveData.shopData.airPrice}";

            SoundManager.instance.SoundPlay("Buy");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void CollisionReductionSpeed()
    {
        if (GameManager.instance.saveData.playerData.gold >= GameManager.instance.saveData.shopData.colliderReductionPrice)
        {
            GameManager.instance.saveData.playerData.gold -= GameManager.instance.saveData.shopData.colliderReductionPrice;
            GameManager.instance.saveData.playerData.speedReduction -= DataTableManager.GetTable<ShopTable>().GetValue(5).NumericChange;
            GameManager.instance.saveData.shopData.colliderReductionPrice *= DataTableManager.GetTable<ShopTable>().GetValue(5).GoldIncrement;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            colliderReductionPricePirceText.text = $"{(int)GameManager.instance.saveData.shopData.colliderReductionPrice}";
            SoundManager.instance.SoundPlay("Buy");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void CollisionIncreaseSpeed()
    {
        if (GameManager.instance.saveData.playerData.gold >= GameManager.instance.saveData.shopData.colliderIncreasePrice)
        {
            GameManager.instance.saveData.playerData.gold -= GameManager.instance.saveData.shopData.colliderIncreasePrice;
            GameManager.instance.saveData.playerData.speedIncrease *= DataTableManager.GetTable<ShopTable>().GetValue(6).NumericChange;
            GameManager.instance.saveData.shopData.colliderIncreasePrice *= DataTableManager.GetTable<ShopTable>().GetValue(6).GoldIncrement;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            colliderIncreasePrice.text = $"{(int)GameManager.instance.saveData.shopData.colliderIncreasePrice}";
            SoundManager.instance.SoundPlay("Buy");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void PurchaseRocketBoots()
    {
        if (GameManager.instance.saveData.playerData.gold >= specialPrice && GameManager.instance.saveData.gameData.rocketParts1 == 2)
        {
            GameManager.instance.saveData.playerData.gold -= specialPrice;
            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            rocketbootsPurChaseButton.gameObject.SetActive(false);
            rocketbootsEquipButton.gameObject.SetActive(true);
            GameManager.instance.saveData.shopData.rocketParchase = true;
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
            SoundManager.instance.SoundPlay("GetBoost");
        }
    }
    public void IncreaseRocketCount()
    {
        if (GameManager.instance.saveData.playerData.gold >= specialPrice && GameManager.instance.saveData.gameData.rocketParts2 == 2)
        {
            GameManager.instance.saveData.playerData.gold -= specialPrice;
            GameManager.instance.saveData.shopData.rocketUsageCount += 2;
            GameManager.instance.saveData.gameData.rocketParts2 = 0;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";

            SoundManager.instance.SoundPlay("GetBoost");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void IncreaseRocketSpeed()
    {
        if (GameManager.instance.saveData.playerData.gold >= specialPrice && GameManager.instance.saveData.gameData.rocketParts3 == 2)
        {
            GameManager.instance.saveData.playerData.gold -= specialPrice;
            GameManager.instance.saveData.shopData.rocketSpeed *= 1.3f;
            GameManager.instance.saveData.gameData.rocketParts2 = 0;
            GameManager.instance.saveData.gameData.rocketParts3 = 0;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            SoundManager.instance.SoundPlay("GetBoost");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void PurchasePortal()
    {
        if (GameManager.instance.saveData.playerData.gold >= specialPrice && GameManager.instance.saveData.gameData.portalParts1 == 2)
        {
            GameManager.instance.saveData.playerData.gold -= specialPrice;
            GameManager.instance.saveData.shopData.portalParchase = true;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
            portalPurChaseButton.gameObject.SetActive(false);
            portalEquipButton.gameObject.SetActive(true);

            SoundManager.instance.SoundPlay("Wormhole");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }

    public void IncreasePortalCount()
    {
        if (GameManager.instance.saveData.playerData.gold >= specialPrice && GameManager.instance.saveData.gameData.portalParts2 == 2)
        {
            GameManager.instance.saveData.playerData.gold -= specialPrice;
            GameManager.instance.saveData.shopData.portalUsageCount++;
            GameManager.instance.saveData.gameData.portalParts2 = 0;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";

            SoundManager.instance.SoundPlay("Wormhole");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void IncreasePortalSpeed()
    {
        if (GameManager.instance.saveData.playerData.gold >= specialPrice && GameManager.instance.saveData.gameData.portalParts3 == 2)
        {
            GameManager.instance.saveData.playerData.gold -= specialPrice;
            GameManager.instance.saveData.shopData.portalIncreaseSpeed += 0.2f;
            GameManager.instance.saveData.gameData.portalParts3 = 0;

            playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";

            SoundManager.instance.SoundPlay("Wormhole");
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
    }
    public void RocketBootsEquip()
    {
        GameManager.instance.saveData.playerData.active = Active.RocketBoots;
        SoundManager.instance.SoundPlay("GetBoost");
        rocketbootsEquip.text = "EQUIPPED";
        portalEquip.text = "EQUIP";
        SaveLoadSystem.AutoSave(GameManager.instance.saveData);
    }
    public void PortalEquip()
    {
        GameManager.instance.saveData.playerData.active = Active.Portal;
        SoundManager.instance.SoundPlay("Wormhole");
        portalEquip.text = "EQUIPPED";
        rocketbootsEquip.text = "EQUIP";
        SaveLoadSystem.AutoSave(GameManager.instance.saveData);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
