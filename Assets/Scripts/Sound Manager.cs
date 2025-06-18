using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayBGM()
    {

    }

    public void PlaySFX()
    {

    }

    public void SetBGMVolume()
    {

    }

    public void SetSFXVolume()
    {

    }
}
