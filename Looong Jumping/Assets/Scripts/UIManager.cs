using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public float score = 0;
    public Transform startLine;
    public Transform playerPos;
    public TextMeshProUGUI scoreText;

    void Start()
    {
    }

    void Update()
    {
        if (playerPos.position.z > startLine.position.z)
        {
            score = playerPos.position.z - startLine.position.z;
        }
        scoreText.text = $"SCORE : {score}";
    }
}
