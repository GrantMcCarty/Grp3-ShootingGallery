using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnPoint 
{
    // half of character plus buffer
    float halfX;
    float halfY;
    float halfZ;
    Collider[] colliders;
    float radius;
    float xSpawnRangeLo;
    float xSpawnRangeHi;
    float ySpawnRangeLo;
    float ySpawnRangeHi;
    float zSpawnRangeLo;
    float zSpawnRangeHi;
   
    public Vector3 Generate(GameObject target, GameObject terrain)
    {
        Vector3 position;
        Vector3 spawnPoint; 
        bool positionCollision;
        int maxAttempts = 80;
        int positionAttempt = 0;
        MeshRenderer targetMeshRenderer = target.GetComponent<MeshRenderer>();
        halfX = targetMeshRenderer.bounds.extents.x + 1;
        halfY = targetMeshRenderer.bounds.extents.y + 1.5f;
        halfZ = targetMeshRenderer.bounds.extents.z + 1;
        radius = Mathf.Max(halfX, halfY, halfZ) + 0.5f;
        MeshRenderer terrainMeshRenderer = terrain.GetComponent<MeshRenderer>();
        float buffer = 10f;
        xSpawnRangeLo = terrainMeshRenderer.bounds.min.x + buffer;
        xSpawnRangeHi = terrainMeshRenderer.bounds.max.x - buffer;
        ySpawnRangeLo = terrainMeshRenderer.bounds.min.y + 30; 
        ySpawnRangeHi = terrainMeshRenderer.bounds.max.y + 50;
        zSpawnRangeLo = terrainMeshRenderer.bounds.min.z + buffer;
        zSpawnRangeHi = terrainMeshRenderer.bounds.max.z - buffer;
        Debug.Log("(xLo,xHi): (" + xSpawnRangeLo + "," + xSpawnRangeHi + ")");
        Debug.Log("(zLo,zHi): (" + zSpawnRangeLo + "," + zSpawnRangeHi + ")");
        do
        {
            positionAttempt++;
            position = PositionInRange();
            positionCollision = CollisionOccurs(position);
        } while (positionCollision && positionAttempt <= maxAttempts);
        if (!positionCollision)
        {            
            spawnPoint = position;
        }
        else
        {
            Debug.Log("could not find a collision-free spawn point - leaving target position unchanged");
            spawnPoint = target.transform.position;
        }
        Debug.Log("spawnPoint: " + spawnPoint);
        return spawnPoint;
    }

    Vector3 PositionInRange()
    {
        float x = Random.Range(xSpawnRangeLo, xSpawnRangeHi);
        float y = Random.Range(ySpawnRangeLo, ySpawnRangeHi);
        float z = Random.Range(zSpawnRangeLo, zSpawnRangeHi);
        Vector3 position = new Vector3(x, y, z);
        return position;
    }

    bool CollisionOccurs(Vector3 position)
    {
        bool collisionOccurs = false;
        colliders = Physics.OverlapSphere(position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 lowerLeft = colliders[i].bounds.min;
            Vector3 upperRight = colliders[i].bounds.max;
            collisionOccurs = (position.x + halfX >= lowerLeft.x && upperRight.x >= position.x - halfX)
            && (position.y + halfY >= lowerLeft.y && upperRight.y >= position.y - halfY)
            && (position.z + halfZ >= lowerLeft.z && upperRight.z >= position.z - halfZ);
            if (collisionOccurs)
            {
                break;
            }
        }
        return collisionOccurs;
    }
}