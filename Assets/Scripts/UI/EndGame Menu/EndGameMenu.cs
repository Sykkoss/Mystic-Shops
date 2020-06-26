using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    private DifficultySlider _difficultySlider;
    [SerializeField]
    private Text _moneyText;


    private void Start()
    {
        _moneyText.text = PlayerMoneyInLevel.Instance.CurrentMoney.ToString();
    }

    public void RestartGame()
    {
        ShopDifficulty._difficulty = _difficultySlider.GetDifficultyValue();
        ScenesManager.ChangeScene("ShopScene");
    }

    public void BackToMainMenu()
    {
        ScenesManager.ChangeScene("Main Menu");
    }
}
