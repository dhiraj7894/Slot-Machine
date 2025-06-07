using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public AudioSource AudioSource;
    public AudioSource AudioSourceB;
    public AudioClip ReelSpinning;
    public AudioClip ReelStopped;

    public void PlayAudio(string audioType)
    {
        if (audioType.Contains("loop"))
        {
            AudioSource.clip = ReelSpinning;
            AudioSource.loop = true;
            AudioSource.Play();
        }else if (audioType.Contains("stop"))
        {            
            AudioSource.loop = false;
            AudioSource.Stop();
            AudioSourceB.clip = ReelStopped;
            AudioSourceB.Play();
        }
    }

}
