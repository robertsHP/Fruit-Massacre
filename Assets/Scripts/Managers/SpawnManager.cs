using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager instance;

    [SerializeField] public List<GameObject> walkingFruit = new List<GameObject>();
    [SerializeField] public List<GameObject> enemyTypes = new List<GameObject>();

    [HideInInspector] public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    
    private bool stalkerAlreadySpawned = false;

    void Awake () => instance = this;

    public void SpawnWalkingFruit () {
        SpawnRandomGameObjectAtRandomPoint(walkingFruit);
    }
    public void SpawnEnemy () {
        SpawnRandomGameObjectAtRandomPoint(enemyTypes);
    }

    public void SpawnRandomGameObjectAtRandomPoint (List<GameObject> gObjList) {
        GameObject gObj = GetRandomGameObjectFromList(gObjList);
        SpawnPoint spawnPoint = GetRandomSpawnPoint();
        
        if (spawnPoint != null && gObj != null) {
            if(typeof(Stalker).Name == gObj.name) {
                if(!stalkerAlreadySpawned) {
                    stalkerAlreadySpawned = true;
                } else {
                    SpawnRandomGameObjectAtRandomPoint(gObjList);
                    return;
                }
            }
            spawnPoint.SpawnGameObject(gObj);
        }
    }
    private GameObject GetRandomGameObjectFromList (List<GameObject> gObjList) {
        if(gObjList.Count != 0) {
            int index = (int) Random.Range(0, gObjList.Count);
            return gObjList.ElementAt(index);
        }
        return null;
    }
    private SpawnPoint GetRandomSpawnPoint () {
        if(spawnPoints.Count != 0) {
            if(!IfAllSpawnPointsAreNotInPlayerBounds()) {
                int index = (int) Random.Range(0, spawnPoints.Count);
                return spawnPoints.ElementAt(index);
            }
        }
        return null;
    }
    private bool IfAllSpawnPointsAreNotInPlayerBounds () {
        PlayerViewBounds playerViewBounds = GameManager.instance.player.viewBounds;
        bool allInBounds = true;

        foreach (SpawnPoint spawnPoint in spawnPoints) {
            if(!playerViewBounds.InBounds(spawnPoint.transform.position)) {
                allInBounds = false;
            }
        }
        return allInBounds;
    }
}
