using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject {
    public Image icon {get; set;}
    public GameObject owner { get; set; }

}

public class MiniMapController : MonoBehaviour
{
    public Transform playerPos;
    public Camera mapCamera;

    public static List<MapObject> mapObjects = new List<MapObject>();

    public static void RegisterMapObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        mapObjects.Add(new MapObject() { owner = o, icon = image });

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawMapIcons();
    }



    void DrawMapIcons()
    {
        foreach(MapObject mo in mapObjects)
        {
            Vector3 screenPos = mapCamera.WorldToViewportPoint(mo.owner.transform.position);
            mo.icon.transform.SetParent(this.transform);
            RectTransform rt = this.GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            screenPos.x = Mathf.Clamp(screenPos.x * rt.rect.width + corners[0].x, corners[0].x, corners[2].x);
            screenPos.y = Mathf.Clamp(screenPos.y * rt.rect.height + corners[0].y, corners[0].y, corners[1].y);
            screenPos.z = 0;
            mo.icon.transform.position = screenPos;
        }
    }
}
