// Version 2020-12-23

// Wichtig: dieses Skript läuft auf Fehler wenn es kein LebenPunkteUndDerGanzeRest.cs im Projekt gibt! Das muss an ein Leerobjekt namens GameController angehängt sein! 
// offen: VerweisAufProgrammklasse ggf. noch "static" machen, dann entfällt unten einiges)

using UnityEngine; // using System.Collections; using System.Collections.Generic;

public class MausklickSpezial : MonoBehaviour {
private LebenPunkteUndDerGanzeRest VerweisAufProgrammklasse; 

     [Header("Was beim Aufsammeln/Koll. verändert wird:")]
	public int PunkteDifferenz = 0; public int EnergieDifferenz = -10; public int LebenDifferenz = 0;
    public bool GegenstandInsInventar = false;
     [Header("welcher Spieler? (0=der Aufsammelnde)")]
    public int Spieler=1; // welche Spielerstände beeinflusst werden sollen
private string tagFuerInventar;
	
    void Start(){
	tagFuerInventar= gameObject.tag; 
    if (tagFuerInventar == "Untagged"){ tagFuerInventar = gameObject.name; }
        // den Verweis auf die Klasse LebenPunkteUndDerGanzeRest (hängt an Objekt "GameController") setzen, sonst kein Aufruf "InventarAendern" möglich
        GameObject VerweisAufDenGamecontroller =  GameObject.Find("GameController"); // alternativ kann man im Inspector auch GameController als Tag zuweisen und hier suchen mit: GameObject.FindGameObjectWithTag ("GameController");
		if (VerweisAufDenGamecontroller != null){ 
            try{
            VerweisAufProgrammklasse = VerweisAufDenGamecontroller.GetComponent <LebenPunkteUndDerGanzeRest>(); 
            } catch { }
        }
    }

    private void OnMouseDown(){
            if (PunkteDifferenz !=0) { VerweisAufProgrammklasse.PunktestandAendern(PunkteDifferenz, Spieler); }
            if (EnergieDifferenz !=0) { VerweisAufProgrammklasse.EnergieAendern(EnergieDifferenz, Spieler); }
            if (LebenDifferenz !=0) { VerweisAufProgrammklasse.EnergieAendern(LebenDifferenz, Spieler); }
	        if (GegenstandInsInventar==true) {VerweisAufProgrammklasse.InventarAendern(tagFuerInventar, 1); Debug.Log("füge "+tagFuerInventar+" zu Inventar hinzu"); }
    Destroy(gameObject);     
    

    }
}
