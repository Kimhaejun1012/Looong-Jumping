using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveDataVC = SaveDataV1;

public class GameManager : MonoBehaviour
{
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

    public int areaIndex = 3;

    public float score;
    public float bestScore;
    public string bestScoreKey = "BestScore";
    public SubCamera subCam;
    public float meteorSpawnInterval = 2f;
    public PlayerContoller playerContoller;

    public SaveDataVC saveData = new SaveDataVC();

    public bool isLanding { get; set; }
    public bool isCamZone { get; set; }
    public bool isJumping { get; set; }
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        SaveLoadSystem.Save(saveData);
        saveData = SaveLoadSystem.AutoLoad() as SaveDataVC;
    }
    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Landing()
    {
        isLanding = true;
        playerContoller.moveSpeed = 0f;
        playerContoller.joystick.gameObject.SetActive(false);
        UIManager.instance.gameoverUI.SetActive(true);
        UIManager.instance.GameOver();
    }

    public void InCamZone()
    {
        subCam.SubCamOn();
    }
    public void OutCamZone()
    {
        subCam.SubCamNo();
    }
}
