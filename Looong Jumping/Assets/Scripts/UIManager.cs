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

    public GameObject gameoverUI;

    public float perfectTextDuration = 1f;

    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
                //������ �������� ���� ����ִ� ��� ���ӿ�����Ʈ ��ȸ�ϸ鼭 �� ���۳�Ʈ�� ��������� ���ͼ� ��������
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
        scoreText.text = $"SCORE : {score}";
    }

    public void GameOver()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat(bestScroeKey, bestScore);
            PlayerPrefs.Save();
        }
        resultScoreText.text = scoreText.text;
        bestScoreText.text = $"BEST SCORE : {bestScore}";
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
