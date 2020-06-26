using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenuButtons;
    [SerializeField]
    private GameObject _difficultySliderMenu;

    [SerializeField]
    private DifficultySlider _difficultySlider;


    private void Start()
    {
        _mainMenuButtons.SetActive(true);
        _difficultySliderMenu.SetActive(false);
    }

    public void PlayGame()
    {
        _mainMenuButtons.SetActive(false);
        _difficultySliderMenu.SetActive(true);
    }

    public void StartGame()
    {
        ShopDifficulty._difficulty = _difficultySlider.GetDifficultyValue();
        ScenesManager.ChangeScene("ShopScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
