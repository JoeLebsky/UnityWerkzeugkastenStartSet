// Version 2020-06-14
using System.Collections; using System.Collections.Generic; using UnityEngine;

public class ObjekteAnordnen_Zufallsreihenfolge_Pausen : MonoBehaviour{
	public int AnzahlX=3; public int AnzahlY=3; public int AnzahlZ=3;	
	public float Abstand = 8.0F; public GameObject Objektvorlage; public float AnteilLeerfelder = 0.1F;
	public Vector3 StartKoordinaten = Vector3.zero; public bool RelativZumSpieler = false;
	public float PausendauerNachJedemObjekt;  public bool NichtZeilenweiseSondernZufaellig = false;
    	public bool ZufaelligeDrehung = false; public bool ZufaelligeDrehungNurYAchse = false; public bool DrehungVonElternObjekt = false; public Vector3 RotationDesObjekts;
	public float LebensdauerObjekt = 0.0F; 
	private GameObject erzeugtesObjekt; private Vector3 woerzeugen; private Quaternion spawnRotation; 
	
	void Start (){
		if (NichtZeilenweiseSondernZufaellig==false){
			StartCoroutine (GeordneteErzeugungsschleife ()); // würde man das nicht machen würde durch das Warten zwischen den Objekten das ganze Programm blockiert
        		}
        		else
        		{  StartCoroutine (ZufaelligeErzeugungsschleife ()); }
	}

	IEnumerator GeordneteErzeugungsschleife(){
        // falls "relativ zum Spieler": diesen natürlich finden :-) und anschließend (wenn es ein Objekt mit Tag Player gibt) die Startkoordinaten verschieben
        if (RelativZumSpieler==true){
            GameObject Spieler = GameObject.FindWithTag("Player");
            if (Spieler!=null){ StartKoordinaten += Spieler.transform.position; }
        }
        // einmal beim Start drei ineinandergeschachtelte Schleifen durchlaufen, für jede Achse eine
        for (int x = 1; x<=AnzahlX; x++){
            for (int y = 1; y<=AnzahlY; y++){
                for (int z = 1; z<=AnzahlZ; z++){
                    // Zufallszahl zwischen 0 und 1. nur wenn größer als Grenzwert dann das Objekt erzeugen, sonst  Lücke lassen
                    if (Random.Range(0.0f, 1.0f) >= AnteilLeerfelder){
                        woerzeugen = StartKoordinaten + new Vector3 (x*Abstand, y*Abstand, z*Abstand);
		                DrehungSetzen();
                        erzeugtesObjekt = Instantiate (Objektvorlage, woerzeugen, spawnRotation);
                        yield return new WaitForSeconds (PausendauerNachJedemObjekt);
                    }
                }
            }
        }
    }
 
    IEnumerator ZufaelligeErzeugungsschleife(){
        // falls "relativ zum Spieler": diesen natürlich finden :-) und anschließend (wenn es ein Objekt mit Tag Player gibt) die Startkoordinaten verschieben
        if (RelativZumSpieler==true){
            GameObject Spieler = GameObject.FindWithTag("Player");
            if (Spieler!=null){ StartKoordinaten += Spieler.transform.position; }
        }
        // das Feld für das "Element schon gesetzt"?
        bool[, , ] wasschongefuellt = new bool[AnzahlX + 1, AnzahlY+1, AnzahlZ+1]; 
        int AnzahlElemente; AnzahlElemente = AnzahlX * AnzahlY * AnzahlZ; int Abbruchcounter; int zufallX; int zufallY; int zufallZ;
        for (int i = 1; i < AnzahlElemente+1; i++){ 
            Abbruchcounter =0;
            // so lange wiederholen bis ein leeres Feld gefunden wird (oder der Sicherheitsabbruch gezogen wird.)
            do
            {
                zufallX = Random.Range(1, AnzahlX+1); zufallY = Random.Range(1, AnzahlY+1); zufallZ = Random.Range(1, AnzahlZ+1);               
                Abbruchcounter++;
            } while (wasschongefuellt[zufallX, zufallY, zufallZ] == true && Abbruchcounter<32000);
            if (Abbruchcounter>=31999) { Debug.Log("Befüllung abgebrochen bei " + i.ToString()); }

            // Zufallszahl zwischen 0 und 1. nur wenn größer als Grenzwert dann das Objekt erzeugen, sonst Lücke lassen. 
            if (Random.Range(0.0f, 1.0f) >= AnteilLeerfelder){
                woerzeugen = StartKoordinaten + new Vector3 (zufallX*Abstand, zufallY*Abstand, zufallZ*Abstand);
	            DrehungSetzen();
                erzeugtesObjekt = Instantiate (Objektvorlage, woerzeugen, spawnRotation);
	if (LebensdauerObjekt!=0) { Destroy (erzeugtesObjekt, LebensdauerObjekt); }
            }
            wasschongefuellt[zufallX, zufallY, zufallZ] = true; // In beiden Fällen aber wasschongefuellt setzen
            yield return new WaitForSeconds (PausendauerNachJedemObjekt);
        }
    }

	void DrehungSetzen(){
	// Drehung festlegen (je nach gewähltem Häkchen)
                spawnRotation = Quaternion.identity; // Standardwert, "Nulldrehung"
                if (ZufaelligeDrehung==true) { spawnRotation = Random.rotation; }
                if (DrehungVonElternObjekt==true) { spawnRotation = transform.rotation; } // das Objekt an dem dieses Skript hängt
                if (ZufaelligeDrehungNurYAchse==true) { 
                    Vector3 rotationVector = new Vector3(0, Random.Range(-90.0F, 90.0F), 0); spawnRotation = Quaternion.Euler(rotationVector); }
                if (RotationDesObjekts!=Vector3.zero) { spawnRotation = Quaternion.Euler(RotationDesObjekts); }
	}
}
