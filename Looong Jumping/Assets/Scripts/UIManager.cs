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

    private float fadeDuration = 1.0f; // 텍스트가 사라지는 데 걸리는 시간 (초)
    private float startAlpha;

    public GameObject gameoverUI;
    public GameObject pauseScreen;
    public GameObject settingScreen;
    public GameObject resetPanel;

    public float perfectTextDuration = 1f;
    private float timer = 0.0f;

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

    private static UIManager m_instance;
    public void ShowPerfectText()
    {
        perfectText.text = "Perfect";
        perfectText.gameObject.SetActive(true);
        startAlpha = perfectText.color.a;
        StartCoroutine(HidePerfectText());
    }

    IEnumerator HidePerfectText()
    {
        while(perfectText.gameObject.activeSelf)
        {
            //yield return new WaitForSeconds(perfectTextDuration);
            yield return null;
            timer += Time.deltaTime;

            Color textColor = perfectText.color;
            textColor.a = Mathf.Lerp(startAlpha, 0.0f, timer / fadeDuration);
            perfectText.color = textColor;
            if (timer >= fadeDuration)
            {
                perfectText.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (playerPos.position.z > startLine.position.z && !GameManager.instance.isLanding)
        {
            score = playerPos.position.z - startLine.position.z;
        }
        scoreText.text = $"SCORE : {(int)score}";
        playerMoney.text = $"Gold : {(int)GameManager.instance.saveData.playerData.gold}";
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
        GameManager.instance.saveData.playerData.gold += (int)score;
        SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        //PlayerPrefs.SetInt(playerInfo.moneyKey, playerInfo.money);
        //PlayerPrefs.Save();
    }
    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.instance.SoundPlay("Click");
    }
    public void Store()
    {
        //PlayerPrefs.SetFloat(playerInfo.jumpingPowerKey, playerInfo.jumpingPower);
        //PlayerPrefs.SetFloat(playerInfo.accelerationKey, playerInfo.acceleration);
        //PlayerPrefs.Save();
        SoundManager.instance.SoundPlay("Click");
        SceneManager.LoadScene(2);
    }
    public void PauseButton()
    {
        pauseScreen.SetActive(true);
        SoundManager.instance.SoundPlay("Click");
        Time.timeScale = 0f;
    }
    public void ContinueButton()
    {
        pauseScreen.SetActive(false);
        SoundManager.instance.SoundPlay("Click");
        Time.timeScale = 1f;
    }
    public void SettingButton()
    {
        settingScreen.SetActive(true);
        SoundManager.instance.SoundPlay("Click");
    }
    public void ExitButton()
    {
        SoundManager.instance.SoundPlay("Click");
        SceneManager.LoadScene(0);
    }
    public void PlayerButton()
    {
        SoundManager.instance.SoundPlay("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void DataReset()
    {
        //SaveLoadSystem.AutoLoad();
        resetPanel.SetActive(true);
        SoundManager.instance.SoundPlay("Click");
    }
    public void ResetOK()
    {
        SaveLoadSystem.Clear(GameManager.instance.saveData);
        resetPanel.SetActive(false);
        SoundManager.instance.SoundPlay("Click");

    }
    public void ResetNo()
    {
        resetPanel.SetActive(false);
        SoundManager.instance.SoundPlay("Click");

    }


    public void GameExit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        //else
        Application.Quit();
    }
    public void SettingSaveAndExit()
    {
        GameManager.instance.saveData.gameData.masterAudioValue = AudioManager.instance.m_MusicMasterSlider.value;
        GameManager.instance.saveData.gameData.bgmAudioValue = AudioManager.instance.m_MusicBGMSlider.value;
        GameManager.instance.saveData.gameData.fxAudioValue = AudioManager.instance.m_MusicSFXSlider.value;

        SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        SoundManager.instance.SoundPlay("Click");
        settingScreen.SetActive(false);
    }

    public void SettingExitButton()
    {

        SoundManager.instance.SoundPlay("Click");
        settingScreen.SetActive(false);
    }
}
