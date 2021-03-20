/* Die Grundlage ist das einfache "addForce" bei einem Körper ... "as seen on TV/internet/tutorials :-)" ... 

	(ergänzt um verzögerten Start, Auswahlmöglichkeit der "Kraft-Art", Zufallsrichtung, relativ zum Objekt oder nicht,
	einer Variante beim Zufall einen Minimalwert angibt (denn z.B. X: -5 bis 5 könnte ja auch Null sein),
	sowie eine Variante für "Hologramme" (also ohne Collider/Rigidbody), wo das Objekt fortlaufend weiterbewegt wird. (ja, das überschneidet sich mit "Bewege_Gerade" :-)
	Version 2021-02-13

	Baustellen:
		- aktuell keine Bugs bekannt/keine weiteren Optionen geplant
*/

using UnityEngine; using System.Collections; using System.Collections.Generic;

public class Schubs : MonoBehaviour{

	public enum WelcheKraftartenGibtEs { Nr1_wenig, Nr2_Impuls, Nr3_MasseEgal, Nr4_Schlagartig }; // siehe answers.unity.com Nr. 696068! (oder docs.unity3d.com/ScriptReference/ForceMode)
    public WelcheKraftartenGibtEs KraftArt = WelcheKraftartenGibtEs.Nr4_Schlagartig; 
		
	public Vector3 KraftvektorMinimal= new Vector3(-10, -10, 10); public Vector3 KraftvektorMaximal=new Vector3(10, 10, 10);
	public bool RelativZuObjekt = false;
	[Header("Zeit-Optionen:")]
	public float VerzoegerungMin=0.0F; public float VerzoegerungMax=0.0F;
	[Header("Variante:")]
	public float MinInBeliebigerRichtung = 0.0F; public float MaxInBeliebigerRichtung = 0.0F;
	[Header("Falls kein coll/rigidb.:")]
	public bool Direktantrieb = false;
	
	private float Verzoegerung; private Vector3 GewaehlteBewegungsrichtung; private ForceMode KraftArtFM; private bool DarfUpdateDirektLoslegen = false;

	void Start(){
		switch (KraftArt)
		{
			case WelcheKraftartenGibtEs.Nr1_wenig:
			KraftArtFM = ForceMode.Force; break;
			case WelcheKraftartenGibtEs.Nr2_Impuls:
			KraftArtFM = ForceMode.Impulse; break;
			case WelcheKraftartenGibtEs.Nr3_MasseEgal:
			KraftArtFM = ForceMode.Acceleration; break;
			case WelcheKraftartenGibtEs.Nr4_Schlagartig:
			KraftArtFM = ForceMode.VelocityChange; break;
			default:
			KraftArtFM = ForceMode.VelocityChange; break;
		}
		if (KraftvektorMinimal!=KraftvektorMaximal){
			GewaehlteBewegungsrichtung = new Vector3 (Random.Range(KraftvektorMinimal.x, KraftvektorMaximal.x), Random.Range(KraftvektorMinimal.y, KraftvektorMaximal.y), Random.Range(KraftvektorMinimal.z, KraftvektorMaximal.z));
			//Debug.Log("Random-Variante 1, min/max ungleich" + GewaehlteBewegungsrichtung);
		} else {
			GewaehlteBewegungsrichtung = KraftvektorMaximal;
		}
		if (MaxInBeliebigerRichtung>0.0F){
			
			GewaehlteBewegungsrichtung = Random.insideUnitSphere * Random.Range(MinInBeliebigerRichtung, MaxInBeliebigerRichtung); 
			//Debug.Log("Random-Variante 2:" + GewaehlteBewegungsrichtung + ", Länge: " + GewaehlteBewegungsrichtung.magnitude);
		}
		if (VerzoegerungMax>0 || VerzoegerungMin>0) { 
			Verzoegerung = Random.Range(VerzoegerungMin, VerzoegerungMax);
			StartCoroutine (VerzoegerterStart());
		}
		else 
		{ KraftAnwenden(); }
	}

	IEnumerator VerzoegerterStart(){
		yield return new WaitForSeconds (Verzoegerung);
		KraftAnwenden();
	}

	void KraftAnwenden(){
		if (Direktantrieb==true) { DarfUpdateDirektLoslegen=true; return; } // dadurch verzögerter Start auch bei "Direktantrieb" möglich
		if (RelativZuObjekt==true)
		{ GetComponent<Rigidbody>().AddRelativeForce(GewaehlteBewegungsrichtung, KraftArtFM); }
		else
		{ GetComponent<Rigidbody>().AddForce(GewaehlteBewegungsrichtung, KraftArtFM); }
		
		//Debug.Log(GetComponent<Rigidbody>().velocity);
	}

	void Update(){
		// hier keine Unterscheidung nach zufällige Richtung oder nicht, das wird einmalig beim Start festgelegt.
		if (Direktantrieb==true && DarfUpdateDirektLoslegen==true)
		{ 
			if (RelativZuObjekt==false)
			{ transform.Translate(GewaehlteBewegungsrichtung*Time.deltaTime, Space.World); }
			else
			{ transform.Translate(GewaehlteBewegungsrichtung*Time.deltaTime, Space.Self); }
		}
	}
}
