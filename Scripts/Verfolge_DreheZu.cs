/* Version 2021-03-18
    Beschreibung: verfolgt das Objekt (bzw. das näheste) mit einem bestimmten "Tag". Dreht sich hin

    Baustellen:
 -  gut wäre noch ein "Hindrehen: nur um Y-Achse"
 -  und ein Beschleunigen beim Verfolgen
 -  und ein Minimalabstand (nicht wann Verfolgung beginnt, sondern Minimalabstand der beim Verfolgen eingehalten wird, wenn Spieler also langsamer, soll z.B. die Kamera ihn nicht überholen)

*/

using UnityEngine; using System.Collections; using System.Collections.Generic; // für "List" benötigt

public class Verfolge_DreheZu : MonoBehaviour {

    // alle möglichen Variablen
    [Header("Geschw. 0 ist fester Standort")]
    public float Geschwindigkeit=20; public string FolgeObjektMitTag = "Player"; 
    public bool ZuZielHindrehen=true; public float ErstFolgenWennDistanzKleinerAls=0.0F; public bool StartabstandBeibehalten=false;
    public bool Weglaufen = false;
    private Transform zielobjekt; private List<Transform> zielliste = new List<Transform>(); private Vector3 Startabstand = Vector3.zero;
    
    private void Start(){
        // Liste aller Spieler erzeugen
        List<GameObject> targetGOs = new List<GameObject>();
        targetGOs.AddRange(GameObject.FindGameObjectsWithTag(FolgeObjektMitTag));
        foreach (GameObject g in targetGOs)         {             zielliste.Add(g.transform);         }
        if (StartabstandBeibehalten==true && zielliste.Count == 1) { Startabstand=transform.position - zielliste[0].position; }
    }
	
	void LateUpdate () {
        // etwas folgen sollte man in LateUpdate, nicht in Update, dadurch die Zielposition schon aktualisiert für diesen Zyklus
        // Grundwerte setzen
        Vector3 direction = Vector3.zero; float distance = Mathf.Infinity;
        // alle Objekte (targets) mit der gewünschten Markierung durchlaufen (kann auch eins sein :-) 
        foreach (Transform zupruefen in zielliste){
            // Richtung und Distanz ermitteln
            Vector3 thisDirection = zupruefen.position - transform.position; float thisDistance = thisDirection.sqrMagnitude;
            // falls sie kleiner als die bisherige ist, wird es das neue Ziel
            if (thisDistance<distance){
                direction = thisDirection; distance = thisDistance; zielobjekt = zupruefen;
            }
        }
        // jetzt haben wir das mit dem geringesten Abstand. Wenn es das Objekt wirklich gibt...
        if (direction != Vector3.zero){
            // und wir laufen im Moment nicht weg statt hin ...
            if (Weglaufen==false){
                // bei Startabstand beibehalten (und es gibt nur einen Spieler): weiterhin Startabstand beibehalten
                if (StartabstandBeibehalten==true && zielliste.Count == 1) { 
                    transform.position = zielliste[0].position + Startabstand;
                } 
                // nichts mit Startabstand. Also bewegen ...
                else {
                    //Debug.Log(distance + " ----- " + (ErstFolgenWennDistanzKleinerAls*ErstFolgenWennDistanzKleinerAls).ToString());
                    // außer das soll erst ab einer bestimmten Distanz erfolgen und die ist noch nicht erreicht, dann Routine wieder verlassen
                    if (ErstFolgenWennDistanzKleinerAls!=0 && distance>ErstFolgenWennDistanzKleinerAls*ErstFolgenWennDistanzKleinerAls) { return; }
                    // so. Jetzt endlich. Aufs Objekt zubewegen...
                    transform.position = Vector3.MoveTowards(transform.position, zielobjekt.position, Geschwindigkeit * Time.deltaTime); }
                }
            else
            {
                // jetzt sind wir im Bereich "Weglaufen ist angehakt" (von Anfang an oder durchs Skript "Kollision Spezial")
                Vector3 Richtung = transform.position - zielobjekt.position; Richtung = Richtung.normalized * Geschwindigkeit * Time.deltaTime;
                direction = Richtung; Richtung = Richtung * (-1);
                transform.Translate(Richtung); 
            }
            if (ZuZielHindrehen==true) { transform.forward = direction; }  // "Gesicht" Richtung Ziel... falls der Haken gesetzt ist 
        }
	}

    public void InvertiereJagdtrieb(){
        if (Weglaufen==false) { Weglaufen=true; }
        else
        { Weglaufen=false; }
    }
}
