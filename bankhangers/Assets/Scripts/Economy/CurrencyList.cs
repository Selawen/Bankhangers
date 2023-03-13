using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class CurrencyList : ScriptableObject
{
    [SerializeField] private List<Currency> currencies;

    /// <summary>
    /// Get the value of the given currency, returns NaN if the currency isn't recognised.
    /// </summary>
    /// <param name="currency"></param>
    /// <returns></returns>
    public float GetCurrencyValue(string currency)
    {
        foreach (Currency c in currencies)
        {
            if (c.currencyName == currency)
            {
                return c.value;
            }
        }

        return float.NaN;
    }
}

[Serializable]
public struct Currency
{
    public string currencyName;

    public float value;
}
