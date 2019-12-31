//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Language.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the languages of the game.</summary>
public class Language : MonoBehaviour
{
    /// <summary>The language</summary>
    private static Dictionary<string, string> language = new Dictionary<string, string>();

    /// <summary>Translate the UI</summary>
    public static void Translate()
    {
        TextAsset filecsv = (TextAsset)Resources.Load("Files/Language");
        language.Clear();

        string fs = Encoding.UTF8.GetString(filecsv.bytes);
        string[] lines = Regex.Split(fs, "\n|\r|\r\n");

        for (int i = 0; i < lines.Length - 1; i = i + 2)
        {
            string[] value = lines[i].Split(';');

            if (Settings.Current.Language == "Español")
            {
                language.Add(value[0], value[1]);
            }

            if (Settings.Current.Language == "English")
            {
                language.Add(value[0], value[2]);
            }

            if (Settings.Current.Language == "French")
            {
                language.Add(value[0], value[3]);
            }
        }

        // Change the values of the text interface:
        Text[] texts = FindObjectsOfType<Text>();
        foreach (Text actualText in texts)
        {
            if (language.ContainsKey(actualText.tag))
            {
                actualText.text = language[actualText.tag];
            }
        }
    }

    /// <summary>Gets the sentence.</summary>
    /// <param name="key">The key.</param>
    /// <returns>The sentence.</returns>
    public static string GetSentence(Key key)
    {
        if (language.ContainsKey(key.ToString()))
        {
            return language[key.ToString()];
        }

        return null;
    }
}