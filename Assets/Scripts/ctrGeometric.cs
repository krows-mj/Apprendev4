using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrGeometric : MonoBehaviour
{
    [System.Serializable]
    public struct FigGeometric{
        [SerializeField]
        private string name;
        [SerializeField]
        private Sprite figure;

        public string getName(){
            return name;
        }
        public Sprite getFigure(){
            return figure;
        }
    }
    public FigGeometric[] GeometricFigures;

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    //void Update(){}

    public string getNameFigure(int n){
        return GeometricFigures[n].getName();
    }
    public Sprite getFigure(int n){
        return GeometricFigures[n].getFigure();
    }
    public int getRandomFigure(){
        return Random.Range(0,GeometricFigures.Length);
    }
}
