// Version 2020-11-25
// sowohl für Spiele wo Ball o.ä. Punktestände verändert, als auch für Aufsammel-Spiele

/*  Baustellen: 
 -  Texte noch komplett dynamisch generieren, wie das "Gewonnen" bei Winterwonderland. Vorerst aber muss man die Texte selbst anlegen und hier im Inspector verknüpfen
 -  eine weitere Verbesserung wären natürlich ein Sprite/Image-Energiebalken und Herzsymbole (Unicode-Emojis?) für Leben etc.
 - und natürlich statt _Spieler1, _Spieler2 noch Arrays (1-x Spieler)
 - und jeder Spieler seine InventarAnAus-Taste
 - if (Lebensenergie_Spieler1<=0) { LebenAbziehen... wenn Leben = 0 Spiel beendet etc. }
 - // if (AnzahlPunkte<=0) { könnte irgendwas passieren }
 - Inventar geht aktuell nur mit Spieler1
*/

using UnityEngine; using UnityEngine.UI; using System.Collections; using System.Collections.Generic;


public class LebenPunkteUndDerGanzeRest : MonoBehaviour{
    public int AnzahlPunkte_Spieler1 = 0; public  int Lebensenergie_Spieler1 = 100; public  int AnzahlLeben_Spieler1 = 3; 
      public int AnzahlPunkte_Spieler2 = 0; public  int Lebensenergie_Spieler2 = 100; public  int AnzahlLeben_Spieler2 = 3; 
    
    public Text lebentext_Spieler1, lebentext_Spieler2;   public Text energietext_Spieler1, energietext_Spieler2;
    public Text punktetext_Spieler1, punktetext_Spieler2; public Text inventartext_Spieler1, inventartext_Spieler2;
    private Dictionary<string, int> inventarliste_Spieler1; private bool InventarSichtbar_Spieler1 = false; private string inventarauflistung_Spieler1 = "";
    private Dictionary<string, int> inventarliste_Spieler2; private bool InventarSichtbar_Spieler2 = false; private string inventarauflistung_Spieler2 = "";

    void Start(){
        if(inventartext_Spieler1!=null) { inventarliste_Spieler1 = new Dictionary<string, int>(); inventartext_Spieler1.text = ""; }
        if(inventartext_Spieler2!=null) { inventarliste_Spieler2 = new Dictionary<string, int>(); inventartext_Spieler2.text = ""; }
        // man könnte oben andere public-Variablen wie AnzahlLebenBeiStart (ohne static) anbieten die der User befüllt und hier dann AnzahlLeben=AnzahlLebenBeiStart
    }
    void Update(){
        if (Input.GetKeyDown("i")){ InventarAnAus(); }
    }

    void InventarAnAus(){
        if (InventarSichtbar_Spieler1==false) { InventarSichtbar_Spieler1=true; inventartext_Spieler1.text = inventarauflistung_Spieler1; } else { InventarSichtbar_Spieler1=false; inventartext_Spieler1.text = ""; }
        if (InventarSichtbar_Spieler2==false) { InventarSichtbar_Spieler2=true; inventartext_Spieler2.text = inventarauflistung_Spieler2; } else { InventarSichtbar_Spieler2=false; inventartext_Spieler2.text = ""; }
    }


    /*      -   -   -   -   -   -   -   -
        Die untenstehenden MethodeN werden von anderen Skripten (z.B. KollidiereMitSpieler.cs) die an anderen Objekten angehängt sind, aufgerufen
          -   -   -   -   -   -   -   - */

    public void EnergieAendern(int wieviel, int Spieler){
        // ein KollidiereMitSpieler-Skript kann auch Energie abziehen durch negativen Wert 
        if (Spieler==1) { Lebensenergie_Spieler1+=wieviel; energietext_Spieler1.text = Lebensenergie_Spieler1.ToString(); }
        else { Lebensenergie_Spieler2+=wieviel; energietext_Spieler2.text = Lebensenergie_Spieler2.ToString(); }
    } 

    public void PunktestandAendern(int wieviel, int Spieler){
        // ein KollidiereMitSpieler-Skript kann auch Punkte abziehen
        if (Spieler==1) { AnzahlPunkte_Spieler1+=wieviel; punktetext_Spieler1.text = AnzahlPunkte_Spieler1.ToString(); }
        else { AnzahlPunkte_Spieler2+=wieviel; punktetext_Spieler2.text = AnzahlPunkte_Spieler2.ToString(); }
    } 

    public void InventarAendern(string welches, int wieviel){
        Debug.Log("Füge "+welches+" zum Inventar  hinzu");
        //if (Spieler==1){
            int anz; inventarliste_Spieler1.TryGetValue(welches, out anz);
            inventarliste_Spieler1[welches] = anz + 1; inventarauflistung_Spieler1 = "";
            foreach(var item in inventarliste_Spieler1){
                inventarauflistung_Spieler1 = inventarauflistung_Spieler1 + item.Key + ": " + item.Value.ToString() + System.Environment.NewLine; // bei längeren Listen sollte man aber StringBuilder verwenden
                if (InventarSichtbar_Spieler1==true){ InventarSichtbar_Spieler1=false; InventarAnAus(); } // etwas komplizierte Lösung für "Aktualisiere die Anzeige falls gerade eingeblendet" :-)
            }
        //}
    }

}
