using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UObject = UnityEngine.Object;
using Satchel;
using UnityEngine.SceneManagement;
using HutongGames.PlayMaker.Actions;

namespace GodHomeNKGTitleCard
{
    internal class GodHomeNKGTitleCard : Mod
    {
        internal static GodHomeNKGTitleCard Instance { get; private set; }

        public GodHomeNKGTitleCard() : base("GodHomeNKGTitleCard") { }

        public override string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public override void Initialize()
        {
            Log("Initializing");

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;

            Log("Initialized");
        }

        private void OnSceneChanged(Scene from, Scene to)
        {
            Modding.Logger.Log(to.name);
            if (to.name == "GG_Grimm_Nightmare")
            {
                Modding.Logger.Log("Here we are!");
                var grimmControlObj = GameObject.Find("Grimm Control");
                if (grimmControlObj == null)
                {
                    Modding.Logger.Log("This thing is null also!");
                }
                var grimmControlFsm = grimmControlObj.LocateMyFSM("Control");
                if (grimmControlFsm == null)
                {
                    Modding.Logger.Log("It's null!");
                }

                grimmControlFsm.ChangeTransition("Take Control 2", "QUICK", "Pan Over");
                grimmControlFsm.AddCustomAction("Fight Start", () => DisableBlackFader());
                grimmControlFsm.GetAction<Wait>("Silhouette", 3).time = 2f;
            }
        }

        private void DisableBlackFader()
        {
            Modding.Logger.Log("Called");
            var blackfader = GameObject.Find("black_fader");
            blackfader.SetActive(false);
        }
    }
}
