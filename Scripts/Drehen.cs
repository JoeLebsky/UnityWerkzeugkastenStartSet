/*  dreht ein Objekt (Tipp: unsichtbares/leeres Objekt, an das ein anderes z.B. Kamera gehängt wird!)
	Version 2021-03-04
	Baustellen: 
	 - ggf.: -100/+100 ist ja "von 0 bis 100 in beide Richtungen". irgendwie die Möglichkeit z.B. "50-100, aber beide Richtungen"?
*/

using UnityEngine; using System.Collections;

public class Drehen : MonoBehaviour{

	// Variablen (diese erscheinen rechts im Inspektor)
	public Vector3 RotationMinimal= new Vector3(0.0F, 30.0F, 0.0F);
	public Vector3 RotationMaximal= new Vector3 (0.0F, 30.0F, 0.0F);
	[Header("(sonst Welt-Achse)")]
	public bool AchseVomObjekt = true;
	 [Header("wenn kein Collider/Rigidbody:")]
	public bool DirektAntrieb = true;
	private Vector3 tatsaechlicheDrehung; private Space WieDennDrehen;
	void Start(){
		// je nachdem wie User das Häkchen gesetzt hat, wird der Drehparameter "um mich selbst" oder "um eine feste Achse" gesetzt, dann dreht sich ein schrägstehendes Objekt anders
		if (AchseVomObjekt==true) { WieDennDrehen = Space.Self; } else { WieDennDrehen = Space.World; }
		// wenn Zufall: Zufallswert ermitteln
		if (RotationMaximal != RotationMinimal) {
			tatsaechlicheDrehung = new Vector3(Random.Range(RotationMinimal.x, RotationMaximal.x), Random.Range(RotationMinimal.y, RotationMaximal.y), Random.Range(RotationMinimal.z, RotationMaximal.z));
		} 
		else
		{
			tatsaechlicheDrehung = RotationMaximal;	
		}	

		// wenn nicht Direktantrieb (das geht dann über die Update-Routine): Anfangsschubs per "AddTorque" mitgeben und das wars.
		// und wenn der Haken AchseVonObjekt gesetzt ist, dann relativ zum (vielleicht schräg stehenden) Objekt schubsen
		if (DirektAntrieb==false) {	
			if (AchseVomObjekt==true)
			{ GetComponent<Rigidbody>().AddRelativeTorque(tatsaechlicheDrehung); }
			else
			{ GetComponent<Rigidbody>().AddTorque(tatsaechlicheDrehung); }
		}
			
		// weiß nicht ob das hier ggf. sinnvoll wäre? GetComponent<Rigidbody>().AddForce(Random.insideUnitCircle.normalized*Random.Range(0, Zufallsdrehung
	}

    void Update(){
        // jedes Frame/alle x Millisekunden: Objekt (an dem dieses Skript hängt) ein Stückchen weiterdrehen
		// (wenn Haken Direktantrieb gesetzt, wenn also KEIN rigidbody dem in der Startroutine ein Drehimpuls (Torque) mitgegeben wurde)
        if (DirektAntrieb==true){
            transform.Rotate(tatsaechlicheDrehung * Time.deltaTime, WieDennDrehen); // Time.deltaTime bewirkt, dass auf schnellerem PC (ruft Update öfter auf) in kleineren Häppchen gedreht wird.
        } 
    }
}


