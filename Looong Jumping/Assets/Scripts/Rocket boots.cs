using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketboots : ActiveItem
{
    private int count;
    private float originalSpeed;
    private float boostedSpeed;
    private bool isBoosting = false;
    public GameObject[] particleSystem;
    public PlayerContoller player;

    public override void ApplyEffectToPlayer(PlayerContoller player)
    {
        if(count != 0 && !isBoosting)
        {
            boostedSpeed = player.moveSpeed * 5f;
            originalSpeed = player.moveSpeed;
            GameObject childObject = transform.Find("Count" + count.ToString()).gameObject;
            childObject.SetActive(false);
            count--;

            StartCoroutine(BoostPlayerSpeed(1.0f));
        }
    }
    public void OnEnable()
    {
        count = GameManager.instance.saveData.shopData.rocketUsageCount;

        for(int i = 0; i < count; i++)
        {
            var x = transform.GetChild(i).gameObject;
            x.SetActive(true);
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
