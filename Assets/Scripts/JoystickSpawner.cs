using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.OnScreen;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class JoystickSpawner : MonoBehaviour
{
    [SerializeField]
    private RectTransform joystickBackground;
    [SerializeField]
    private OnScreenStick onScreenStick;

    private Canvas canvas;
    private Finger movementFinger;

    void Awake()
    {
        canvas = joystickBackground.GetComponentInParent<Canvas>();
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += Touch_onFingerDown;
        ETouch.Touch.onFingerUp += Touch_onFingerUp;
    }

    void OnDestroy()
    {
        ETouch.Touch.onFingerDown -= Touch_onFingerDown;
        ETouch.Touch.onFingerUp -= Touch_onFingerUp;
        EnhancedTouchSupport.Disable();
    }

    private void Touch_onFingerDown(Finger finger)
    {
        if (movementFinger != null)
            return;

        movementFinger = finger;

        Vector2 screenPosition = finger.screenPosition;
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out anchoredPos);

        joystickBackground.anchoredPosition = anchoredPos;
        joystickBackground.gameObject.SetActive(true);
    }

    private void Touch_onFingerUp(Finger finger)
    {
        if (movementFinger != finger)
            return;

        joystickBackground.gameObject.SetActive(false);
        movementFinger = null;
    }
}
