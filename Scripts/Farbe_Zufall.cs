// weist dem Objekt an dem es hängt eine Zufallsfarbe zu
// Version 2020-11-10

using System.Collections; using System.Collections.Generic; using UnityEngine;

public class Farbe_Zufall : MonoBehaviour{
    public bool NurVierGrundfarben = false;
    void Start(){
        Color[] farbauswahl = { Color.red, Color.green, Color.blue, Color.yellow };
        if (NurVierGrundfarben==false)
        { transform.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(); }
        else
        { 
            var welcheFarbe = Random.Range(0, farbauswahl.Length);
            transform.gameObject.GetComponent<Renderer>().material.color = farbauswahl[welcheFarbe];
            }
    }

}
