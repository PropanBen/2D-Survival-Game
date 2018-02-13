using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiMap {

    Lottery<Number> lottery = new Lottery<Number>();
    private VoronoiPoint[] voronoiPoints;
    private Dictionary<VoronoiPoint, List<Vector2>> regions;

    public VoronoiMap(TileType[] tileTypes)
    {
        for(byte i = 0; i < tileTypes.Length; i++)
        {
            lottery.Add(new Number() { Index = i }, tileTypes[i].Weight);
        }
    }

    public byte[,] GenerateMap(int mapWidth, int mapHeight, int pointCount, out Dictionary<VoronoiPoint, List<Vector2>> regions)
    {
        this.regions = new Dictionary<VoronoiPoint, List<Vector2>>();
        GenerateVoronoiPoints(mapWidth, mapHeight, pointCount);
        byte[,] map = GenerateByteMap(mapWidth, mapHeight);
        regions = this.regions;
        return map;
    }
    
    private void GenerateVoronoiPoints(int mapWidth, int mapHeight, int pointCount)
    {
        if (mapWidth * mapHeight < pointCount)
            throw new System.Exception("Es können nicht mehr VoronoiPoints exisitieren als die gesamt Anzahl an Tiles");

        voronoiPoints = new VoronoiPoint[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            VoronoiPoint newPoint = new VoronoiPoint();
            do
            {
                newPoint.X = Random.Range(0, mapWidth);
                newPoint.Y = Random.Range(0, mapHeight);

            } while (VoronoiPointAlreadyExists(newPoint));

            newPoint.TileIndex = (byte)(lottery.Draw().Index+1);//+1 weil der Index 0 für die Border reserviert ist
            voronoiPoints[i] = newPoint;
        }
    }

    private bool VoronoiPointAlreadyExists(VoronoiPoint voronoiPoint)
    {
        foreach(VoronoiPoint p in voronoiPoints)
        {
            if (p == null)
                return false;
            if (p.X == voronoiPoint.X && p.Y == voronoiPoint.Y)
                return true;
        }
        return false;
    }

    private byte[,] GenerateByteMap(int mapWidth, int mapHeight)
    {
        byte[,] map = new byte[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                VoronoiPoint nearestVP = NearestVoronoiPoint(x, y);
                map[x, y] = nearestVP.TileIndex;
                if (!regions.ContainsKey(nearestVP))
                    regions.Add(nearestVP, new List<Vector2>());

                regions[nearestVP].Add(new Vector2(x, y));
            }
        }

        return map;
    }

    private VoronoiPoint NearestVoronoiPoint(int x, int y)
    {
        float smallestDistance = float.MaxValue;
        VoronoiPoint nearestPoint = null;

        foreach (VoronoiPoint point in voronoiPoints)
        {
            if (point.X == x && point.Y == y)
                return point;

            float distance = Mathf.Sqrt(Mathf.Pow(point.X - x, 2) + Mathf.Pow(point.Y - y, 2));
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                nearestPoint = point;
            }
        }

        return nearestPoint;
    }
}

class Number
{
    public byte Index;
}

public class VoronoiPoint
{
    public int X, Y;
    public byte TileIndex;
}