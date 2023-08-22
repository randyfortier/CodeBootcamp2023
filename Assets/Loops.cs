using System.Collections.Generic;
using UnityEngine;

public class Loops : MonoBehaviour {
    public int minNum = 0;
    public int maxNum = 10;
    public int stepSize = 0;

    public Color startColour = Color.red;
    public Color endColour = Color.blue;

    public string[] names = {
        "jaq","Blake"
    };

    public GameObject prefab;
    public int count = 8;
    public float xSpacing = 1f;

    private List<GameObject> Prefabs;

    public void Generate() {

        Prefabs = new List<GameObject>();

        Vector3 position = transform.position;
        for (int i = 0; i < count; i++) {
            GameObject newObject = Instantiate(prefab);
            newObject.transform.position = position;
            position = new Vector3(position.x + xSpacing, position.y, position.z);
            
            Prefabs.Add(newObject); 
        }
    }
    
    void Start() {
        /*for (int i = minNum; i < maxNum; i += stepSize) {
            Debug.Log("Hello");
        }*/



        for (int i = 0; i < names.Length; i++) {
            string name = names[i];
            Debug.Log("Hello" + names[i]);
        }

        Generate();

    }

    private float GetOffset(int index) {
        return Mathf.Sin(Time.time + index + 50);
    }


    private void SetPrefabPosition(GameObject obj, int i) {
        float y = GetOffset(i);
        obj.transform.position = new Vector3(obj.transform.position.x, y, obj.transform.position.z);
    }

    private void SetPrefabColour(GameObject obj,int index) {
        float t = (GetOffset(index) + 1) / 2;
        var colour = Color.Lerp (startColour, endColour, t);
        var objRenderer = obj.GetComponent<Renderer>();
        objRenderer.material.color = colour;
    }


    public void Update()
    { 
        for (int i = 0; i < Prefabs.Count; i++) {
            float y = Mathf.Sin(Time.time + i * 50);

            if ((i % 2) == 0) {


                GameObject obj = Prefabs[i];

                SetPrefabPosition(obj, i);
                SetPrefabColour (obj, i);
                
            }
        }
    }
    

}
