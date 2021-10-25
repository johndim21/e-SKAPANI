using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem
{
    public enum Language
    {
        English,
        Greek
    }

    public static Language language = Language.English;

    private static Dictionary<string, string> localizedEN;
    private static Dictionary<string, string> localizedGR;

    public static bool isInit;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localizedEN = csvLoader.GetDictionaryValues("en");
        localizedGR = csvLoader.GetDictionaryValues("gr");

        isInit = true;
    }

    public static string GetLocalizedValue(string key)
    {
        if(!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.English:
                localizedEN.TryGetValue(key, out value);
                break;
            case Language.Greek:
                localizedGR.TryGetValue(key, out value);
                break;
        }

        return value;
    }

    public static void SetEnglish()
    {
        language = Language.English;
    }

    public static void SetGreek()
    {
        language = Language.Greek;
    }

    public static Language GetLanguage()
    {
        return language;
    }
}
