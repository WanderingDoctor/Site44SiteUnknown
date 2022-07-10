using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Utils
{
    public static string GetStringKey(string key, string value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
            return value;
        }
        return PlayerPrefs.GetString(key, value);
    }

    public static int GetIntKey(string key, int value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
            return value;
        }
        return PlayerPrefs.GetInt(key, value);
    }

    public static float GetFloatKey(string key, float value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
            return value;
        }
        return PlayerPrefs.GetFloat(key, value);
    }

    public static int ToInt(this bool boolean)
    {
        if (boolean == true) return 1;
        return 0;
    }

    public static bool ToBool(this int interger)
    {
        if (interger >= 1) return true;
        return false;
    }

}
