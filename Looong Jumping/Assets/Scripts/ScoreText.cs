using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreText : MonoBehaviour
{
    public float score = 0;
    public Transform startLine;
    public Transform playerPos;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos.position.z > startLine.position.z)
        {
            score = playerPos.position.z - startLine.position.z;
        }
        scoreText.text = $"SCORE : {score}";
    }
}
