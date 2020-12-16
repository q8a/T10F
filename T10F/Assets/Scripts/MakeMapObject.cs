using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMapObject : MonoBehaviour
{
    [SerializeField]
    public Image image;
    void Start()
    {
        MiniMapController.RegisterMapObject(this.gameObject, image);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
