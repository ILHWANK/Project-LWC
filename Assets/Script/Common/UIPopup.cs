using UnityEngine;

namespace script.Common
{
    public class UIPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvas;
        [SerializeField] private Animator popupAnimator;

        // 애니메이션 이벤트에서 호출할 메서드
        public void OnCloseAnimationFinished()
        {
            // 애니메이션 완료 후 팝업 캔버스를 비활성화
            UIUtilities.SetUIActive(popupCanvas, false);
        }

        public void Open()
        {
            if (popupCanvas == null) 
                return;
            
            UIUtilities.SetUIActive(popupCanvas, true);

            if (popupAnimator != null)
            {
                popupAnimator.SetTrigger("Open");
            }
        }
        
        public void Close()
        {
            if (popupCanvas == null) 
                return;
            
            if (popupAnimator != null)
            {
                popupAnimator.SetTrigger("Close");
            }
            else
            {
                UIUtilities.SetUIActive(popupCanvas, false);
            }
        }

        // 팝업 내에서 어떤 동작을 수행할 때 호출될 메서드 추가
    }    
}
