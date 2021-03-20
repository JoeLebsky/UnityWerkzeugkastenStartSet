/* Version 2020-11-07
   Liest beim Start alle Kameras ein. Verteilt deren Anzeigen auf dem Bildschirm.
   (könnte man auch händisch jeweils im Inspektor, aber so zieht man dieses Skript einfach auf ein Objekt und es geht automatisch :-)

  --- Bugs: ---
    - es ignoriert die "Main Camera" ... ist die nicht von Type "Camera"? :-)

  --- nächste Features: ---
    - für jede Kamera wird nur der jeweilige "rect"-Bereich gezeichnet, aber nicht jeweils das mittige Stück der Kamera ...ggf. irgendwie händisch verschieben?
*/

using System.Collections; using System.Collections.Generic; using UnityEngine;

public class KamerasAnordnen : MonoBehaviour{
    [Header("Neben- und Untereinander auch kombinierbar")]
    public bool Nebeneinander = true; public bool Untereinander = false;
    private Camera[] KameraListe; 

    void Start(){
        // ggf. noch was lustiges überlegen für "beides nicht angehakt" :-)
        if (Nebeneinander==false && Untereinander==false){ return; } 
        // alle Kameras einlesen (Alternative: oben Camera[] private statt public, und die Kameras im Inspektor per Drag und Drop verknüpfen)       
        KameraListe = FindObjectsOfType<Camera>(); 
        // wenn nebeneinander UND untereinander, schachteln wir zwei for/next-Durchlaufschleifen ineinander...
        if (Nebeneinander==true && Untereinander==true){
            // wie oft nebeneinander? z.B. bei 4 Kameras 2 (und 2 darunter). Bei 5 Kameras  brauchen wir 3 nebeneinander und darunter 2 ... daher müssen wir trickreich runden
            
            int Anzahl_Kameras_X_Achse = (int)Mathf.Round(Mathf.Sqrt(KameraListe.Length)+0.5F); // Debug.Log(Anzahl_Kameras_X_Achse);
            // und das "wie oft untereinander" ergibt sich durch Kamerazahl durch "wie oft nebeneinander". 4 durch 2 nebeneinander = 2 untereinander, 5 durch 3 nebeneinander auch 2 untereinander, etc.
            int Anzahl_Kameras_Y_Achse = (int)Mathf.Round((KameraListe.Length / Anzahl_Kameras_X_Achse)+0.5F); //Debug.Log(Anzahl_Kameras_Y_Achse);
            // und: Vorsicht, bei 1 / Anzahl_Kameras_... wird fälschlicherweise auf Null abgerundet :-)    
            float Schrittweite_X = (float)(1.0F / Anzahl_Kameras_X_Achse); float Schrittweite_Y = (float)(1.0F / Anzahl_Kameras_Y_Achse);
            // hier nicht so arg verwirren lassen, die Startpositionen sind 0 und die Endpositionen bei 2 hoch, 2 quer jeweils 0.5
            // (denn die Bereiche des Bildschirm gehen von 0 links bis 1 rechts bzw. 0 oben bis 1 unten)
            float x_start; float y_start=0.0F; float x_bis; float y_bis = y_start + Schrittweite_Y; int aktuelleKamera = 0; 
            // und jetzt eben "geschachtelt": wir fangen mit der ersten (nullten) Reihe an...
            for (int y = 0; y < Anzahl_Kameras_Y_Achse; y++){
                // setzen jedesmal die x-Werte (rechts links) zurück (in jeder neuen Reihe soll wieder links angefangen werden)
                x_start=0.0F; x_bis=x_start + Schrittweite_X; 
                // und setzen Kamerabild für Kamerabild nebeneinander
                for (int x = 0; x < Anzahl_Kameras_X_Achse; x++){    
                    // das hier ist die eigentliche Kernzeile, alles andere nur "Gedöns" :-) : 
                    // für die Kamera Nummer "aktuelleKamera" wird der Darstellungsbereich (rect) festgelegt
                    KameraListe[aktuelleKamera].rect = new Rect(x_start, y_start, x_bis, y_bis);
                    // wenn wir damit fertig sind, gehen wir zur nächsten Kamera ... und setzen die x-Werte start/ende (bzw. "bis") ein Stück nach rechts
                    aktuelleKamera++; if (aktuelleKamera>=KameraListe.Length) { return; }
                    x_start = x_start + Schrittweite_X; x_bis = x_bis + Schrittweite_X;
                }
                // ... und eine Zeile runterrutschen
                y_start = y_start + Schrittweite_Y; y_bis = y_bis + Schrittweite_Y;
            }
            return; // fertig, Startroutine verlassen
        }
        // bleiben die Fälle "nur nebeneinander" und "nur untereinander"
        
        float Teilfaktor = (1.0F / KameraListe.Length); float von = 0.0F; float bis = von + Teilfaktor; 
        // etwas anders wie oben: alle Kameras durchlaufen... 
        for (int i = 0; i<KameraListe.Length ; i++){
            // jeweiligen Anzeigebereich (Rect) mit den Parametern xstart, ystart, xende, yende) zuordnen ...
            if (Nebeneinander==true && Untereinander==false){ KameraListe[i].rect  = new Rect(von, 0.0F, bis, 1.0F); }
            if (Nebeneinander==false && Untereinander==true){ KameraListe[i].rect  = new Rect(0.0F, von, 1.0F, bis); }    
            // und ein Stückchen weiterschieben (runter oder rechts) für die nächste Kamera
            von += Teilfaktor; bis = von + Teilfaktor;
        }
    }
}
