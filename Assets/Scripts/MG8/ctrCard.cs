using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ctrCard : MonoBehaviour
{
    private ctrMG8 ctrGame;
    
    private GameObject Content;
    private GameObject Obj2; //Imagen de respuesta
    [SerializeField]
    private string resp;
    private Image Img;
    private Animator anim;
    private Button btn;
    private int nf, nc; //id de color y figura

    private void Awake(){
        ctrGame= GameObject.Find("Game").GetComponent<ctrMG8>();
        Content= transform.GetChild(0).gameObject;
        Img= Content.GetComponent<Image>();
        Obj2= transform.GetChild(1).gameObject;
        anim= GetComponent<Animator>();
        btn= GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Content.SetActive(false);
        Obj2.SetActive(false);
        nf=0;
        nc=0;
    }

    // Update is called once per frame
    //void Update(){}

    public void ChangeCard(Sprite sp, Color c){
        Img.sprite= sp;
        Img.color= c;
    }
    public void SendAnwer(){
        if(ctrGame.CompareAnswer(resp)){
            GoodAnswer();
        }else{
            BadAnswer();
        }
    }
    public void GoodAnswer(){
        anim.SetTrigger("good");
    }
    public void BadAnswer(){
        anim.SetTrigger("bad");
    }
    public void ResetCard(){
        anim.SetTrigger("reset");
        //Obj2.SetActive(false);
    }
    public void ResetCards(){
        ctrGame.ResetCards();
    }
    
    //Off and On
    public void OnContentCard(){
        Content.SetActive(true);
    }
    public void OffContentCard(){
        Content.SetActive(false);
    }
    public void OnObjAnswer(){
        Obj2.SetActive(true);
    }
    public void OffObjAnswer(){
        Obj2.SetActive(false);
    }
    public void OnButtonCard(){
        btn.enabled= true;
    }
    public void OffButtonCard(){
        btn.enabled= false;
    }

    //Getters and Setter (Obtener y Colocar)
    public void setResp(string cad){
        resp= cad;
    }
    public string getResp(){
        return resp;
    }
    public void setNF(int n){
        nf= n;
    }
    public int getNF(){
        return nf;
    }
    public void setNC(int n){
        nc= n;
    }
    public int getNC(){
        return nc;
    }
}