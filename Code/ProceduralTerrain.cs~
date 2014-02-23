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

        const float scale = 10.0f;
        float[,] heights = new float[wMax,hMax];
        Texture2D tex = new Texture2D(wMax,hMax);

        for (int h=0; h<hMax; ++h)
            for (int w=0; w<wMax; ++w)
            {
                heights[w,h] = Mathf.PerlinNoise(((float)w)/wMax*scale, ((float)h)/hMax*scale) * 1.0f;
                tex.SetPixel(w,h, Color.Lerp(Color.black, Color.white, heights[w,h]));
            }

        tex.Apply();
        SplatPrototype splat = new SplatPrototype();
        splat.texture = tex;
        terrainComp.terrainData.splatPrototypes = new SplatPrototype[]{ splat };

        float[,,] alphamap = new float[terrainComp.terrainData.alphamapWidth, terrainComp.terrainData.alphamapHeight, 1];
		
		// For each point on the alphamap...
		for (var y = 0; y < terrainComp.terrainData.alphamapHeight; y++)
			for (var x = 0; x < terrainComp.terrainData.alphamapWidth; x++)
                alphamap[x,y,0] = 1.0f;

        terrainComp.terrainData.SetAlphamaps(0, 0, alphamap);
        terrainComp.terrainData.SetHeights(0, 0, heights);
    }
}