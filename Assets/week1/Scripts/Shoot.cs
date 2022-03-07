using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject myFish;
    public List<GameObject> fishes = new List<GameObject>();
    public float time1 = 0;
    public bool shot = false;
    // Start is called before the first frame update
    void Start()
    {
        //Spawns a pool of bullets at game start.
        for (int i = 0; i < 25; i++){
            GameObject temp = Instantiate(myFish, Vector3.zero, Quaternion.identity);
            //temp.transform.parent = transform;
            temp.SetActive(false);
            fishes.Add(temp);
        }
        
    }

    // Update is called once per frame
    public float easeOutElastic(float x)
    {
        float c4 = (2 * Mathf.PI) / 3;

        return Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && transform.parent.parent.parent.GetComponent<View>().isActive)
        {
            for (int i = 0; i < fishes.Count; i++){

                //Looks for the first inactive bullet in the list.
                if (!fishes[i].activeInHierarchy && !shot){

                    fishes[i].GetComponent<Bullet>().time = 0;
                    fishes[i].transform.position = transform.position;
                    fishes[i].transform.rotation = transform.rotation;
                    fishes[i].GetComponent<Bullet>().dir = transform.parent.transform.forward;
                    fishes[i].SetActive(true);
                    i = fishes.Count;

                    time1 = 0.0f;
                    shot = true;
                }

            }
        }

        if (shot)
        {
            time1 = Mathf.Clamp(time1 + Time.deltaTime, 0f, 0.25f);
            transform.localScale = new Vector3(0.01f + Mathf.Sin(time1 * 4 * Mathf.PI) * 0.005f, 0.01f + Mathf.Sin(time1 * 4 * Mathf.PI) * 0.005f, 0.01f + Mathf.Sin(time1 * 4 * Mathf.PI) * 0.005f);
            
            if (time1 >= 0.25f)
            {
                time1 = 0.0f;
                shot = false;
            }
        }
    }
}
