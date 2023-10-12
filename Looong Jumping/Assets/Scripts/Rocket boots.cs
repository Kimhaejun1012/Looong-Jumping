using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketboots : ActiveItem
{
    private int count = 3;
    private float originalSpeed;
    private float boostedSpeed;
    private bool isBoosting;
    public GameObject[] particleSystem; 
    public PlayerContoller player;

    public override void ApplyEffectToPlayer(PlayerContoller player)
    {
        if(count != 0)
        {
            boostedSpeed = player.moveSpeed + 5f;
            originalSpeed = player.moveSpeed;
            count--;
            StartCoroutine(BoostPlayerSpeed(1.0f));
        }
    }
    private IEnumerator BoostPlayerSpeed(float duration)
    {
        isBoosting = true;
        float elapsed = 0f;
        player.playerAnimator.SetBool("Rocket",true);

        for(int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].SetActive(true);
        }

        while (elapsed < duration)
        {
            SetPlayerSpeed(boostedSpeed);

            elapsed += Time.deltaTime;
            yield return null;
        }

        SetPlayerSpeed(originalSpeed);
        player.playerAnimator.SetBool("Rocket", false);
        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].SetActive(false);
        }
        isBoosting = false;
    }
    private void SetPlayerSpeed(float speed)
    {

        player.moveSpeed = speed;
    }
}
