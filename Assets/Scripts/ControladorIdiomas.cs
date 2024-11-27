using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings; // Corrigido o namespace

public class ControladorIdiomas : MonoBehaviour
{
    private bool _active = false;

    // Start is called before the first frame update
    void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID); // Corrigido o nome do método
    }

    public void ChangeLocale(int localeID) // Mantido o nome do método consistente
    {
        if (_active)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);
        _active = false;
    }
}
