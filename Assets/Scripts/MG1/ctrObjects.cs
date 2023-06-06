using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrObjects : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> TheObject;
    [SerializeField]
    private GameObject Objectprefab;
    private int i;

    private void Awake(){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        for(i=0; i<transform.childCount; i++){
            TheObject.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Objectprefab= TheObject[0];
    }

    // Update is called once per frame
    //void Update(){}

    public GameObject RequestObject(){
        for(i=0; i<TheObject.Count; i++){
            if(!TheObject[i].activeSelf){
                //TheObject[i].SetActive(true);
                return TheObject[i];
            }
        }
        AddObject();
        return TheObject[TheObject.Count-1];
    }
    private void AddObject(){
        GameObject obj= Instantiate(Objectprefab);
        obj.SetActive(false);
        TheObject.Add(obj);
        obj.transform.SetParent(transform);
    }
}
