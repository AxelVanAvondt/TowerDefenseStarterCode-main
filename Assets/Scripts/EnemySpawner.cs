using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;

    private void SpawnEnemy(int type, Path path)
    {
        var newEnemy = Instantiate(Enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newEnemy.GetComponentInParent<Enemy>();
        script.path = path;
        script.target = Path1[1];
    }
    private void SpawnTester()
    {
        SpawnEnemy(0, Path.Path1);
    }
    public GameObject RequestTarget(Path path, int index)
    {
        if (path == Path.Path1)
        {
            if (index + 1 >= Path1.Count)
            {
                return null;
            }
            else
            {
                return Path1[index + 1];
            }
        }
        else
        {
            if (index + 1 >= Path2.Count)
            {
                return null;
            }
            else
            {
                return Path2[index + 1];
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
