// Version 2020-11-12
// BodenY raus!
using UnityEngine;

public class Objekte_Anordnen : MonoBehaviour{
    public int AnzahlX=3; public int AnzahlY=3; public int AnzahlZ=3;
    public float Abstand = 8.0F; 
    //public Transform Objektvorlage; 
    public GameObject[] ObjektVorlage; // Hier Platzhalter für alle Objekte, die zufällig gesetzt werden. Man muss die Prefabs im Inspector draufziehen
    public float AnteilLeerfelder = 0.1F;
    public Vector3 StartKoordinaten = Vector3.zero; public bool RelativZumSpieler = false; public float LebensdauerObjekt = 0.0F; 
	[Header("Einstellungen zur Drehung:")]
    public bool ZufaelligeDrehung = false; public bool ZufaelligeDrehungNurYAchse = false; public bool DrehungVonElternObjekt = false; public Vector3 RotationDesObjekts;
    public float WieTiefNachBodenSuchen = 0.0F; public float Einbuddeltiefe = 0.4F;
	private GameObject erzeugtesObjekt; private Vector3 woerzeugen;

    void Start (){
        // falls "relativ zum Spieler": diesen natürlich finden :-) und anschließend (wenn es ein Objekt mit Tag Player gibt) die Startkoordinaten verschieben
        if (RelativZumSpieler==true){
            GameObject Spieler = GameObject.FindWithTag("Player");
            if (Spieler!=null){
                StartKoordinaten += Spieler.transform.position;
            }
        }
        // einmal beim Start drei ineinandergeschachtelte Schleifen durchlaufen, für jede Achse eine
        for (int x = 1; x<=AnzahlX; x++){
            for (int y = 1; y<=AnzahlY; y++){
                for (int z = 1; z<=AnzahlZ; z++){
                    // Zufallszahl zwischen 0 und 1. nur wenn größer als Grenzwert dann das Objekt erzeugen, sonst  Lücke lassen
                    if (Random.Range(0.0f, 1.0f) >= AnteilLeerfelder){
                        woerzeugen = StartKoordinaten + new Vector3 (x*Abstand, y*Abstand, z*Abstand);
                        // eins der Objekte zufällig auswählen
                        int MyIndex = Random.Range(0, ObjektVorlage.Length); GameObject welcheDerPrefabs = ObjektVorlage[MyIndex];
                // Drehung festlegen (je nach gewähltem Häkchen)
                Quaternion spawnRotation; spawnRotation = Quaternion.identity; // Standardwert, "Nulldrehung"
                if (ZufaelligeDrehung==true) { spawnRotation = Random.rotation; }
                if (DrehungVonElternObjekt==true) { spawnRotation = transform.rotation; } // das Objekt an dem dieses Skript hängt
                if (ZufaelligeDrehungNurYAchse==true) { 
                    Vector3 rotationVector = new Vector3(0, Random.Range(-90.0F, 90.0F), 0); spawnRotation = Quaternion.Euler(rotationVector); }
                if (RotationDesObjekts!=Vector3.zero) { spawnRotation = Quaternion.Euler(RotationDesObjekts); }
                        // jetzt noch eventuelle Bodenkorrektur
                        if (WieTiefNachBodenSuchen!=0.0F) { float ykorrektur = -Einbuddeltiefe -BodenY(woerzeugen); woerzeugen+=new Vector3(0, ykorrektur, 0); }

                        erzeugtesObjekt = Instantiate (welcheDerPrefabs, woerzeugen, spawnRotation);
                        if (LebensdauerObjekt!=0) { Destroy (erzeugtesObjekt, LebensdauerObjekt); }
                        // man könnte auch einfache Formen erzeugen: #endregion
                        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); cube.transform.position = new Vector3(x*Abstand, y*Abstand, z*Abstand);    
                    }
                }
            }
        }
    }
     // doppelt vorgehalten Quatsch.
      float BodenY(Vector3 start){
        Vector3 s1=start; float t = WieTiefNachBodenSuchen;
        RaycastHit hit; Ray downRay = new Ray(s1, Vector3.down);
        if (Physics.Raycast(downRay, out hit)){
            t = hit.distance; // ich dachte schon wir müssten wiederholt die Distanz halbieren/Startpunkt verschieben bis man es genau hat, aber es gibt ja hit.distance
        }
        return t;
    }
}