using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerControls : MonoBehaviour
{
    /*
        Camera é uma classe do UnityEngine que representa a câmera no espaço 3D
        Utilizada para renderizar cenas, controlar visualizações e aplicar efeitos visuais
    */
    private Camera mainCamera;
    /*    
        Vector3 é uma estrutura que representa um ponto ou vetor no espaço 3D
        Contém três componentes: x, y, z
        Usado para posições, direções e escalas no Unity
    */
    private Vector3 offset;

    private float maxLeft;
    private float maxRight;
    private float maxDown;
    private float maxUp;

    void Start()
    {
        mainCamera = Camera.main;

        StartCoroutine(SetBoundaries());
    }

    void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            if (Touch.activeTouches[0].finger.index == 0)
            {
                Touch userTouch = Touch.activeTouches[0];
                Vector3 touchPosition = userTouch.screenPosition;

#if UNITY_EDITOR
                if (touchPosition.x == Mathf.Infinity)
                    return;
#endif
                touchPosition = mainCamera.ScreenToWorldPoint(touchPosition);

                ChangeTransformPosition(touchPosition);
            }

            LimitClampCalculate();
        }
    }

    private void ChangeTransformPosition(Vector3 touchPosition)
    {
        switch (Touch.activeTouches[0].phase)
        {
            case TouchPhase.Began:
                offset = touchPosition - transform.position;
                break;
            case TouchPhase.Moved:
                transform.position = new Vector3(touchPosition.x - offset.x, touchPosition.y - offset.y, 0);
                break;
            case TouchPhase.Stationary:
                transform.position = new Vector3(touchPosition.x - offset.x, touchPosition.y - offset.y, 0);
                break;
        }
    }

    private void LimitClampCalculate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, maxLeft, maxRight),  // x
            Mathf.Clamp(transform.position.y, maxDown, maxUp),     // y
            0);                                                    // z
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private IEnumerator SetBoundaries()
    {
        // faz algo ou nada
        yield return new WaitForSeconds(0.4f);
        // faz algo
        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;

        maxDown = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.05f)).y;
        maxUp = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.6f)).y;
    }
}
