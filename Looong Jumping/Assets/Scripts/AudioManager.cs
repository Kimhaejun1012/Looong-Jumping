using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] public Slider m_MusicMasterSlider;
    [SerializeField] public Slider m_MusicBGMSlider;
    [SerializeField] public Slider m_MusicSFXSlider;

    private void Start()
    {
        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
        m_MusicMasterSlider.value = GameManager.instance.saveData.gameData.masterAudioValue;
        m_MusicSFXSlider.value = GameManager.instance.saveData.gameData.fxAudioValue;
        m_MusicBGMSlider.value = GameManager.instance.saveData.gameData.bgmAudioValue;
        SetMasterVolume(m_MusicMasterSlider.value);
        SetMusicVolume(m_MusicBGMSlider.value);
        SetSFXVolume(m_MusicSFXSlider.value);
    }
    public static AudioManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<AudioManager>();
            }
            return m_instance;
        }
    }
    private static AudioManager m_instance;
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //if(bgm != null && clip != null)
        //{
        //    bgm.PlayOneShot(clip);
        //}
    }

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 40);
    }

    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 40);
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("Effect", Mathf.Log10(volume) * 40);
    }
}
