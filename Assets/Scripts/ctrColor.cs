using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrColor : MonoBehaviour
{
    [System.Serializable]
    public struct TheColor{   
        [SerializeField]
        private string name;
        [SerializeField]
        private Color color;

        public string getName(){
            return name;
        }
        public Color getColor(){
            return color;
        }
    }
    [SerializeField]
    private TheColor[] Colors;

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    //void Update(){}

    public string getNameColor(int n){
        return Colors[n].getName();
    }
    public Color getColor(int n){
        return Colors[n].getColor();
    }
    public int getRandomColor(){
        return Random.Range(0,Colors.Length);
    }
}
