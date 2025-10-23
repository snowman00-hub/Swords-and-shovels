using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;  
    public Texture2D doorWay;  
    public Texture2D pointer;   
    public Texture2D sword;   
    public Vector2 hotspot = Vector2.zero; 
    public CursorMode cursorMode = CursorMode.Auto;

    private Texture2D currentCursor;

    void Start()
    {        
        Cursor.SetCursor(defaultCursor, hotspot, cursorMode);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Texture2D targetCursor = defaultCursor; 

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag(Tag.Obstacle))
            {
                targetCursor = pointer;
            }
            else if (hit.collider.CompareTag(Tag.Doorway))
            {
                targetCursor = doorWay;
            }
            else if (hit.collider.CompareTag(Tag.Monster))
            {
                targetCursor = sword;
            }
        }

        if (currentCursor != targetCursor)
        {
            SetCursor(targetCursor);
        }
    }

    private void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotspot, cursorMode);
        currentCursor = texture;
    }

    void OnDisable()
    {
        // 스크립트 꺼질 때 기본 커서로 복구
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
