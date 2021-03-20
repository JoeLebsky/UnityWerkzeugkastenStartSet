/* Version 2021-03-18
  Beschreibung: wird an ein Objekt gehängt und bietet ein paar Standard-Funktionen, falls der Spieler aufs Objekt klickt oder mit der Maus drüberfährt

  Baustellen: 
  - Mausklick noch Farbwechsel (und nicht nur blau :-), Größenskalierung, Objekterzeugung
  - selbiges natürlich für MouseOver (und -Out) 
*/

using UnityEngine; 

public class Mausklick_MouseOver : MonoBehaviour {
	// über diese Variablen (die der User im Inspector sieht) kann man festlegen, was bei Klick geschehen soll
	[Header("Bei Mausklick:")]
	public bool Loeschen = false; public bool Deaktivieren = false; public bool Unsichtbar = false;
	public bool SpieleAngehaengtenSound = false; public Vector3 Drehung;

	[Header("Bei MouseOver (berühren):")]
	public bool Farbwechsel = false; 
	private Material MaterialDesObjekts; private Color Ursprungsfarbe;
	void Start(){ MaterialDesObjekts = GetComponent<Renderer>().material; Ursprungsfarbe = GetComponent<Renderer>().material.color; }
    private void OnMouseDown(){
		if (Drehung != Vector3.zero) { gameObject.transform.Rotate(Drehung); }
		if (SpieleAngehaengtenSound==true) { gameObject.GetComponent<AudioSource>().Play(); }
		if (Unsichtbar==true) { gameObject.GetComponent<MeshRenderer>().enabled = false; }
		if (Deaktivieren==true) { gameObject.SetActive(false); }
        if (Loeschen==true) { Destroy(gameObject); }
    }

	void OnMouseOver(){
        if (Farbwechsel==true) { MaterialDesObjekts.color = Color.blue; }
    }

    void OnMouseExit(){
        if (Farbwechsel==true) { MaterialDesObjekts.color = Ursprungsfarbe; }
    }
}
