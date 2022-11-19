using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenPlatformCounter : MonoBehaviour
{
    private int _amount;
    [SerializeField] private Text _amountText;

    private void Awake()
    {
        UpdateText();
    }
    public void IncreaseAmount()
    {
        _amount++;
        UpdateText();
    }
    private void UpdateText() => _amountText.text = $"{_amount}";
}
