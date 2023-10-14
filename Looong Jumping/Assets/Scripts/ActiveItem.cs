using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ActiveItem : MonoBehaviour
{
    public abstract void ApplyEffectToPlayer(PlayerContoller player);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
