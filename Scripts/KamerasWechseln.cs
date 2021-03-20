// Version 2020-06-14

// Liest beim Start alle (hm. nur aktiven) Kameras aus und wechselt dann alle x Sekunden zwischen ihnen (durch User einstellbar)
// (könnte man auch händisch im Inspektor verknüpfen, aber so zieht man dieses Skript einfach auf ein Objekt und es geht automatisch :-)
using UnityEngine; using System; using System.Collections; using System.Collections.Generic;

public class KamerasWechseln : MonoBehaviour{
    public float AnzeigedauerBeiAutomatischemWechsel=2.0F; public bool ManuellUmschaltenZiffertaste=false;
    private Camera[] KameraListe;
    private int aktuelleKamera = 0; private char Taste;

    void Start(){
        // alle Kameras einlesen (Alternative: oben bei Camera[] statt private: public, und die Kameras im Inspektor per Drag und Drop verknüpfen)       
        KameraListe = FindObjectsOfType<Camera>(); // ginge auch Camera.allCameras?
        // den 2-sekundlichen Wechsel anstoßen (in separater Routine, damit nicht das ganze Programm ausgebremst wird)
        if(AnzeigedauerBeiAutomatischemWechsel>0) { StartCoroutine (KameraWeiterschalten()); }
    }
    void OnGUI(){
        Event e = Event.current;
        if (ManuellUmschaltenZiffertaste==false) { return; }
        if (e.isKey){
            //Debug.Log(e.keyCode.ToString());
            // vorerst mal keine elegantere Lösung gefunden ... ggf. e.keyCode.ToInt()-1?
            if (e.keyCode== KeyCode.Keypad1 || e.keyCode== KeyCode.Alpha1) { aktuelleKamera=0; }
            if (e.keyCode== KeyCode.Keypad2 || e.keyCode== KeyCode.Alpha2) { aktuelleKamera=1; }
            if (e.keyCode== KeyCode.Keypad3 || e.keyCode== KeyCode.Alpha3) { aktuelleKamera=2; }
            if (e.keyCode== KeyCode.Keypad4 || e.keyCode== KeyCode.Alpha4) { aktuelleKamera=3; }
            if (e.keyCode== KeyCode.Keypad5 || e.keyCode== KeyCode.Alpha5) { aktuelleKamera=4; }
            if (e.keyCode== KeyCode.Keypad6 || e.keyCode== KeyCode.Alpha6) { aktuelleKamera=5; }
            if (e.keyCode== KeyCode.Keypad7 || e.keyCode== KeyCode.Alpha7) { aktuelleKamera=6; }
            if (e.keyCode== KeyCode.Keypad8 || e.keyCode== KeyCode.Alpha8) { aktuelleKamera=7; }
            if (e.keyCode== KeyCode.Keypad9 || e.keyCode== KeyCode.Alpha9) { aktuelleKamera=8; }
            if (aktuelleKamera<KameraListe.Length) { KameraUmschalten(); }
        }
    }
   
     IEnumerator KameraWeiterschalten(){
        // Dauerschleife
        while (true){
            aktuelleKamera++; if (aktuelleKamera>=KameraListe.Length) { aktuelleKamera = 0; }
            KameraUmschalten();
            // für bestimmte Anzahl Sekunden anhalten (kein Problem, da es nicht das gesamte Programm ist)
            yield return new WaitForSeconds (AnzeigedauerBeiAutomatischemWechsel);
        }
    }
    void KameraUmschalten(){
        for (int i = 0; i < KameraListe.Length; i++){
            if (i==aktuelleKamera)
                { KameraListe[i].gameObject.SetActive (true); }
            else
                { KameraListe[i].gameObject.SetActive (false); }
        }
    }
}
