﻿#pragma warning disable 0626
#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;

public class ETGModDebugLogMenu : IETGModMenu {

    /// <summary>
    /// All debug logged text lines. Feel free to add your lines here!
    /// </summary>
    private static Dictionary<string, LoggedText> allLoggedText = new Dictionary<string, LoggedText>();

    public static Vector2 ScrollPos;

    private static Rect _MainBoxRect = new Rect(16, 16, Screen.width-32, Screen.height-32);
    private static Rect _ViewRect = new Rect(16, 16, Screen.width-32, Screen.height-32);

    public void Start() {
    }

    public void Update() {

    }

    public void OnGUI() {

        //Set rect
        _MainBoxRect = new Rect(16, 16, Screen.width - 32, Screen.height - 32);

        //Draw main box
        _DrawMainBox();

        //Draw the logged text
        _DrawLoggedText();
    }

    public void OnOpen() { }

    public void OnClose() { }

    public void OnDestroy() { }

    private void _DrawMainBox() {
        GUI.Box(_MainBoxRect, string.Empty);
    }

    private void _DrawLoggedText() {

        GUILayout.BeginArea(_MainBoxRect);

        ScrollPos=GUILayout.BeginScrollView(ScrollPos);

        foreach (KeyValuePair<string, LoggedText> msg in allLoggedText) {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("", GUILayout.Width(20)))
                msg.Value.IsStacktraceShown=!msg.Value.IsStacktraceShown;
            GUILayout.Label(( msg.Value.LogCount>0 ? "("+ msg.Value.LogCount + ")" : "") + msg.Value.LogMessage);
            GUILayout.EndHorizontal();

            if (msg.Value.IsStacktraceShown) {
                GUILayout.Label("----Stacktrace---- \n" + "    " + msg.Value.Stacktace);
            }

            GUILayout.Space(3);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();

    }

    public static void Log(string Log) {
        Logger(Log, StackTraceUtility.ExtractStackTrace(), LogType.Log);
    }

    public static void LogError(string Log) {
        Logger(Log, StackTraceUtility.ExtractStackTrace(), LogType.Error);
    }

    public static void LogWarning(string Log) {
        Logger(Log, StackTraceUtility.ExtractStackTrace(), LogType.Warning);
    }

    public static void Logger(string text, string stackTrace, LogType type) {

        if (allLoggedText.ContainsKey(text)) {
            allLoggedText[text].LogCount++;
        } else {
            allLoggedText.Add(text, new LoggedText(text, StackTraceUtility.ExtractStackTrace(), type));
        }

    }

    class LoggedText {

        public LoggedText(string logMessage,string stackTrace, LogType type) {
            this.LogMessage=logMessage;
            this.Stacktace=stackTrace;
            this.LogType=type;
        }

        public string LogMessage;
        public string Stacktace;
        public bool IsStacktraceShown=false;
        public int LogCount=0;
        public LogType LogType;
    }

}

