using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    private Camera m_camera;

    public new Camera camera
    {
        get { return m_camera; }
    }

    public float Depth
    {
        get { return m_camera.depth; }
        set { m_camera.depth = value; }
    }

    public void Init()
    {
        m_camera = GetComponent<Camera>();
    }
}
