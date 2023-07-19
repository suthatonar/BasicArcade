using UnityEngine;

public class Drag : MonoBehaviour
{
    private item item;
    private Vector3 offset;
    private Vector3 startposition;
    private float zCoord;

    private void Start()
    {
        item = GetComponent<item>();
    }

    private void OnMouseDown()
    {
        // Calculate the offset from the object's position to the mouse position when clicked
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        
        Cursor.visible = false;

        item.itemStatus = item.ItemStatus.Grabbed;
    }

    private void OnMouseDrag()
    {
        // Update the object's position based on the mouse movement
        Vector3 newPos = GetMouseWorldPos() + offset;
        newPos.y = GameManager.instance.objectHeight; // ¡ÓË¹´¤ÇÒÁÊÙ§ãËÁèãËé¡Ñºá¡¹ y
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        Cursor.visible = true;

        item.itemStatus = item.ItemStatus.None;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
