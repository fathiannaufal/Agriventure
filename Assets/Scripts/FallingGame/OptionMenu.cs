using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public void SetVolume (float volume)
    {
        audiomixer.SetFloat("MusicVol", Mathf.Log10 (volume) * 20);
    }

}
