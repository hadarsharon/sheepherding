using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAreaScript : MonoBehaviour
{
    [SerializeField]
    GameObject sheepPrefab;

    GameStateManager.LevelData thisLevelData;

    Transform spawnArea;
    float area_x;
    float area_z;

    void Start()
    {
        Debug.Log("StartArea: Start()");

        spawnArea = transform;
        area_x = spawnArea.GetComponent<MeshRenderer>().bounds.size.x;
        area_z = spawnArea.GetComponent<MeshRenderer>().bounds.size.z;


    }

    public void SpawnSheep(GameStateManager.LevelData levelData)
    {
        Debug.Log("spawn sheep start");

        int numOfSheep = levelData.noOfSheep;

        
        //corner of spawn point
        float cornerPoint_x = spawnArea.position.x - area_x/2;
        float cornerPoint_z = spawnArea.position.z - area_z/2;

        int div = (int)Mathf.Ceil(Mathf.Sqrt(numOfSheep));

        int count = 0;
        bool done = false;
        for(int i = 0; i < div; i++)
        {
            for (int j = 0; j < div; j++)
            {
                //instantiate sheep as child of spawn area
                GameObject obj = Instantiate(sheepPrefab, Vector3.zero, Quaternion.identity, spawnArea) as GameObject;

                SheepBehavior sheepBehaviour = obj.GetComponent<SheepBehavior>();
                sheepBehaviour.InjectSheepParameters(levelData);

                float spawnPoint_x = cornerPoint_x + j*(area_x/div) + (area_x/div)/2;
                float spawnPoint_y = cornerPoint_z + i*(area_z/div) + (area_z/div)/2;

                //move sheep
                obj.transform.position = new Vector3(spawnPoint_x, 0, spawnPoint_y);

                //unset parent of sheep
                obj.transform.parent = null;

                count++;
                if (count == numOfSheep)
                {
                    done = true;
                    break;
                }
            }
            if(done)
            {
                break;
            }
        }



    }
}
