using UnityEngine;

public class Portal : ActiveItem
{
    public GameObject portal;

    public override void ApplyEffectToPlayer(PlayerContoller player)
    {
        //var x = Instantiate(portal);
        ////x.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.forward /*player.transform.position.z + 50f*/);
        //x.transform.position = player.transform.position;
        //Debug.Log("Æ÷Å» ¹öÆ°");
        var x = Instantiate(portal);
        Vector3 playerForward = player.transform.forward;
        Vector3 portalPosition = player.transform.position + playerForward * 10f;
        x.transform.position = portalPosition;
        x.transform.LookAt(player.transform);
        Debug.Log("Æ÷Å» ¹öÆ°");
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
