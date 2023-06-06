using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ctrMG3 : MonoBehaviour
{
    private ctrColor Colors;
    private ctrGeometric Figures;
    private ctrTime Timer;

    private enum Type{Figure, Color, FigureAndColor}
    [Header("Datos del juego")]
    [SerializeField]
    private Type GameType; 
    [SerializeField]
    private string resp;
    [SerializeField]
    private int points;
    [SerializeField]
    private int Error;
    [SerializeField]
    private int frequency;
    private float counter; //contador
    private Animator anim;
    [Header("Objetos del Juego")]
    [SerializeField]
    private GameObject[] Boxs= new GameObject[4];
    private GameObject[] BoxContent= new GameObject[4];
    private Image[] imageBoxContent= new Image[4];
    [SerializeField]
    private Text textInit;
    [SerializeField]
    private Text textFinal;
    [SerializeField]
    private Text textPoints;
    //[SerializeField]
    private Animator animPoint;
    
    private struct content{
        public string name;
        public Sprite sp;
        public Color c;
        public bool active;
    }
    private content[] Contents= new content[4];

    private int i, o, nc, nf;
    

    private void Awake(){
        Colors= GameObject.Find("CTR").GetComponent<ctrColor>();
        Figures= GameObject.Find("CTR").GetComponent<ctrGeometric>();
        Timer= GetComponent<ctrTime>();
        for(i=0; i<4; i++){
            BoxContent[i]= Boxs[i].transform.GetChild(0).gameObject;
            imageBoxContent[i]= BoxContent[i].GetComponent<Image>();
        }
        anim= BoxContent[0].GetComponent<Animator>();
        animPoint= textPoints.gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(i=0; i<4; i++){
            BoxContent[i].SetActive(false);
            Contents[i].active= false;
        }
        Error=0;
        points=0;
        counter=0;
        frequency=2;
        
        switch (Random.Range(1,4)){
            case 1:
                GameType= Type.Figure;
                textInit.text= "Figure";
                break;
            case 2:
                GameType= Type.Color;
                textInit.text= "Color";
                break;
            case 3:
                GameType= Type.FigureAndColor;
                textInit.text= "Color and Figure";
                break;
            default:
                GameType= Type.Figure;
                textInit.text= "Figure";
                Debug.Log("Error Start, selección de juego");
                break;
        }
        //Tareas.Nueva(1, InstantiateObject);
        textPoints.text= "x00";
    }

    // Update is called once per frame
    void Update()
    {
        if(!Timer.getPausa()){
            counter += Time.deltaTime;
            if(counter >= frequency){
                counter= 0;
                frequency= Random.Range(3,8);
                InstantiateObject();
            }
        }
        if(Timer.getGameOver() && Error < 3){
            textFinal.text= (points!=1 )? " "+points+" points": " "+points+" point";
            Error= 10;
        }
    }

    public void CompareAnswer(string cad){
        resp= Contents[0].name;
        cad.ToLower();
        if(resp.Equals(cad)){
            points++;
            animPoint.SetTrigger("add");
            textPoints.text=(points<10)?"x0"+points:"x"+points;
            Error=0;
            anim.SetTrigger("god");
            anim.SetTrigger("reset");
            anim.SetInteger("E",Error);
            suitBoxContents();
        }else{
            Error++;
            anim.SetInteger("E",Error);
            if(Error >=3) Fault();
        }
    }

    public void InstantiateObject(){
        for(i=0; i<4; i++){
            if(Contents[i].active && i==3){
                RemoveTime();
                break;
            }
            if(!Contents[i].active){
                CreateObject(i);
                i=4;
            }
        }
    }
    private void CreateObject(int n){
        nc= Colors.getRandomColor();
        nf= Figures.getRandomFigure();
        Contents[n].active= true;
        Contents[n].sp= Figures.getFigure(nf);
        Contents[n].c= Colors.getColor(nc);
        switch(GameType){
            case Type.Figure:
                Contents[n].name= Figures.getNameFigure(nf);
                break;
            case Type.Color:
                Contents[n].name= Colors.getNameColor(nc);
                break;
            case Type.FigureAndColor:
                Contents[n].name= Colors.getNameColor(nc) +" "+Figures.getNameFigure(nf);
                break;
            default:
                Debug.Log("Error tipo de juego, CreateObject");
                break;
        }
        imageBoxContent[n].sprite= Contents[n].sp;
        imageBoxContent[n].color= Contents[n].c;
        BoxContent[n].SetActive(true);
        if(n == 0) anim.SetTrigger("reset");
    }
    private void suitBoxContents(){
        for(o=0; o<3; o++){
            if(Contents[o+1].active){
                Contents[o].name= Contents[o+1].name;
                Contents[o].sp= Contents[o+1].sp;
                Contents[o].c= Contents[o+1].c;
                imageBoxContent[o].sprite= Contents[o].sp;
                imageBoxContent[o].color= Contents[o].c;
                if(o==0){
                    anim.SetTrigger("reset");
                    resp= Contents[0].name;    
                }
            }else{
                Contents[o].active= false;
                BoxContent[o].SetActive(false);
                break;
            }
        }
        if(o == 3){
            Contents[o].active=false;
        }
        if(o == 0){
            Contents[0].active=false;
            Tareas.Nueva(1, InstantiateObject);
        }
    }

    private void Fault(){
        suitBoxContents();
        RemoveTime();
        Error=0;
    }

    private void RemoveTime(){
        Timer.RemoveTime(10f);
    }

    public void Reset(){
        Error=0;
        anim.SetTrigger("reset");
    }
}
