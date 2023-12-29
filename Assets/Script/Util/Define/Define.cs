using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WHDle.Util.Define
{
    public enum IntroPhase
    {
        Start,
        ApplicationSetting,
        ServerInit,
        VersionCheck,
        BeforeLogin,
        Register,
        AfterLogin,
        Save_Load,
        StaticData,
        UserData,
        Resource,
        UI,
        Complete
    }

    public enum LoginType
    {
        Null,
        Google,
        Guest
    }

    public class Define : MonoBehaviour
    {

    }
}