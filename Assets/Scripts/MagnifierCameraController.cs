using UnityEngine;
using UnityEngine.UI;

public class MagnifierCameraController : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;               
    public Camera magnifierCamera;    
    public RawImage magnifierRawImage;  
    public Image magnifierMaskImage;  

    [Header("Settings")]
    [Range(1f, 8f)] public float zoom = 2f;  
    public int renderTextureSize = 512;      
    public float diameter = 150f;            
    public LayerMask magnifyLayer;        
    public bool followMouse = true;
    public float smoothFollow = 0.0f;    

    private RenderTexture rt;
    private RectTransform rawRect;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (mainCamera == null) Debug.LogError("Main Camera not assigned!");

        if (magnifierCamera == null) Debug.LogError("Magnifier Camera not assigned!");
        if (magnifierRawImage == null) Debug.LogError("Magnifier RawImage not assigned!");

        rt = new RenderTexture(renderTextureSize, renderTextureSize, 16, RenderTextureFormat.ARGB32);
        rt.name = "MagnifierRT";
        rt.Create();

        magnifierCamera.targetTexture = rt;
        magnifierRawImage.texture = rt;

        magnifierCamera.cullingMask = magnifyLayer;
        magnifierCamera.orthographic = true;

        rawRect = magnifierRawImage.GetComponent<RectTransform>();
        rawRect.sizeDelta = Vector2.one * diameter;

        UpdateCameraZoom();
    }

    void Update()
    {
        Vector3 screenPos = Input.mousePosition;

        if (followMouse)
        {
            if (smoothFollow > 0f)
                rawRect.position = Vector3.Lerp(rawRect.position, screenPos, 1f - Mathf.Exp(-smoothFollow * Time.deltaTime));
            else
                rawRect.position = screenPos;
        }

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -mainCamera.transform.position.z));
        magnifierCamera.transform.position = new Vector3(mouseWorld.x, mouseWorld.y, magnifierCamera.transform.position.z);

        UpdateCameraZoom();
    }

    void UpdateCameraZoom()
    {
        if (mainCamera != null && magnifierCamera != null)
            magnifierCamera.orthographicSize = mainCamera.orthographicSize / zoom;
    }

    void OnDestroy()
    {
        if (rt != null)
        {
            rt.Release();
            Destroy(rt);
        }
    }
}
