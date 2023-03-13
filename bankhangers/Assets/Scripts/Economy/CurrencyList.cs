using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEditor;

[CreateAssetMenu]
public class CurrencyList : ScriptableObject
{
    [SerializeField] private string path;

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
    
    public float GetCurrencyValue(CurrencyEnums currencyEnum)
    {
        if ((int)currencyEnum < currencies.Count)
            return currencies[(int)currencyEnum].value;

        return float.NaN;
    }

    public float maxExchangeAmount(string currency, float posessedCoins)
    {
        foreach (Currency c in currencies)
        {
            if (c.currencyName == currency)
            {
                return posessedCoins / c.value;
            }
        }
        return float.NaN;
    }

    #region //update enum script
    [ContextMenu("Update Enum Script")]
    public void CreateEnumScript()
    {
        StringBuilder sb = new StringBuilder();
        string enumName = "CurrencyEnums";
        Include(sb);
        Header(sb, enumName);
        Body(sb, enumName);
        Footer(sb);
        string url = Path.Combine(Application.dataPath, path, (enumName + ".cs"));
        StreamWriter streamWriter = new StreamWriter(url, false);
        streamWriter.Write(sb);
        streamWriter.Flush();
        streamWriter.Close();
        AssetDatabase.Refresh();
        //Debug.Log(sb + "\n\npath: " + url);
    }
    void Include(StringBuilder sb)
    {
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("");
    }
    void Header(StringBuilder sb,  string enumName)
    {
        sb.AppendLine("public enum " + enumName + "\n {");
    }
    void Body(StringBuilder sb, string enumName)
    {
        sb.Append("\t" + currencies[0].currencyName);

        for (int i = 1; i< currencies.Count; i++)
        {
            sb.Append(",\n\t" + currencies[i].currencyName);
        }
        sb.Append("\n");
    }
    void Footer(StringBuilder sb)
    {
        sb.AppendLine("}");
    }
    #endregion
}

[Serializable]
public struct Currency
{
    public string currencyName;

    public float value;

    public Sprite icon;
}
