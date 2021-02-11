using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    public bool showOnGui = true;

    public enum LogType
    {
        ERROR,
        LOG,
        WARNING
    }

    private static string _logText = "";

    /// <summary>
    /// Print the text in the console and on the GUI (IF ATTACHED TO A GAMEOBJECT).
    /// </summary>
    /// <param name="log">The text to print</param>
    /// <param name="logType">Type of log</param>
    public static void PrintOnGUI(object log, LogType logType = LogType.LOG)
    {
        string color;
        string logStr = log.ToString();
        switch (logType)
        {
            case LogType.ERROR:
                Debug.LogError(logStr);
                color = "red";
                break;
            case LogType.WARNING:
                Debug.LogWarning(logStr);
                color = "yellow";
                break;
            default:
                print(logStr);
                color = "white";
                break;
        }
        _logText = $"<color={color}>{logStr}</color>\n" + _logText;
    }

    private void OnGUI()
    {
        if (showOnGui)
        {
            GUI.TextArea(new Rect(10, 10, Screen.width / 4, Screen.height - 10),
                $"<size=15>{_logText}</size>", new GUIStyle {richText = true});
        }
    }
}
