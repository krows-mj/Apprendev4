using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ctrTime : MonoBehaviour
{
    [SerializeField] private Text timeTxt;

    [SerializeField] private int init;
    private float time;
    [SerializeField] private bool pausa;
    [SerializeField] private bool gameOver;
    private string min, seg;
    private int tmin, tseg;

    private ctrPanel ctrPanel;
    private Animator anim;

    private void Awake(){
        ctrPanel= GetComponent<ctrPanel>();
        anim= timeTxt.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        min="";
        seg="";
        //Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(pausa==false && gameOver==false){
            time -= Time.deltaTime;
            TimeText(time);
            if(time <= 0.000){
                gameOver= true;
                pausa= true;
                ctrPanel.onPanelFin();
            }
        }
    }
    public void Reset(){
        gameOver= false;
        time= init;
        TimeText(time);
    }

    public void RemoveTime(float f){
        time -= f;
        anim.SetTrigger("remove");
        TimeText(time);
    }

    private void TimeText(float n){
        if(n <=0.0000f){
            timeTxt.text= "00:00";
        }else{
            tmin= (int)n/60;
            tseg= (int)n- (60*tmin);
            min= "0"+tmin.ToString("f0");
            seg=(tseg<10)?"0"+tseg.ToString("f0"):tseg.ToString("f0");
            timeTxt.text= min + ":" + seg;
        }
    }
    
    public void setPausa(bool p){
        pausa= p;
    }
    public bool getPausa(){
        return pausa;
    }
    public void setGameOver(bool go){
        gameOver= go;
    }
    public bool getGameOver(){
        return gameOver;
    }
    public void setTime(int t){
        init = t;
    }
}