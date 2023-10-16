using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject stat;
    public GameObject special;

    // 패널을 전환하는 메서드
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
