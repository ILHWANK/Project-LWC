using UnityEngine;

namespace script.Common
{
    public static class UIUtilities
    {
        // 게임 오브젝트의 활성화 상태를 설정하는 유틸리티
        public static void SetUIActive(GameObject uiObject, bool isActive)
        {
            if (uiObject)
            {
                uiObject.SetActive(isActive);
            }
        }
    }
}
