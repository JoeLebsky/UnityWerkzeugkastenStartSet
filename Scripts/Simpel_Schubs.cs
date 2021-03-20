// das große Schubs-Skript ist schon abschreckend kompliziert geworden wegen der ganzen Parameter/Wahlmöglichkeiten :-)
// hier die simple Variante (Version 2021-02-14)

using UnityEngine;

public class Simpel_Schubs : MonoBehaviour{
    
    // hier der Maximalwert die der Benutzer im Inspector einstellen kann (noch simpler wäre, den Wert vorzugeben)
    public float MaxInBeliebigerRichtung = 200.0F;
    void Start(){
        // beim Start wird aus dem Bereich (Range) zwischen 0 und Maximalwert oben zufällig (Random) ein Wert gewählt
        float wieStark = Random.Range(0.0F, MaxInBeliebigerRichtung); 
        // das ist noch keine Richtung, nur die Größe/Länge... die Funktion insideUnitSphere spuckt einen Vektor Länge 1 in beliebige Richtung aus
        Vector3 wohin = Random.insideUnitSphere;
        // jetzt noch mit der Länge multiplizieren
        wohin = wohin * wieStark;
        // und jetzt der eigentliche Befehl :-) füge auf den Körper (rigidbody) dieses Objekts (wo das Skript hängt) die Kraft aus
        GetComponent<Rigidbody>().AddForce(wohin); // Welt-Koordinaten, aber wir haben ja eh Zufallsrichtung. Und die voreingestellte Kraftart "Force"
        // ist die kleinste, daher der Maximalwert oben höher gewählt
        // So. Puh. Jetzt hab ich aus der simplen Zeile " rigidbody.addforce doch wieder einen Roman gemacht... aber alles Erläuterungen :-)
    }

    
}
