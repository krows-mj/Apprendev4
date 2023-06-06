using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ctrMG8 : MonoBehaviour
{
    private ctrColor Colors;
    private ctrGeometric Figures;
    private ctrTime Timer;
    private ctrTextSpeech AuxTextSpeech;

    private enum Type{Figure, Color, FigureAndColor}
    [Header ("Datos del juego")]
    [SerializeField]
    private Type GameType;
    [SerializeField]
    private string answer;
    [SerializeField]
    private int points;
    [Header ("Objetos del juego")]
    [SerializeField]
    private GameObject[] Card= new GameObject[4];
    private ctrCard[] ctrCard= new ctrCard[4];
    [SerializeField]
    private Text textInit;
    [SerializeField]
    private Text textFinal;
    [SerializeField]
    private Text textPoints;
    private Animator animPoints;
    private AudioSource ctrAudio;

    //Variables auxiliares
    private int i, sp, c, na, nf, nc;
    private bool b;

    private void Awake(){
        Figures= GameObject.Find("CTR").GetComponent<ctrGeometric>();
        Colors= GameObject.Find("CTR").GetComponent<ctrColor>();
        AuxTextSpeech= GameObject.Find("ManagerTextToSpeech").GetComponent<ctrTextSpeech>();
        Timer= GetComponent<ctrTime>();
        for(i=0; i<4; i++){
            ctrCard[i]= Card[i].GetComponent<ctrCard>();
        }
        animPoints= textPoints.gameObject.GetComponent<Animator>();
        ctrAudio= GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        points= 0;
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
        textPoints.text= "x00";   
    }

    // Update is called once per frame
    //void Update(){}

    public bool CompareAnswer(string cad){
        if(answer.Equals(cad)){
            b= true;
            points++;
            animPoints.SetTrigger("add");
            textPoints.text=(points<10)?"x0"+points:"x"+points;
        }else{
            Timer.RemoveTime(10f);
            b= false;
        }
        return b;
    }

    public void ChangeCards(){
        switch(GameType){
            case Type.Figure:
                for(i=0; i<4; i++){
                    sp= Figures.getRandomFigure();
                    ctrCard[i].ChangeCard(Figures.getFigure(sp), Colors.getColor(Colors.getRandomColor()));
                    ctrCard[i].setResp(Figures.getNameFigure(sp));
                    ctrCard[i].setNF(sp);
                }
                break;
            case Type.Color:
                for(i=0; i<4; i++){
                    c= Colors.getRandomColor();
                    ctrCard[i].ChangeCard(Figures.getFigure(Figures.getRandomFigure()), Colors.getColor(c));
                    ctrCard[i].setResp(Colors.getNameColor(c));
                    ctrCard[i].setNC(c);
                }
                break;
            case Type.FigureAndColor:
                for(i=0; i<4; i++){
                    sp= Figures.getRandomFigure();
                    c= Colors.getRandomColor();
                    ctrCard[i].ChangeCard(Figures.getFigure(sp), Colors.getColor(c));
                    ctrCard[i].setResp(Colors.getNameColor(c)+" "+Figures.getNameFigure(sp));
                    ctrCard[i].setNF(sp);
                    ctrCard[i].setNC(c);
                }
                break;
            default:
                for(i=0; i<4; i++){
                        sp= Figures.getRandomFigure();
                        ctrCard[i].ChangeCard(Figures.getFigure(sp), Colors.getColor(Colors.getRandomColor()));
                        ctrCard[i].setResp(Figures.getNameFigure(sp));
                        ctrCard[i].setNF(sp);
                }
                Debug.Log("Erron, ChangeCards");
                break;
        }
        ChangeAnswer();
    }
    public void ResetCards(){
        ctrCard[0].ResetCard();
        ctrCard[1].ResetCard();
        ctrCard[2].ResetCard();
        ctrCard[3].ResetCard();
        Tareas.Nueva(0.5f, ChangeCards);
    }
    private void ChangeAnswer(){
        answer= ctrCard[Random.Range(0,4)].getResp();
        /** Obtener el audio a preproducir * En construcción*
        na= Random.Range(0,4);
        switch(GameType){
            case Type.Figure:
                nf= ctrCard[na].getNF();
                break;
            case Type.Color:
                nc= ctrCard[na].getNC();
                break;
            case Type.FigureAndColor:
                nf= ctrCard[na].getNF();
                nc= ctrCard[na].getNC();
                break;
            default:
                nf= ctrCard[na].getNF();
                break;
        }
        */
        PlayAnswer();
    }
    public void StartGameCards(){
        Tareas.Nueva(2.8f, ResetCards);
    }
    public void PlayAnswer(){
        AuxTextSpeech.StartSpeaking(answer);
    }
}
