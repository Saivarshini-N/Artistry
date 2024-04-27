/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ExampleClass : MonoBehaviour, IPointerDownHandler, IDragHandler// required interface when using the OnPointerDown method.
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the position of the UI object based on the pointer's movement
        rectTransform.anchoredPosition += eventData.delta;
    }
}*/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ExampleClass : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;

    // Define boundaries for the UI object movement

    public Vector2 minMaxPositionX;// Example minimum position
    public Vector2 minMaxPositionY;
    public Image thisImage;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        thisImage = GetComponent<Image>();
    }

    // Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        EditingManager.instance.targetImage = thisImage;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the new position of the UI object
        //Vector2 startPos = eventData.pressPosition;
        //Vector2 dragPos = eventData.position;
        //Vector2 dir = dragPos - startPos;
        //rectTransform.anchoredPosition = dir;
        if (eventData.dragging)
        {
            // Object is being dragged.
            Debug.Log("Dragging:" + eventData.position);
        }
        Vector2 dir = eventData.position;
        dir.x = Mathf.Clamp(dir.x, minMaxPositionX.x, minMaxPositionX.y);
        dir.y = Mathf.Clamp(dir.y, minMaxPositionY.x, minMaxPositionY.y);
        transform.position = dir;
        //float currXPos = transform.position.x;
        //float currYPos = transform.position.y;
        //currXPos = Mathf.Clamp(currXPos, minMaxPositionX.x, minMaxPositionX.y);
        //currYPos = Mathf.Clamp(currYPos, minmaxPositionY.x, minmaxPositionY.y);
        //transform.position = new Vector2(currXPos, currYPos);

        //rectTransform.anchoredPosition = eventData.pressPosition;
    }
    public void UpdateYClampValues(float min,float max)
    {
        minMaxPositionY.x = min;
        minMaxPositionY.y = max;
    }
    public void UpdateXClampValues(float min, float max)
    {
        minMaxPositionX.x = min;
        minMaxPositionX.y = max;
    }
}
