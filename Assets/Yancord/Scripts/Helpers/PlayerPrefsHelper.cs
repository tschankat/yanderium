using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
    public static class PlayerPrefsHelper
    {
        /// <summary>
        /// Sets a PlayerPref bool.
        /// </summary>
        public static void SetBool(string name, bool flag)
        {
            PlayerPrefs.SetInt(name, flag ? 1 : 0);
        }

        /// <summary>
        /// Gets a PlayerPref bool.
        /// </summary>
        public static bool GetBool(string name)
        {
           return PlayerPrefs.GetInt(name) == 1 ? true : false;
        }
}
}
