using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    // �г��� ��ȯ�ϴ� �޼���
    public void TogglePanels()
    {
        if (panel1.activeSelf)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
        }
        else
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
        }
    }
}
