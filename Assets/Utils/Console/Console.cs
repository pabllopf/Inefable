//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Console.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Debug
{
    using UnityEngine;

    /// <summary>Manage the console messages depends of develop platform.</summary>
    public static class Console
    {
        /// <summary>Prints the specified message.</summary>
        /// <param name="message">The message.</param>
        public static void Print(string message) 
        {
            Debug.Log(message);
        }

        /// <summary>Prints the error.</summary>
        /// <param name="message">The message.</param>
        public static void PrintError(string message)
        {
            Debug.LogError(message);
        }

        /// <summary>Prints the warning.</summary>
        /// <param name="message">The message.</param>
        public static void PrintWarning(string message)
        {
            Debug.LogWarning(message);
        }
    }
}
