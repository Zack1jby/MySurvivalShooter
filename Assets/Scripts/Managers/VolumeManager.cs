using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private Slider volumeSlider;

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
