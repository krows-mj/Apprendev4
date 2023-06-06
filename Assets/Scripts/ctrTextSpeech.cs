using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TextSpeech;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ctrTextSpeech : MonoBehaviour
{
    [SerializeField] private string languaje= "en-US";
    [SerializeField] private Text txt;
    [SerializeField] private string answerSpeech=""; //texto obtenido de escuchar por microfono

    private void Awake(){
        //Condicional de compilación
        #if UNITY_ANDROID
        //Pedir permiso para el microfono en caso de no tenerlo
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone)){
            Permission.RequestUserPermission(Permission.Microphone);
        }
        #endif
    }
    // Start is called before the first frame update
    void Start()
    {
        TextToSpeech.Instance.Setting(languaje, 1, 1);
        SpeechToText.Instance.Setting(languaje);

        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
        #if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
        #endif        
    }

    // Update is called once per frame
    //void Update(){}

    //-----     //Speech to text//  -------//
    // Escuchar por microfono
    public void StartListening(){
        SpeechToText.Instance.StartRecording();
    }
    //dejar de escuchar por microfono
    public void StopListening(){
        SpeechToText.Instance.StopRecording();
    }
    //Texto obtenido luego de hablar
    public void OnFinalSpeechResult(string result){
        answerSpeech= result;
    }
    //Mostrar el texto según se esta hablando
    public void OnPartialSpeechResult(string result){
        txt.text=result;
    }
    

    //-----     //Text to speech//  -------//
    //Hablar el mensaje recibido
    public void StartSpeaking(string message){
        TextToSpeech.Instance.StartSpeak(message);
    }
    public void StopSpeaking(){
        TextToSpeech.Instance.StopSpeak();
    }


    //Se ejecuta cuando empieza a hablar
    public void OnSpeakStart(){
        Debug.Log("Empieza a hablar...");
    }
    //Se ejecuta cuando termina de hablar
    public void OnSpeakStop(){
        Debug.Log("Dejando de hablar...");
    }

    //Getter and Setter
    public string getAnswerSpeech(){
        return answerSpeech;
    }
}
