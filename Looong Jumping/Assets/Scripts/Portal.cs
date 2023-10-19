using UnityEngine;

public class Portal : ActiveItem
{
    public GameObject[] portalEnter;
    public GameObject[] portalExit;
    public CameraContoller camContoller;
    public int count;

    public void OnEnable()
    {
        count = GameManager.instance.saveData.shopData.portalUsageCount;
    }

    public override void ApplyEffectToPlayer(PlayerContoller player)
    {
        if (count != 0)
        {
            player.isPortal = true;
            int ran = Random.Range(0, 3);
            player.moveSpeedOffset = player.moveSpeed;
            var ent = Instantiate(portalEnter[ran]);
            var exi = Instantiate(portalExit[ran]);
            Vector3 portalPosition = player.transform.position + Vector3.forward * 3;
            portalPosition.y += 1f;

            ent.transform.position = portalPosition;
            portalPosition.z += 30f;
            portalPosition.y += 10f;
            exi.transform.position = portalPosition;
            count--;
            camContoller.PortalOn();

        }
    }
}
