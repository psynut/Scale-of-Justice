using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle() {
        int rnd = Random.Range(0,prefabs.Length);
        GameObject m_GameObject = Instantiate(prefabs[rnd],transform.position,Quaternion.Euler(270f,0f,0f),transform);
        m_GameObject.name = prefabs[rnd].name;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position,"Obstacle", true,Color.red);
    }
}
