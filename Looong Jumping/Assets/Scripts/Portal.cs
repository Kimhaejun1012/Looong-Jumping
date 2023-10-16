using UnityEngine;

public class Portal : ActiveItem
{
    public GameObject[] portalEnter;
    public GameObject[] portalExit;

    public override void ApplyEffectToPlayer(PlayerContoller player)
    {
        //var x = Instantiate(portal);
        ////x.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.forward /*player.transform.position.z + 50f*/);
        //x.transform.position = player.transform.position;
        //Debug.Log("��Ż ��ư");
        int ran = Random.Range(0, 3);
        player.moveSpeedOffset = player.moveSpeed;
        var ent = Instantiate(portalEnter[ran]);
        var exi = Instantiate(portalExit[ran]);
        Vector3 portalPosition = player.transform.position + Vector3.down * 3;
        //Vector3 portalPosition = player.transform.position + playerForward * 10f;
        //portalPosition.y -= 3f;

        ent.transform.position = portalPosition;
        portalPosition.z += 30f;
        portalPosition.y += 10f;
        exi.transform.position = portalPosition;
        player.isPortal = true;
        //player.playerAnimator.SetBool("Jumping",false);
        //x.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 30);

        //x.transform.LookAt(player.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
