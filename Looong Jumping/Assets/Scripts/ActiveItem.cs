using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ActiveItem : MonoBehaviour
{
    public abstract void ApplyEffectToPlayer(PlayerContoller player);
}
