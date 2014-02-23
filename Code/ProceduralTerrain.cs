using UnityEngine;

//using System.Collections;

public class ProceduralTerrain : MonoBehaviour {
    void Awake()
    {
        Terrain terrainComp = (Terrain)gameObject.GetComponent(typeof(Terrain));
        if (terrainComp == null)
            return;

        int wMax = terrainComp.terrainData.heightmapWidth;
        int hMax = terrainComp.terrainData.heightmapHeight;

        if (wMax <= 0.0f || hMax <= 0.0f)
            return;

        const float freq = 10.0f;
        const float scale = 1.0f;
        float[,] heights = new float[wMax,hMax];

        for (int h=0; h<hMax; ++h)
            for (int w=0; w<wMax; ++w)
            {
                float noise = Mathf.PerlinNoise(((float)w)/wMax*freq, ((float)h)/hMax*freq);
                heights[h,w] = noise * scale;
            }

        terrainComp.terrainData.SetHeights(0, 0, heights);

        int alphaW = terrainComp.terrainData.alphamapWidth;
        int alphaH = terrainComp.terrainData.alphamapHeight;
        float[,,] alphamap = new float[alphaH, alphaW, 2];
        for (int h=0; h<alphaH; ++h)
            for (int w=0; w<alphaW; ++w)
            {
                float height = terrainComp.terrainData.GetInterpolatedHeight(((float)w)/alphaW, ((float)h)/alphaH) / scale / terrainComp.terrainData.heightmapScale.y;
                alphamap[h,w,0] = 1.0f - height;
                alphamap[h,w,1] = height;
                
            }
        terrainComp.terrainData.SetAlphamaps(0, 0, alphamap);
    }
}