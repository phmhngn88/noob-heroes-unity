using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject mainMenu;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    public void Close()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("BackgroundAudio", volume);
        audioMixer.SetFloat("RobotWalkAudio", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
