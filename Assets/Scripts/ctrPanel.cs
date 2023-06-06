using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ctrPanel : MonoBehaviour
{
    [Header ("Datos del juego")]
    [SerializeField] private int time;
    [SerializeField] private bool pausa;
    //[SerializeField] private int transTime;
    private ctrTime ctrTime;
    [Header("Paneles")]
    [SerializeField] private GameObject panelInit;
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject panelT; 
    [SerializeField] private GameObject panelFin;
    //
    private Text tTextT; //Tiempo de transición (Texto)
    private bool flagT;
    private float timeT;
    
    private void Awake(){
        ctrTime= GetComponent<ctrTime>();
        tTextT= panelT.transform.GetChild(0).GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pausa= true;
        flagT= false;
        Reset();
        ctrTime.setPausa(pausa);
        ctrTime.setTime(time);
        ctrTime.Reset();
    }
    // Update is called once per frame
    void Update(){
        if(flagT== true){
            timeT -= Time.deltaTime;
            tTextT.text= timeT.ToString("f0");
            if(timeT <= 0.000){
                flagT= false;
                pausa= false;
                panelT.SetActive(false);
                ctrTime.setPausa(pausa);
            }
        }
    }

    public void offPanelInit(){
        panelInit.SetActive(false);
        onPanelT();
    }
    public void onPanelPausa(){
        pausa= true;
        ctrTime.setPausa(pausa);
        panelPausa.SetActive(true);
    }
    public void offPanelPausa(){
        panelPausa.SetActive(false);
        onPanelT();
    }
    private void onPanelT(){
        panelT.SetActive(true);
        timeT= 3;
        flagT= true;
        //contador 3....1;
    }
    public void onPanelFin(){
        pausa= true;
        panelFin.SetActive(true);
    }
    public void Reset(){
        panelInit.SetActive(true);
        panelPausa.SetActive(false);
        panelT.SetActive(false);
        panelFin.SetActive(false);
    }
}
