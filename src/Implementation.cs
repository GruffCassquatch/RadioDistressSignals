using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Story;
using BepInEx;
using BepInEx.Logging;


namespace RadioDistressSignals
{
    [BepInPlugin(myGUID, pluginName, versionString)]

    public class RadioDistressSignals : BaseUnityPlugin
    {
        private const string myGUID = "com.GruffCassquatch.RadioDistressSignals";
        private const string pluginName = "Radio Distress Signals";
        private const string versionString = "2.0.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + versionString + " " + "loaded.");
            logger = Logger;
        }
    }

    [HarmonyPatch(typeof(Radio))]
    [HarmonyPatch("OnRepair")]
    internal class Radio_onRepair_Patch
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            String[] key = new string[10];
            key[0] = "RadioBounceBack";
            key[1] = "RadioSecondOfficer";
            key[2] = "RadioBloodKelp29";
            key[3] = "RadioGrassy25";
            key[4] = "RadioShallows22";
            key[5] = "RadioMushroom24";
            key[6] = "RadioRadiationSuit";
            key[7] = "RadioGrassy21";
            key[8] = "RadioKelp28";
            key[9] = "RadioKoosh26";

            float[] delay = { 15, 37, 75, 96, 120, 146, 173, 196, 223, 245 };

            Story.GoalType goalType = Story.GoalType.Radio;
            for (int i = 0; i < key.Length; i++)
            {
                StoryGoal goal = new StoryGoal(key[i], goalType, delay[i]);
                StoryGoalScheduler.main.Schedule(goal);
            }
        }
    }
}