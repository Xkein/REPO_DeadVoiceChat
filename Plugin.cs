using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace DeadVoiceChat;

public static class PluginInfo
{
    public const string PLUGIN_GUID = "DeadVoiceChat";
    public const string PLUGIN_NAME = "Dead Voice Chat";
    public const string PLUGIN_VERSION = "1.0.0";
}

[BepInPlugin(PluginInfo.PLUGIN_GUID, "DeadVoiceChat", PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private static Harmony _harmony;


    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        _harmony = new Harmony("DeadVoiceChat");
        _harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(PlayerVoiceChat))]
static class PlayerVoiceChatPatchs
{
    [HarmonyPatch("ToggleLobby")]
    [HarmonyPostfix]
    static void DeadChatToggle(PlayerVoiceChat __instance, bool __0, AudioSource ___audioSource)
    {
        bool toggle = __0;
        Plugin.Logger.LogInfo($"DeadVoiceChat: ToggleLobby({toggle})");
        if (toggle)
        {
            ___audioSource.outputAudioMixerGroup = __instance.mixerMicrophoneSound;
        }
    }
}