using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float score = 0f;
    public float bestScore = 0f;
    public string bestScroeKey = "BestScore";

    public Transform startLine;
    public Transform playerPos;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI playerMoney;

    public GameObject gameoverUI;

    public float perfectTextDuration = 1f;

    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
                //위에꺼 쓰지마라 씬에 들어있는 모든 게임오브젝트 순회하면서 저 컴퍼넌트가 들어있으면 나와서 리턴해줌
            }
            return m_instance;
        }
    }
    private void Awake()
    {
        bestScore = PlayerPrefs.GetFloat(bestScroeKey, 0f);
    }
    private static UIManager m_instance;
    public void ShowPerfectText()
    {
        perfectText.text = "Perfect";
        perfectText.gameObject.SetActive(true);
        StartCoroutine(HidePerfectText());
    }

    IEnumerator HidePerfectText()
    {
        yield return new WaitForSeconds(perfectTextDuration);
        perfectText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerPos.position.z > startLine.position.z && !GameManager.instance.isLanding)
        {
            score = playerPos.position.z - startLine.position.z;
        }
        scoreText.text = $"SCORE : {(int)score}";
        playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
    }

    public void GameOver()
    {
        if (score > GameManager.instance.saveData.gameData.bestScore)
        {
            //bestScore = score;
            //PlayerPrefs.SetFloat(bestScroeKey, bestScore);
            GameManager.instance.saveData.gameData.bestScore = score;
        }
        resultScoreText.text = scoreText.text;
        bestScoreText.text = $"BEST SCORE : {(int)GameManager.instance.saveData.gameData.bestScore}";
        GameManager.instance.saveData.playerData.gold += (int)(score * 100f);
        SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        //PlayerPrefs.SetInt(playerInfo.moneyKey, playerInfo.money);
        //PlayerPrefs.Save();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Store()
    {
        //PlayerPrefs.SetFloat(playerInfo.jumpingPowerKey, playerInfo.jumpingPower);
        //PlayerPrefs.SetFloat(playerInfo.accelerationKey, playerInfo.acceleration);
        //PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
