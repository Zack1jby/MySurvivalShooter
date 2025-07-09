using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    // Set game volume to value of slider
    void Update()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
