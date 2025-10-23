using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;  
    public Texture2D doorWay;  
    public Texture2D pointer;   
    public Texture2D sword;   
    public Vector2 hotspot = Vector2.zero; 
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotspot, cursorMode);
    }

    void OnDisable()
    {
        // 스크립트 꺼질 때 기본 커서로 복구
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
