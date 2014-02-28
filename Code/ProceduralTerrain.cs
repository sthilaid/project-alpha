using UnityEngine;
using System;

public class ProceduralTerrain : MonoBehaviour {
    public Terrain m_Terrain;

    public float   m_Scale  = 1.0f;
    public float   m_Freq   = 10.0f;
    public Vector2 m_Offset = Vector2.zero;
    
    public void UpdateTerrain()
    {
        if (m_Terrain == null)
            return;

        int wMax = m_Terrain.terrainData.heightmapWidth;
        int hMax = m_Terrain.terrainData.heightmapHeight;

        if (wMax <= 0.0f || hMax <= 0.0f)
            return;

        //DateTime beginning = DateTime.Now;
        
        float[,] heights = new float[wMax,hMax];
        float centerX = wMax * 0.5f;
        float centerY = hMax * 0.5f;

        for (int h=0; h<hMax; ++h)
            for (int w=0; w<wMax; ++w)
            {
                float x = (((float)w - centerX) * m_Freq + m_Offset.x) / wMax;
                float y = (((float)h - centerY) * m_Freq + m_Offset.y) / hMax;
                float noise = Mathf.PerlinNoise(x, y);
                heights[h,w] = noise * m_Scale;
            }

        m_Terrain.terrainData.SetHeights(0, 0, heights);

        //DateTime afterNoise = DateTime.Now;

        int alphaW = m_Terrain.terrainData.alphamapWidth;
        int alphaH = m_Terrain.terrainData.alphamapHeight;
        // int alphaW = 64;
        // int alphaH = 64;
        float[,,] alphamap = new float[alphaH, alphaW, 2];
        for (int h=0; h<alphaH; ++h)
            for (int w=0; w<alphaW; ++w)
            {
                float height = m_Terrain.terrainData.GetInterpolatedHeight(((float)w)/alphaW, ((float)h)/alphaH) / m_Scale / m_Terrain.terrainData.heightmapScale.y;
                alphamap[h,w,0] = 1.0f - height;
                alphamap[h,w,1] = height;
                
            }
        m_Terrain.terrainData.SetAlphamaps(0, 0, alphamap);

        //DateTime afterAlphamaps = DateTime.Now;

        // Debug.Log(String.Format("Noise DT: {0}, AlphaMaps DT: {1} Total: {2}",
        //                         (float)afterNoise.Subtract(beginning).TotalSeconds,
        //                         (float)afterAlphamaps.Subtract(afterNoise).TotalSeconds,
        //                         (float)afterAlphamaps.Subtract(beginning).TotalSeconds));
    }
}