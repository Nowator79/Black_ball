using UnityEngine;

using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using System.Security.Principal;

public static class TelegramBot
{
    static private string key = "1669248231:AAHCzh2f2335OuJ7OJ-ewCWdKDFR6X9b_BI";
    //static private string name = "remontsytehelper_bot";
    static private int meId = 450733921;
    static public bool telegramDebug;

    public static void SendMessage(string nameUser = "noName", string massage = "Test")
    {
        if (telegramDebug)
        {
            massage = $"({nameUser}): {massage}";
            string req = "https://api.telegram.org/bot" + key + "/sendMessage?chat_id=" + meId + "&text="+ massage;
            UnityWebRequest myRequest = new UnityWebRequest(req);
            myRequest.SendWebRequest();
        }

    }
}
