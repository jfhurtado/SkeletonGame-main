/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstantiateEnemy : MonoBehaviour
{
    public GameObject spiderEnemy;
    public GameObject cubeWayPoint;
    public GameObject newSpiderObject;
    public List<GameObject> enemyList;
    System.Random r = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
       for (int i = 0; i < 10; i++)
        {
            createSpiders(i);
        }
        //Instantiate(cubeWayPoint, new Vector3(2, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createSpiders(int i )
    {
        
        var position = (float)(r.NextDouble() * 27);
         position = position +(float)(r.Next(0,10) );
        var position2 = (float)(r.NextDouble() * 16);
        position2 = position2 + (float)(r.Next(0, 10));

        System.Console.WriteLine(position);

        object instantiatedObject = Instantiate(spiderEnemy, new Vector3(position * -1 , 0, position2), Quaternion.identity);
        newSpiderObject = (GameObject)instantiatedObject;
        SpiderAnimationController animController = newSpiderObject.GetComponent<SpiderAnimationController>();
        animController.waypoints =  new GameObject[5];
        for (int wayPointCount = 0; wayPointCount < 5; wayPointCount++)
        {
            addWayPoint(animController ,wayPointCount);
        }
        enemyList.Add(newSpiderObject);
    }

    private void addWayPoint(SpiderAnimationController animController , int wayPointCount)
    {
        var position2 = (float)(r.NextDouble() * 19);
        position2 = position2 + (float)(r.Next(0, 10));
        var position3 = (float)(r.NextDouble() * 27);
        position3 = position3 + (float)(r.Next(0, 10));

        if(r.Next(0, 1000) % 3 == 0 )
        {
            position3 = position3 * -1;
        }
        if (r.Next(0, 1000) % 5 == 0)
        {
            position2 = position2 * -1;
        }
        animController.waypoints[wayPointCount] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        animController.waypoints[wayPointCount].transform.position = new Vector3(position2, 0, position3);
    }
}

*/