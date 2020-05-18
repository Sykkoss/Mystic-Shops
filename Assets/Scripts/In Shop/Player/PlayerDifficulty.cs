using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDifficulty : MonoBehaviour
{
    [System.Serializable]
    public struct CustomCurve
    {
        public string name;
        public AnimationCurve curve;
    }

    public static PlayerDifficulty Instance;
    //Difficulty should be contained between 1 and 100 (inclusive)
    public int _difficulty;
    public List<CustomCurve> _numberItemsRatesCurves;
    public List<CustomCurve> _itemComplexityRatesCurves;

    public int Difficulty
    {
        get
        {
            return _difficulty;
        }
        private set
        {
            _difficulty = value;
        }
    }


    private void Awake()
    {
        Instance = this;
    }

    public int GetNumberItemsForOrder()
    {
        float difficultyTime = Difficulty / 100f;
        float oneItemRate = _numberItemsRatesCurves[0].curve.Evaluate(difficultyTime);
        float twoItemRate = _numberItemsRatesCurves[1].curve.Evaluate(difficultyTime);
        float threeItemRate = _numberItemsRatesCurves[2].curve.Evaluate(difficultyTime);

        return ComputeDependingOnRates(oneItemRate, twoItemRate, threeItemRate, difficultyTime);
    }

    public int GetItemComplexityForOrder()
    {
        float difficultyTime = Difficulty / 100f;
        float oneItemRate = _itemComplexityRatesCurves[0].curve.Evaluate(difficultyTime);
        float twoItemRate = _itemComplexityRatesCurves[1].curve.Evaluate(difficultyTime);
        float threeItemRate = _itemComplexityRatesCurves[2].curve.Evaluate(difficultyTime);

        return ComputeDependingOnRates(oneItemRate, twoItemRate, threeItemRate, difficultyTime);
    }

    private int ComputeDependingOnRates(float oneItemRate, float twoItemRate, float threeItemRate, float difficultyTime)
    {
        float random = Random.Range(0.01f, 1.00f);

        if (random >= 0.00f && random <= oneItemRate)
            return 1;
        else if (random > oneItemRate && random <= twoItemRate)
            return 2;
        else if (random > twoItemRate && random <= threeItemRate)
            return 3;

        Debug.LogError("Error: A rate in PlayerDifficulty has a problem. At time '" + difficultyTime + "' for a random of '" + random + "'.");
        return -1;
    }
}
