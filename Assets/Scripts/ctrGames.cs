using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrGames : MonoBehaviour
{
    private ctrSceneManager SceneManager;
    [SerializeField] private string[] Read;
    [SerializeField] private string[] Write;
    [SerializeField] private string[] Speak;
    [SerializeField] private string[] Listen;

    private int i;

    private void Awake(){
        SceneManager= GetComponent<ctrSceneManager>();
    }
    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    //void Update(){}

    public void AllPlay(){
        switch (Random.Range(1,5))
        {
            case 1:
                ReadPlay();
                break;
            case 2:
                WritePlay();
                break;
            case 3:
                SpeakPlay();
                break;
            case 4:
                ListenPlay();
                break;
            default:
                Debug.Log("Erron AllPlay");
                AllPlay();
                break;
        }
    }

    public void ReadPlay(){
        i= Random.Range(0, Read.Length);
        GamePlay(Read[i]);
    }
    public void WritePlay(){
        i=Random.Range(0, Write.Length);
        GamePlay(Write[i]);
    }
    public void SpeakPlay(){
        i=Random.Range(0, Speak.Length);
        GamePlay(Speak[i]);
    }
    public void ListenPlay(){
        i= Random.Range(0, Listen.Length);
        GamePlay(Listen[i]);
    }

    private void GamePlay(string c){
        SceneManager.CambiarScena(c);
    }
}
