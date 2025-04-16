using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandGenerator : MonoBehaviour
{
    public int width = 50;
    public int height = 30;
    public float noiseScale = 10f;
    public float falloffStrength = 3f;
    public Tilemap tilemap;
    public TileBase grassTile, dirtTile, rockTile;
    public float grass = 0.7f, dirt = 0.4f; // 🎯 しきい値の利用

    void Start()
    {
        GenerateIsland();
    }

    void GenerateIsland()
    {
        float[,] noiseMap = GenerateNoiseMap();
        float[,] falloffMap = GenerateFalloffMap();

        for (int x = 0; x < width; x++)
        {
            int groundHeight = 0;

            for (int y = 0; y < height; y++)
            {
                float noiseValue = noiseMap[x, y] - falloffMap[x, y];

                if (noiseValue > dirt) // 🎯 土のしきい値を適用（調整可能）
                {
                    groundHeight = y;
                }
            }

            // 🎯 groundHeight に基づいてタイルを配置
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x - width / 2, y - height / 2, 0);

                if (y > groundHeight)
                {
                    tilemap.SetTile(tilePosition, null); // 空（配置しない）
                }
                else if (y == groundHeight)
                {
                    tilemap.SetTile(tilePosition, grassTile); // 草
                }
                else if (y > groundHeight - 3)
                {
                    tilemap.SetTile(tilePosition, dirtTile); // 土
                }
                else
                {
                    tilemap.SetTile(tilePosition, rockTile); // 岩
                }
            }
        }
    }

    float[,] GenerateNoiseMap()
    {
        float[,] map = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sampleX = x / noiseScale;
                float sampleY = y / noiseScale;
                map[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
            }
        }
        return map;
    }

    float[,] GenerateFalloffMap()
    {
        float[,] map = new float[width, height];
        float centerX = width / 2f;
        float centerY = height / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float nx = (x - centerX) / (width / 2f);
                float ny = (y - centerY) / (height / 2f);

                float distance = Mathf.Sqrt(nx * nx + ny * ny);
                float falloff = Mathf.Pow(distance, falloffStrength);

                // 🎯 上側を強く削る影響を調整可能に
                float upperModifier = (y > centerY) ? (y - centerY) / (height / 2f) * 1.2f : 0f;
                falloff += upperModifier;

                map[x, y] = falloff;
            }
        }
        return map;
    }
}
