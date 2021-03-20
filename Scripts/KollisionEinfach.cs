// Version 2020-11-25

// nächste Schritte: 
// das  Feature "Animation spielen falls vorhanden" und "Sound spielen falls vorhanden"
// und bei "festgelegtes Objekt" auch Zerstören/Deaktivieren/Unsichtbar anbieten, ggf. weitere (Collider entfernen, etc.)
// und ein bisschen "Refaktoring", den Code nicht doppelt für Coll/Trigger, sondern Aufruf Unterroutine mit Übergabe Koll-Objekt
using UnityEngine; 

public class KollisionEinfach : MonoBehaviour{
    [Header("Dieses Objekt selbst:")]
    public bool ZerstoereDieses=false; // also dieses Objekt hier selbst, an dem das Skript hängt (z.B. eine Rakete o.ä.)
    public bool UnsichtbarDieses = false;
    public bool DeaktiviereDieses = false;
    [Header("Aufprallendes Objekt:")]
    public bool ZerstoereAufprallobjekt = true;
    public bool UnsichtbarAufprObj=false;
    public bool DeaktAufprObj=false;
public string ObjekteMitDiesemTagBeeinflussen; // da kann man im Inspector einstellen welches Objekt
    [Header("Oder festgelegtes Objekt:")]
    public GameObject DiesesObjektErzeugen;

    private void OnCollisionEnter(Collision collision){
        // hmmm: wann ist tag besser, wann CompareTag?
        //Debug.Log("Coll.");
	    // falls kein Tag angegeben ist: alle Objekte. Oder (das sind die senkrechten Striche) falls eins angegeben ist: muss es übereinstimmen
        if (ObjekteMitDiesemTagBeeinflussen=="" || collision.gameObject.tag == ObjekteMitDiesemTagBeeinflussen){
            if(ZerstoereAufprallobjekt==true){ Destroy(collision.gameObject); }
            if(UnsichtbarAufprObj==true) { collision.gameObject.GetComponent<MeshRenderer>().enabled = false;}
            if(DeaktAufprObj==true) { collision.gameObject.SetActive(false);}
        }
        if (ZerstoereDieses) { Destroy(gameObject); }
        if (UnsichtbarDieses==true) { gameObject.GetComponent<MeshRenderer>().enabled = false; }
		if (DeaktiviereDieses==true) { gameObject.SetActive(false); }

        // falls ein anderes Objekt erzeugt werden soll (z.B. Chemie-Tutorial): Instanz erzeugen
        if (DiesesObjektErzeugen!=null) { Instantiate(DiesesObjektErzeugen, transform.position, transform.rotation); }
    }

    // und als Variante wenn der Collider vom Typ "Trigger" ist - siehe Anleitung (dann fährt man durch und prallt nicht ab)
	private void OnTriggerEnter(Collider other){
        //Debug.Log("coll.trigger");
        if (ObjekteMitDiesemTagBeeinflussen=="" || other.gameObject.CompareTag(ObjekteMitDiesemTagBeeinflussen)){
            if(ZerstoereAufprallobjekt==true){ Destroy(other.gameObject); }
            if(UnsichtbarAufprObj==true) { other.gameObject.GetComponent<MeshRenderer>().enabled = false;}
            if(DeaktAufprObj==true) { other.gameObject.SetActive(false);}
        }
        if (ZerstoereDieses) { Destroy(gameObject); }
        if (UnsichtbarDieses==true) { gameObject.GetComponent<MeshRenderer>().enabled = false; }
		if (DeaktiviereDieses==true) { gameObject.SetActive(false); }

        // falls ein anderes Objekt erzeugt werden soll (z.B. Chemie-Tutorial): Instanz erzeugen
        if (DiesesObjektErzeugen!=null) { Instantiate(DiesesObjektErzeugen, transform.position, transform.rotation); }
    }
}