using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager instance;

    [SerializeField] public List<GameObject> walkingFruitTypes = new List<GameObject>();
    [SerializeField] public List<GameObject> enemyTypes = new List<GameObject>();

    void Awake () => instance = this;

    public void SpawnWalkingFruit () {
        SpawnRandomGameObjectAtRandomPoint(walkingFruitTypes);
    }
    public void SpawnEnemy () {
        SpawnRandomGameObjectAtRandomPoint(enemyTypes);
    }

    public void SpawnRandomGameObjectAtRandomPoint (List<GameObject> gObjList) {
        GameObject gObj = GetRandomGameObjectFromList(gObjList);
        SpawnPoint spawnPoint = GetRandomSpawnPoint();
        
        if (spawnPoint != null && gObj != null) {
            Instantiate(gObj, spawnPoint.transform.position, new Quaternion(0f, 0f, 0f, 0f));
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
        List<SpawnPoint> spawnPoints = GameManager.instance.spawnPoints;

        if(spawnPoints.Count != 0) {
            int index = (int) Random.Range(0, spawnPoints.Count);
            return spawnPoints.ElementAt(index);
        }
        return null;
    }
}
