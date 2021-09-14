using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 每幾秒要生出一波敵人？
    /// </summary>
    [SerializeField] float spawnInterval;

    [SerializeField] List<GameObject> _foeList;

    public static int Wave = 0;
    public static int Score = 0;
    public static int Hp => Player.Instance.hp;

    // Start is called before the first frame update
    void Start()
    {
        Wave = 1;
        Score = 0;
        // Hp
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(1f);

        for(Wave = 1; true; Wave++)
        {
            // spawn
            int baseSpawnCount = 2 + Wave;
            int spawnCount = (int)Random.Range(baseSpawnCount * 0.8f, baseSpawnCount * 1.2f);
            for(int i = 0; i < spawnCount; i++)
            {
                Vector3 rdnVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized; // this may be bad lol
                // print(rdnVector);
                Vector3 pos = Player.Instance.transform.position + rdnVector * Random.Range(3f, 9f); // random pos arround player

                GameObject g = Instantiate(_foeList[Random.Range(0, _foeList.Count)]); // random foe
                g.transform.position = pos;
            }

            // wait
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
