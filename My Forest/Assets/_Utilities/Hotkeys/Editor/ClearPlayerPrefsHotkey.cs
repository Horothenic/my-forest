using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefsHotkey : MonoBehaviour
{
    // CTRL + ALT + E
    [MenuItem("Tools/Hotkeys/Clear Player Prefs %&e")]
    private static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("[HOTKEYS] PlayerPrefs cleared!");
    }
}
