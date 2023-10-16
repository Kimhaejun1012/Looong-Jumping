using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject stat;
    public GameObject special;

    // �г��� ��ȯ�ϴ� �޼���
    public void StatPanelOpen()
    {
        stat.SetActive(true);
        special.SetActive(false);
    }
    public void SpecialPanelOpen()
    {
        stat.SetActive(false);
        special.SetActive(true);
    }
}
