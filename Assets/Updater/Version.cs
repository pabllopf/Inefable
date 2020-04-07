//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Version.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace AutoUpdater
{
    using System;
    using UnityEngine;

    /// <summary>Save the version data.</summary>
    [Serializable]
    public class Version
    {
        /// <summary>The identifier</summary>
        [SerializeField]
        private double id;

        /// <summary>The URL</summary>
        [SerializeField]
        private string url;

        /// <summary>Initializes a new instance of the <see cref="Version"/> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="url">The URL.</param>
        public Version(double id, string url)
        {
            this.id = id;
            this.url = url;
        }

        #region Encapsulate Fields

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public double Id { get => id; set => id = value; }
       
        /// <summary>Gets or sets the URL.</summary>
        /// <value>The URL.</value>
        public string Url { get => url; set => url = value; }

        #endregion
    }
}
