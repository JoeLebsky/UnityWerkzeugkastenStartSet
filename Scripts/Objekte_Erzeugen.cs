// Version 2021-02-10
// Baustellen: ggf. noch Option "Objekte dürfen nicht kollidieren"

using UnityEngine; using System.Collections; using System.Collections.Generic; 

public class Objekte_Erzeugen : MonoBehaviour{
    [Header("Welche/s Objekt/e:")]
    public GameObject[] ObjektVorlage; // Hier Platzhalter für alle Objekte, die zufällig gesetzt werden. Man muss die Prefabs im Inspector draufziehen
    [Header("Wo erzeugen:")]
    public Vector3 ErzeugungsbereichVon; public Vector3 ErzeugungsbereichBis;    
    public bool RelativZumSpieler_XAchse = false; public bool RelativZumSpieler_YAchse = true; public bool RelativZumSpieler_ZAchse = false;
    public bool UndZwarInFlugrichtung = true;
    [Header("(0 Sek. zwischen Wellen = nur einmalig)")]
    [Header("Zeitliche Steuerung:")]
    public int WieVieleAufEinmal; 
    public float SekundenZwischenObjekten;     public float SekundenBevorsLosgeht; 
    
    public float SekundenZwischenWellen;
    
    [Header("Auf Boden platzieren:")]
    
    public float WieTiefNachBodenSuchen = 0.0F; public float Einbuddeltiefe = 0.4F;
    [Header("Drehung:")]
    public bool ZufaelligeDrehung = false; public bool ZufaelligeDrehungNurYAchse = false; public bool DrehungVonElternObjekt = false; public Vector3 RotationDesObjekts;
    public bool DrehungVonSpieler = false; 
    [Header("(Lebensdauer 0 heißt dauerhaft)")]
    [Header("Sonstiges:")]
    public float LebensdauerObjekt = 0.0F; 
    
    private GameObject erzeugtesObjekt; private Vector3 relativerVektor; private GameObject Spieler;
    
    void Start(){
        if (RelativZumSpieler_XAchse==true || RelativZumSpieler_YAchse==true || RelativZumSpieler_ZAchse==true || DrehungVonSpieler==true){
            Spieler = GameObject.FindWithTag("Player");
            if (Spieler==null) {
                // Werte zurücksetzen, dann braucht man das in der Zusatzschleife nicht dauernd prüfen (wir gehen mal davon aus dass nicht während des Spiels per Skript ein Objekt zum Spieler gemacht wird)
                RelativZumSpieler_XAchse=false; RelativZumSpieler_YAchse=false; RelativZumSpieler_ZAchse=false; DrehungVonSpieler=false;
            }
        }
        StartCoroutine (Zusatzschleife ());
    }

    IEnumerator Zusatzschleife(){
        // erst mal warten falls gewünscht
        yield return new WaitForSeconds (SekundenBevorsLosgeht);
        // Endlosschleife
        while (true){
            for (int i = 0; i < WieVieleAufEinmal; i++){
                // eins der Objekte zufällig auswählen
                int MyIndex = Random.Range(0, ObjektVorlage.Length); GameObject welcheDerPrefabs = ObjektVorlage[MyIndex];

                // Position bestimmen (ggf. relativ zum Spieler)
                Vector3 woErzeugen = new Vector3 (Random.Range (ErzeugungsbereichVon.x, ErzeugungsbereichBis.x), Random.Range(ErzeugungsbereichVon.y, ErzeugungsbereichBis.y), Random.Range(ErzeugungsbereichVon.z, ErzeugungsbereichBis.z));
                
                if (UndZwarInFlugrichtung==true){
                                relativerVektor = Spieler.transform.rotation * woErzeugen;
                                woErzeugen = relativerVektor ;
                }
                if (RelativZumSpieler_XAchse==true) { woErzeugen.x += Spieler.transform.position.x; }
                if (RelativZumSpieler_YAchse==true) { woErzeugen.y += Spieler.transform.position.y; }
                if (RelativZumSpieler_ZAchse==true) { woErzeugen.z += Spieler.transform.position.z; }

                // Drehung festlegen (je nach gewähltem Häkchen)
                Quaternion DrehungErzeugtesObjekt; DrehungErzeugtesObjekt = Quaternion.identity; // Standardwert, "Nulldrehung"
                if (ZufaelligeDrehung==true) { DrehungErzeugtesObjekt = Random.rotation; }
                if (DrehungVonElternObjekt==true) { DrehungErzeugtesObjekt = transform.rotation; } // das Objekt an dem dieses Skript hängt
                if (ZufaelligeDrehungNurYAchse==true) { 
                    Vector3 rotationVector = new Vector3(0, Random.Range(-90.0F, 90.0F), 0); DrehungErzeugtesObjekt = Quaternion.Euler(rotationVector); }
                if (RotationDesObjekts!=Vector3.zero) { DrehungErzeugtesObjekt = Quaternion.Euler(RotationDesObjekts); }
                if (DrehungVonSpieler==true) { DrehungErzeugtesObjekt = Spieler.transform.rotation; }
                // jetzt noch eventuelle Bodenkorrektur
                if (WieTiefNachBodenSuchen!=0.0F) { float ykorrektur = -Einbuddeltiefe -BodenY(woErzeugen); woErzeugen+=new Vector3(0, ykorrektur, 0); }
                // jetzt endlich das Objekt erzeugen :-)
                erzeugtesObjekt = Instantiate(welcheDerPrefabs, woErzeugen, DrehungErzeugtesObjekt);
                if (LebensdauerObjekt!=0) { Destroy (erzeugtesObjekt, LebensdauerObjekt); }
                // vor dem nächsten Objekt warten (falls im Inspector ein Wert dafür festgelegt wurde, bei 0 gehts gleich weiter)
                yield return new WaitForSeconds (SekundenZwischenObjekten);
            }
            // wenn 0, wieder aufhören nach 1. Durchlauf
            if (SekundenZwischenWellen==0) { yield break; }
            yield return new WaitForSeconds (SekundenZwischenWellen);
        }
    }
        float BodenY(Vector3 start){
        Vector3 s1=start; float t = WieTiefNachBodenSuchen;
        RaycastHit hit; Ray downRay = new Ray(s1, Vector3.down);
        if (Physics.Raycast(downRay, out hit)){
            t = hit.distance; // ich dachte schon wir müssten wiederholt die Distanz halbieren/Startpunkt verschieben bis man es genau hat, aber es gibt ja hit.distance
        }
        return t;
    }
}
    