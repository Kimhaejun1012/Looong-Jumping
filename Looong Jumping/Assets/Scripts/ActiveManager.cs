using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveManager : MonoBehaviour
{
    public static ActiveManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ActiveManager>();
            }
            return m_instance;
        }
    }

    private static ActiveManager m_instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Button[] activeItemButtons; // 액티브 아이템 버튼 배열
    public int currentActiveItemIndex; // 현재 액티브 아이템 인덱스

    void Start()
    {
        DeactivateAllItemButtons();
    }

    public void DeactivateAllItemButtons()
    {
        foreach (Button button in activeItemButtons)
        {
            button.gameObject.SetActive(false); // 버튼을 비활성화
        }
    }

    public void ActivateCurrentItemButton()
    {
        if ((int)GameManager.instance.saveData.playerData.active > 0)
        {
            activeItemButtons[(int)GameManager.instance.saveData.playerData.active - 1].gameObject.SetActive(true);
        }
    }
    public void DeactivateAllItemButton()
    {
        if ((int)GameManager.instance.saveData.playerData.active > 0)
        {
            activeItemButtons[(int)GameManager.instance.saveData.playerData.active - 1].gameObject.SetActive(false);
        }
    }
}
