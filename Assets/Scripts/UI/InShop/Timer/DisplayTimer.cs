using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTimer : MonoBehaviour
{
    [SerializeField]
    private Text _timerText;

    private void Start()
    {
        if (_timerText == null)
            Debug.LogError("Error: No component Text on gameObject named '" + name + "'.");
    }

    private void OnGUI()
    {
        _timerText.text = PlayerTime.Instance.GetCurrentTime().ToString("0");
    }
}
