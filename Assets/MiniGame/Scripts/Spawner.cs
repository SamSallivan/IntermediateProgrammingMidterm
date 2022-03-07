using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject myCheems;
    public float maxCheems;

    public List<GameObject> cheems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Spawns a pool of doggos at game start.
        for (int i = 0; i < maxCheems + 5; i++){
            GameObject temp = Instantiate(myCheems, Vector3.zero, Quaternion.identity);
            temp.transform.parent = transform;
            temp.SetActive(false);
            cheems.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int activeCheems = 0;
        
        //Checks if enough amount of doggos are activated.
        for (int i = 0; i < cheems.Count; i++){
            if (cheems[i].activeInHierarchy){
                activeCheems++;
            }
        }

        //if not, activates the ones that are inactive till there are enough.
        for (int i = 0; i < cheems.Count; i++){
            if (!cheems[i].activeInHierarchy && activeCheems < maxCheems){
                cheems[i].GetComponent<Cheems>().respawn();
                cheems[i].SetActive(true);
                activeCheems++;
            }
        }
    }
}
