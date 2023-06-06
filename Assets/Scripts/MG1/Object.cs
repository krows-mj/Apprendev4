using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    private ctrMG1 MG1;
    private ctrTime Timer;
    [Header("Propiedades del Objeto")]
    private RectTransform rt;
    private Image img;
    private Animator anim;
    [SerializeField] private Vector3 dir;
    private float speed;
    [SerializeField] private string resp;
    
    [SerializeField] private float tiempo;
    private Button btn;

    private void Awake(){
        MG1= GameObject.Find("Game").GetComponent<ctrMG1>();
        Timer= GameObject.Find("Game").GetComponent<ctrTime>();
        rt= GetComponent<RectTransform>();
        img= GetComponent<Image>();
        anim= GetComponent<Animator>();
        btn= GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed=400f;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {   
        if(!Timer.getPausa()){
            rt.transform.Translate(dir * Time.deltaTime * speed);
            tiempo -= Time.deltaTime;
            if(tiempo<= 0) Die();
        }
    }
    [ContextMenu("ChangePosition")]
    public void ChangePosition(Vector2 v2){
        rt.anchoredPosition= v2;
    }
    public void Direction(Vector3 v3){
        dir= v3;

    }
    public void ChangeAppearance(Sprite sp, Color c){
        img.sprite= sp;
        img.color= c;
    }
    public void Answer(){
        dir= Vector3.zero;
        anim.SetInteger("answer",MG1.Answer(resp));
        btn.enabled= false;
    }
    public void Die(){
        MG1.OffObject(gameObject);
    }
    public void Reset(){
        //ChangePosition(new Vector3(65f, 1067f, 0f));
        anim.SetInteger("answer",0);
        tiempo= 8f;
        btn.enabled= true;
    }

    //Getters and Setters (Obtener y Colocar)
    public string GetResp(){
        return resp;
    }
    public void SetResp(string r){
        resp= r;
    }
}
