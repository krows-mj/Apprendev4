using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ctrMG1 : MonoBehaviour
{
    private ctrTime Timer;
    private ctrGeometric Figures;
    private ctrColor Colors;
    private ctrObjects ctrObjects;

    private enum Type{Figure, Color, FigureAndColor}
    [System.Serializable]
    private struct Objetivo{
        public string resp;
        public int counter;
        public GameObject obj;
        [HideInInspector]
        public Text textObj;
        [HideInInspector]
        public Image imgObj;
        [HideInInspector]
        public Animator animTextC;
        public void addPoint(){
            counter++;
            animTextC.SetTrigger("add");
            textObj.text= (counter < 10)? "x0"+counter: "x"+counter;
        }
    }
    [Header("Datos del juego")]
    [SerializeField]
    private Type GameType;
    [SerializeField]
    private int mision;
    private int direction;
    [SerializeField]
    private float spawTime;
    [SerializeField]
    private Objetivo[] Objetivos= new Objetivo[3];
    [Header("Objetos del juego")]
    [SerializeField]
    private Text textInit;
    [SerializeField]
    private Text textFinal;
    //[SerializeField]
    //private GameObject[] TheObject;
    private float width, high, increment; //width-ancho / high-alto

    private float frequency=0;
    private int i, ans, o=2;
    private int[] f= new int[3];
    private int[] c= new int[3];

    //variables para modificar el objeto
    private GameObject obj;
    private Object buttonObject;
    private int objrnd; //Codigo del objeto aleatorio //variable aux

    private void Awake(){
        Timer= GetComponent<ctrTime>();
        Figures= GameObject.Find("CTR").GetComponent<ctrGeometric>();
        Colors= GameObject.Find("CTR").GetComponent<ctrColor>();
        for(i=0; i<3; i++){
            Objetivos[i].textObj= Objetivos[i].obj.transform.GetChild(0).gameObject.GetComponent<Text>();
            Objetivos[i].imgObj= Objetivos[i].obj.GetComponent<Image>();
            Objetivos[i].animTextC= Objetivos[i].obj.transform.GetChild(0).gameObject.GetComponent<Animator>();
        }
        ctrObjects= transform.GetChild(0).gameObject.GetComponent<ctrObjects>();
    }
    // Start is called before the first frame update
    void Start()
    {
        width= 820f;
        high= 360f;
        increment= 150f;
        for(i=0; i<3; i++){
            Objetivos[i].counter= 0;
            Objetivos[i].textObj.text= "x00";
            Objetivos[i].obj.SetActive(false);
        }
        mision= Random.Range(1,3);
        direction= Random.Range(1,3);
        switch (Random.Range(1,3)){
            case 1:
                GameType= Type.Figure;
                textInit.text= "Figure";
                if(mision == 1){
                    f[0]= Figures.getRandomFigure();
                    Objetivos[0].resp= Figures.getNameFigure(f[0]);
                    Objetivos[0].imgObj.sprite= Figures.getFigure(f[0]);
                    Objetivos[0].imgObj.color= Colors.getColor(Colors.getRandomColor());
                }else{
                    if(mision == 2){
                        do{
                            f[0]= Figures.getRandomFigure();
                            f[1]= Figures.getRandomFigure();
                        }while(f[0] == f[1]);
                        for(i=0; i<mision; i++){
                            Objetivos[i].resp= Figures.getNameFigure(f[i]);
                            Objetivos[i].imgObj.sprite= Figures.getFigure(f[i]);
                            Objetivos[i].imgObj.color= Colors.getColor(Colors.getRandomColor());
                        }
                    }else{
                        mision= 3;
                        do{
                            f[0]= Figures.getRandomFigure();
                            f[1]= Figures.getRandomFigure();
                            f[2]= Figures.getRandomFigure();
                        }while(f[0]==f[1] && f[1]==f[2] && f[0]==f[2]);
                        for(i=0; i<mision; i++){
                            Objetivos[i].resp= Figures.getNameFigure(f[i]);
                            Objetivos[i].imgObj.sprite= Figures.getFigure(f[i]);
                            Objetivos[i].imgObj.color= Colors.getColor(Colors.getRandomColor());
                        }
                    }
                }
                break;
            case 2:
                GameType= Type.Color;
                textInit.text= "Color";
                if(mision == 1){
                    c[0]= Colors.getRandomColor();
                    Objetivos[0].resp= Colors.getNameColor(c[0]);
                    Objetivos[0].imgObj.color= Colors.getColor(c[0]);
                    Objetivos[0].imgObj.sprite= Figures.getFigure(Figures.getRandomFigure());
                }else{
                    if(mision == 2){
                        do{
                            c[0]= Colors.getRandomColor();
                            c[1]= Colors.getRandomColor();
                        }while(c[0] == c[1]);
                        for(i=0; i<mision; i++){
                            Objetivos[i].resp= Colors.getNameColor(c[i]);
                            Objetivos[i].imgObj.color= Colors.getColor(c[i]);
                            Objetivos[i].imgObj.sprite= Figures.getFigure(Figures.getRandomFigure());
                        }
                    }else{
                        mision=3;
                        do{
                            c[0]= Colors.getRandomColor();
                            c[1]= Colors.getRandomColor();
                            c[2]= Colors.getRandomColor();
                        }while(c[0]==c[1] && c[1]==c[2] && c[0]==c[2]);
                        for(i=0; i<mision; i++){
                            Objetivos[i].resp= Colors.getNameColor(c[i]);
                            Objetivos[i].imgObj.color= Colors.getColor(c[i]);
                            Objetivos[i].imgObj.sprite= Figures.getFigure(Figures.getRandomFigure());
                        }
                    }
                }
                break;
            case 3:
                GameType= Type.FigureAndColor;
                textInit.text= "Color and Figure";
                break;
            default:
                GameType= Type.Figure;
                textInit.text= "Figure";
                mision= 1;
                f[0]= Figures.getRandomFigure();
                Objetivos[0].resp= Figures.getNameFigure(f[0]);
                Objetivos[0].imgObj.sprite= Figures.getFigure(f[0]);
                Objetivos[0].imgObj.color= Colors.getColor(Colors.getRandomColor());
                Debug.Log("Error Start, selección de juego");
                break;
        }
        for(i=0; i<mision; i++) Objetivos[i].obj.SetActive(true);
        frequency=spawTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Timer.getPausa()){
            frequency += Time.deltaTime;
            if(frequency >= spawTime){
                frequency=0;
                Instantiate();
            }
        }
    }
    private void Instantiate(){
        obj= ctrObjects.RequestObject();
        buttonObject= obj.GetComponent<Object>();
        buttonObject.Reset();
        switch(GameType){
            case Type.Figure:
                objrnd= Figures.getRandomFigure();
                buttonObject.SetResp(Figures.getNameFigure(objrnd));
                buttonObject.ChangeAppearance(
                    Figures.getFigure(objrnd),
                    Colors.getColor(Colors.getRandomColor())
                );
                break;
            case Type.Color:
                objrnd= Colors.getRandomColor();
                buttonObject.SetResp(Colors.getNameColor(objrnd));
                buttonObject.ChangeAppearance(
                    Figures.getFigure(Figures.getRandomFigure()),
                    Colors.getColor(objrnd)
                );
                break;
            default:
                Debug.Log("Erron Instantiate ctrGame");
                break;
        }
        objrnd= (direction==1)? 0: Random.Range(0,4); //2¨1->2 / 2¨2->4 / 2¨3->8
        switch(objrnd){
            case 0: //arriba -> abajo
                buttonObject.ChangePosition(
                    new Vector2 (Random.Range(-width+80f,width-80f), high+increment)
                );
                buttonObject.Direction(Vector3.down); //(0,-1,0)
                break;
            case 1: // abajo -> arriba
                buttonObject.ChangePosition(
                    new Vector2(Random.Range(-width+80f, width-80f), -high-increment)
                );
                buttonObject.Direction(Vector3.up); //(0,1,0)
                break;
            case 2: // izquierda -> derecha
                buttonObject.ChangePosition(
                    new Vector2(-width-increment, Random.Range(-high+80f,high-80f))
                );
                buttonObject.Direction(Vector3.right); //(1,0,0)
                break;
            case 3: // derecha -> izquierda
                buttonObject.ChangePosition(
                    new Vector2(width+increment, Random.Range(-high+80f,high-80f))
                );
                buttonObject.Direction(Vector3.left); //(-1,0,0)
                break;
            default:
                Debug.Log("Erron directon2.ctrGame");
                buttonObject.ChangePosition(new Vector2 (Random.Range(-width+80f,width-80f), high+increment));
                break;
        }
        obj.SetActive(true);
    }
    
    public int Answer(string answer){
        for(ans=0; ans<mision; ans++){
            if(Objetivos[ans].resp.Equals(answer)){
                o=1;
                Objetivos[ans].addPoint();
                break;
            }
            o=2;
        }
        if(o==2) Timer.RemoveTime(10f);
        return o;
    }
    public void OffObject(GameObject gObj){
        gObj.SetActive(false);
    }

}
