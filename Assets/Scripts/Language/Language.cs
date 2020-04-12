//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Language.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the languages of the game.</summary>
public class Language : MonoBehaviour
{
    /// <summary>The language</summary>
    private static readonly Dictionary<string, string> CurrentLanguage = new Dictionary<string, string>();

    /// <summary>Translates to.</summary>
    /// <param name="language">The language.</param>
    public static void TranslateTo(string language)
    {
        Settings.Current.Language = language;
        Translate();
    }

    /// <summary>Translates this instance.</summary>
    public static void Translate()
    {
        TextAsset filecsv = (TextAsset)Resources.Load("Files/Language");
        CurrentLanguage.Clear();

        string fs = Encoding.UTF8.GetString(filecsv.bytes);
        string[] lines = Regex.Split(fs, "\n|\r|\r\n");

        for (int i = 0; i < lines.Length - 1; i = i + 2)
        {
            string[] value = lines[i].Split(';');

            if (Settings.Current.Language.Equals("Spanish"))
            {
                CurrentLanguage.Add(value[0], value[1]);
            }

            if (Settings.Current.Language == "English")
            {
                CurrentLanguage.Add(value[0], value[2]);
            }

            if (Settings.Current.Language == "French")
            {
                CurrentLanguage.Add(value[0], value[3]);
            }
        }

        FindObjectsOfType<Text>()
            .ToList()
            .FindAll(i => CurrentLanguage.ContainsKey(i.tag))
            .ForEach(i => i.text = CurrentLanguage[i.tag]);
    }

    /// <summary>Gets the sentence.</summary>
    /// <param name="key">The key.</param>
    /// <returns>The sentence.</returns>
    public static string GetSentence(Clef key)
    {
        return CurrentLanguage.ContainsKey(key.ToString()) ? CurrentLanguage[key.ToString()] : string.Empty;
    }

    /// <summary>Translates the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static string Translate(string key)
    {
        return CurrentLanguage.ContainsKey(key) ? CurrentLanguage[key] : string.Empty;
    }
}