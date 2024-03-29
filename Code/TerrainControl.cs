using UnityEngine;

//using System.Collections;

public class TerrainControl : MonoBehaviour {
    const string HorizontalAxis = "Horizontal";
    const string VerticalAxis   = "Vertical";
    const string ZoomAxis       = "Zoom";

    public ProceduralTerrain m_terrain;
    public float m_ScrollSpeedFactor = 5.0f;
    public float m_ZoomSpeedFactor = 2.0f;
    
    void Begin()
    {
        
    }
    
    void Update()
    {
        PerformMovement();
        if (m_terrain != null)
            m_terrain.UpdateTerrain();
    }

    void PerformMovement()
    {
        if (m_terrain == null)
            return;

        // Y Zoom movement
        float dy              = Input.GetAxis(ZoomAxis);
        float actualZoomSpeed = m_ZoomSpeedFactor * m_terrain.m_Freq * 0.5f * Time.deltaTime;

        m_terrain.m_Freq += dy * actualZoomSpeed;

        // X-Z offset movement
        float dx                = Input.GetAxis(HorizontalAxis);
        float dz                = Input.GetAxis(VerticalAxis);
        float actualScrollSpeed = m_ScrollSpeedFactor * m_terrain.m_Freq * 0.5f * Time.deltaTime;
        
        m_terrain.m_Offset.x += dx * actualScrollSpeed;
        m_terrain.m_Offset.y += dz * actualScrollSpeed;


    }
}