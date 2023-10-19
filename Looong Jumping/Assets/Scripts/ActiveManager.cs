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

    public Button[] activeItemButtons; // ��Ƽ�� ������ ��ư �迭
    public int currentActiveItemIndex; // ���� ��Ƽ�� ������ �ε���

    void Start()
    {
        DeactivateAllItemButtons();
    }

    public void DeactivateAllItemButtons()
    {
        foreach (Button button in activeItemButtons)
        {
            button.gameObject.SetActive(false); // ��ư�� ��Ȱ��ȭ
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
