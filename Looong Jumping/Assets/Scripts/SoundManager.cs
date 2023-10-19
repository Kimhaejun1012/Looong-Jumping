using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip buysoundClip;
    public AudioClip clicksoundClip;
    public AudioClip footstepClip;
    public AudioClip gameoverClip;
    public AudioClip getBoostClip;
    public AudioClip getCoinClip;
    public AudioClip getSpecialpartsClip;
    public AudioClip hitMeteorClip;
    public AudioClip roketBootsClip;
    public AudioClip wormholeClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static SoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SoundManager>();
            }
            return m_instance;
        }
    }
    private static SoundManager m_instance;
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
    }

    public void SoundPlay(string id)
    {
        switch (id)
        {
            case "Buy":
                audioSource.clip = buysoundClip;
                break;
            case "Footstep":
                audioSource.clip = footstepClip;
                break;
            case "Gameover":
                audioSource.clip = gameoverClip;
                break;
            case "GetBoost":
                audioSource.clip = getBoostClip;
                break;
            case "GetCoinClip":
                audioSource.clip = getCoinClip;
                break;
            case "GetSpecialparts":
                audioSource.clip = getSpecialpartsClip;
                break;
            case "HitMeteor":
                audioSource.clip = hitMeteorClip;
                break;
            case "RoketBoots":
                audioSource.clip = roketBootsClip;
                break;
            case "Wormhole":
                audioSource.clip = wormholeClip;
                break;
            case "Click":
                audioSource.clip = clicksoundClip;
                break;
        }
        audioSource.Play();
    }
}
