using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Text _sliderValueText;


    private void Start()
    {
        int currentDifficulty = ShopDifficulty._difficulty;

        _sliderValueText.text = currentDifficulty.ToString();
        _slider.value = currentDifficulty;
    }

    public void UpdateSliderValueText(float newValue)
    {
        _sliderValueText.text = newValue.ToString("0");
    }

    public int GetDifficultyValue()
    {
        return int.Parse(_sliderValueText.text);
    }
}
