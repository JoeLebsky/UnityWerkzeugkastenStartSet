// Version 2021-12-19
// läuft auf Fehler wenn es kein LebenPunkteUndDerGanzeRest.cs im Projekt gibt! Das muss an ein Leerobjekt namens GameController angehängt sein!
// Baustellen: noch freie Wahl (Spieler = 0: also wenn Gegenstand von Spieler 1 aufgesammelt dann dessen Punktestand erhöhen, etc.)
using UnityEngine; 

public class KollisionEnergieLebenPunkte : MonoBehaviour{

	// hier könnte man später noch eine public float AlleWievielSekundenWiederholen und eine CoRoutine starten (die z.B. wiederholt Energie abzieht bei längerem Kontakt)

    public string ObjekteMitDiesemTagBeeinflussen = "Player";
    public bool DiesesObjektNachTrefferZerstoeren = false;
     [Header("Was beim Aufsammeln/Koll. verändert wird:")]
	public int PunkteDifferenz = 0;
	public int EnergieDifferenz = -10;
    public int LebenDifferenz = 0;
     [Header("welcher Spieler? (0=der Aufsammelnde)")]
    public int Spieler=1; // welche Spielerstände beeinflusst werden sollen
	private LebenPunkteUndDerGanzeRest VerweisAufProgrammklasse; 

    void Start(){
        // den Verweis auf die Klasse LebenPunkteUndDerGanzeRest (hängt an Objekt "GameController") setzen, sonst kein Aufruf "EnergieAbzug" möglich
        GameObject VerweisAufDenGamecontroller =  GameObject.Find("GameController"); // alternativ kann man im Inspector auch GameController als Tag zuweisen und hier suchen mit: GameObject.FindGameObjectWithTag ("GameController");
		if (VerweisAufDenGamecontroller != null){ 
            try{
            VerweisAufProgrammklasse = VerweisAufDenGamecontroller.GetComponent <LebenPunkteUndDerGanzeRest>(); 
            } catch { }
        }
    }


    private void OnCollisionEnter(Collision collision){
        //Debug.Log("coll.");
        //CompareTag?
        if (ObjekteMitDiesemTagBeeinflussen=="" || collision.gameObject.tag == ObjekteMitDiesemTagBeeinflussen){
            //Debug.Log("coll. korrekter tag");
            if (PunkteDifferenz !=0) { VerweisAufProgrammklasse.PunktestandAendern(PunkteDifferenz, Spieler); }
            if (EnergieDifferenz !=0) { VerweisAufProgrammklasse.EnergieAendern(EnergieDifferenz, Spieler); }
            if (LebenDifferenz !=0) { VerweisAufProgrammklasse.EnergieAendern(LebenDifferenz, Spieler); }
        }
        if (DiesesObjektNachTrefferZerstoeren) { Destroy(gameObject); }
    }
    // und als Variante wenn der Collider vom Typ "Trigger" ist - siehe Anleitung (dann fährt man durch und prallt nicht ab)
    private void OnTriggerEnter(Collider other){
        Debug.Log("coll.trigger");
        if (ObjekteMitDiesemTagBeeinflussen=="" || other.gameObject.CompareTag(ObjekteMitDiesemTagBeeinflussen)){
            Debug.Log("coll.trigger korrekter tag");
            if (PunkteDifferenz !=0) { VerweisAufProgrammklasse.PunktestandAendern(PunkteDifferenz, Spieler); }
            if (EnergieDifferenz !=0) { VerweisAufProgrammklasse.EnergieAendern(EnergieDifferenz, Spieler); }
            if (LebenDifferenz !=0) { VerweisAufProgrammklasse.EnergieAendern(LebenDifferenz, Spieler); }

        }    
    }
}
