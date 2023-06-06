using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelSetting;
    // Start is called before the first frame update
    void Start()
    {
        offPanelSettings();
    }

    // Update is called once per frame
    //void Update(){}

    public void onPanelSettings(){
        panelSetting.SetActive(true);
    }
    public void offPanelSettings(){
        panelSetting.SetActive(false);
    }
}
