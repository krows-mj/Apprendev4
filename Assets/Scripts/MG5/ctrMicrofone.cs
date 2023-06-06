using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ctrMicrofone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /*   El juego 5
    Reutilizamos la por completo el script de ctrMG3 por la funcionalidad, 
    en este script capturamos lo pronunciado por el usuario 
    y lo transformamos en texto para comprar la respuesta
    */

    public UnityEvent OnButton;
    public UnityEvent OffButton;

    private ctrTextSpeech ctrTextSpeech;
    [SerializeField] private ctrMG3 ctrMG3;

    public void OnPointerDown(PointerEventData eventData){
        OnButton?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData){
        OffButton?.Invoke();
    }

    private void Awake(){
        ctrTextSpeech= GameObject.Find("ManagerSpeechToText").GetComponent<ctrTextSpeech>();
        //ctrMG3= GetComponent<ctrMG3>();
    }

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    //void Update(){}

    public void Answer(){
        ctrMG3.CompareAnswer(ctrTextSpeech.getAnswerSpeech());
    }
}
