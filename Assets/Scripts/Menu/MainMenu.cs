using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Camera[] cameras;
    public GameObject[] panels;
    int lastCameraIndex = 0;
    int lastPanelIndex = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPanel(int index)
    {
        if (index != lastPanelIndex)
        {
            panels[index].gameObject.SetActive(true);
            panels[lastPanelIndex].gameObject.SetActive(false);
            lastPanelIndex = index;
        }
    }

    public void SetCamera(int index)
    {
        if (index != lastCameraIndex)
        {
            cameras[index].gameObject.SetActive(true);
            cameras[lastCameraIndex].gameObject.SetActive(false);
            lastCameraIndex = index;
        }       
    }
}
