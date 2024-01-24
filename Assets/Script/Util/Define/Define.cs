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
        PoolableObject,
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
        Make,
        Place,
        PlaceItem,
        End
    }

    public enum Language
    {
        English,
        Korean
    }

    public enum ItemType
    {
        P,
        PI,
        M
    }

    public enum PlaceLocation
    {
        In,
        Out
    }

    public enum MakeItemMixDivision
    {
        and,
        or
    }

    public enum MakeItemLocation
    {
        C,
        A,
        P
    }

    public enum SceneType
    {
        Title,
        Loading,
        GamePlay
    }

    public enum PoolType
    {
        Slot,
        End
    }

    public enum DeserializeType { SD, DTO, DefineDtoByBackend }

    public class Define : MonoBehaviour
    {

    }
}