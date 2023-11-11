using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float speedSetting = 5f;
    [SerializeField]
    private float lerpPeriod = 2f;
    [SerializeField]
    private float mouseXSpeed = 0.5f;
    [SerializeField]
    private float mouseYSpeed = 0.5f;

    public AnimationCurve mouseXCurve;

    private Camera povCamera;
    
    private bool isLerping = false;
    private Vector2 vec2 = Vector2.zero;
    private Vector2 targetVec2 = Vector2.zero;
    private Vector2 currentVec2 = Vector2.zero;
    private Vector2 lastVec2 = Vector2.zero;
    private Vector2 lerpStartVec2 = Vector2.zero;
    private float lerpStart;

    private Vector2 mousePos = Vector2.zero;

    public void Awake   () {
    }

    public void Start() {
        povCamera = GetComponentInChildren<Camera>();
        if(povCamera == null) {
            Debug.LogWarning("No Camera Attached to Player Object!");
        }
    }

    private void Update() {
        if(Time.time > lerpStart + lerpPeriod && isLerping == true) {
            isLerping = false;
            currentVec2 = targetVec2;
        }
        if(isLerping) {
            currentVec2 = vec2 = Vector2.Lerp(lerpStartVec2,targetVec2,(Time.time - lerpStart) / lerpPeriod);
        }
        transform.Translate(new Vector3(currentVec2.x * speedSetting * Time.deltaTime,0,currentVec2.y * speedSetting * Time.deltaTime));
        transform.Rotate(Vector3.up * mouseXCurve.Evaluate(Mathf.Clamp(mousePos.x * 2 - 1f,-1f,1f))*Time.deltaTime*mouseXSpeed);
        povCamera.transform.Rotate(Vector3.left * Mathf.Clamp(mousePos.y*2-1f, -1f, 1f) * Time.deltaTime * mouseYSpeed);
        //if(povCamera.transform.localRotation.eulerAngles.x >30 && ) {
        if(Mathf.Clamp(povCamera.transform.localRotation.eulerAngles.x,30f,180f) == povCamera.transform.localRotation.eulerAngles.x) {
            povCamera.transform.localRotation = Quaternion.Euler(new Vector3(30f,0f,0f));
        }
        if(Mathf.Clamp(povCamera.transform.localRotation.eulerAngles.x, 180f, 330f)== povCamera.transform.localRotation.eulerAngles.x){
            povCamera.transform.localRotation = Quaternion.Euler(new Vector3(330f,0f,0f));
        }
        //povCamera.transform.localRotation = Quaternion.Euler(Mathf.Clamp((0.5f - mousePos.y) *60f,-30f,30f),0,0);
    }

    public void OnMove(InputValue value) {
        targetVec2 = value.Get<Vector2>();
        if(targetVec2 != lastVec2) {
            isLerping = true;
            lerpStart = Time.time;
            lerpStartVec2 = currentVec2;
            lastVec2 = targetVec2;
        }
    }
    public void OnLook(InputValue value) {
        mousePos = value.Get<Vector2>();
        mousePos = povCamera.ScreenToViewportPoint(mousePos);
    }
}
