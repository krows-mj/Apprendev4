using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class ctrGestionTareas : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    void Update()
    {
        foreach(Tareas.tarea t in Tareas.ListaTareas){
            if(Time.time > t.tiempo){
                //Una vez pase el tiempo establecido, ejecutar accion
                t.accion();
                Tareas.ListaTareas.Remove(t);
                break;
            }
        }
    }
}

//Para ejectuar una accion (después de un tiempo determinado)
public static class Tareas{
    public class tarea{
        public float tiempo; //Tiempo inicio
        public Action accion;
    }

    public static List<tarea> ListaTareas = new List<tarea>();

    public static void Nueva(float t, Action a){
        ListaTareas.Add(new tarea{
            tiempo= Time.time + t,
            accion= a
        });
    }
}