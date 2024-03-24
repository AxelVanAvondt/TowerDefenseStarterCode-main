using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite
{
    public Vector3Int TilePosition;
    public Vector3 WorldPosition;
    public SiteLevel Level;
    public TowerType TowerType;
    private GameObject Tower;
    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        // wijs de tilePosition en worldPosition toe.
        // Bij world position die je krijgt zal de y waarde 0.5 te laag zijn.
        // Die moet je dus aanpassen.
        // verder stel je tower gelijk aan null
        worldPosition.y += 0.5f;
        TilePosition = tilePosition;
        WorldPosition = worldPosition;
        Tower = null;
    }
    public void SetTower(GameObject tower, SiteLevel level, TowerType type)
    {
        // Voordat je de tower toewijst, moet je eerst controleren of de huidige
        // tower verschillend is aan null. Dat kan als je een upgrade van een bestaande tower
        // doet.
        // Als dat zo is, moet je het gameobject eerst verwijderen.
        if(tower != null)
        {
            Tower = tower;
            Level = level;
            TowerType  = type;
        }
        else
        {
            GameObject.Destroy(Tower);
        }
    }
}