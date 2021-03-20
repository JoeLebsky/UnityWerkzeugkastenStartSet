/* Version 2021-03-18
  Beschreibung: verändert die Größe (und, wenn gewünscht, den Ankerpunkt (liegt in der Mitte von Objekten), damit die )

  Baustellen:
	- ist momentan nur sauber bei einem Objekt der Größe 1, 1, 1  :-) ... das natürlich korrigieren
	- und für das "in welche Richtung die Verzerrung" noch bessere Wahlmöglichkeiten bieten
*/

using System.Collections; using System.Collections.Generic; using UnityEngine;

public class Skalieren_Groesse_Aendern : MonoBehaviour{
    public float AnzeigeDauer = 0.5F;
    public Vector3 Verzerrung = new Vector3(1.0F, 1.0F, 50.0F);
    public bool InAlleRichtungen = false;
    private Vector3 OriginalGroesse; private Vector3 temporaerGroesse; private Vector3 korrektur;
	
    void Start(){
        OriginalGroesse = transform.localScale;
        StartCoroutine (GroesseVeraendern ());
    }

IEnumerator GroesseVeraendern (){
    // ist (siehe auch Saeulen_aus_Daten.cs) etwas komplizierter: wenn man ein Objekt streckt/skaliert, muss man es (Ankerpunkt=Mitte) auch verschieben!
    
    // 1. Größe anpassen:
    transform.localScale = Verzerrung;
    // eigentlich bei Objekt ungleich 1,1,1:
    //temporaerGroesse = new Vector3(Verzerrung.x * OriginalGroesse.x, Verzerrung.y * OriginalGroesse.y, Verzerrung.z * OriginalGroesse.z); transform.localScale = temporaerGroesse;
    
    // Position korrigieren
   if (InAlleRichtungen == false){
    korrektur = new Vector3((Verzerrung.x-1)/2.0F, (Verzerrung.y-1)/2.0F, (Verzerrung.z-1)/2.0F); 
    transform.position += korrektur;
   }

    // warten (Objekt bewegt sich ggf. weiter, aber wir haben uns den Korrekturfaktor ja in eine Variable gespeichert)
    yield return new WaitForSeconds (AnzeigeDauer);

    // Größe und Position zurücksetzen
    transform.localScale = OriginalGroesse;
       if (InAlleRichtungen == false) transform.position -= korrektur;
    }
}
