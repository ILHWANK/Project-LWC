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
        Complete
    }

    public enum LoginType
    {
        Null,
        Google,
        Guest
    }

    public enum SDType
    {
        Item,
        ItemMaxAmount,
        ItemName,
        End
    }

    public enum Language
    {
        English,
        Korean
    }

    public enum ItemType
    {
        Expendables,
        Crops
    }

    public enum SubType
    {
        NULL,
        Seed
    }

    public enum SceneType
    {
        Title,
        Loading,
        GamePlay
    }

    public enum DeserializeType { SD, DTO, DefineDtoByBackend }

    public class Define : MonoBehaviour
    {

    }
}