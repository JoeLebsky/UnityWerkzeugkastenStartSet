// Version 2020-11-09
/* künftige mögliche Verbesserungen (aber eher unwichtig):
	- ggf. Abwurfpunkt automatisch abhängig von der Größe des geworfenen Prefabs.
	- falls kein Asset zugewiesen: irgendeins https://docs.unity3d.com/ScriptReference/AssetDatabase.FindAssets.html :-)
*/

using UnityEngine; using System.Collections; using System.Collections.Generic;


public class Schuss_Wurf_Abwurf : MonoBehaviour
{

	// entweder ...Leertaste true und ...alleXSek 0 => Spieler wirft/schießt
	//   oder  ...Leertaste false und Sekundenwert  => nicht spielergesteuertes Objekt schießt/wirft alle x Sekunden
	public GameObject welchesObjekt; // muss man draufziehen (Rakete, Teddy, ...)
	public bool aufLeertasteReagieren = true;	    
	public int wiederholtAutomatischAbwerfenAlleXSekunden = 0; 
    public Vector3 ObjektVersetztErzeugen = new Vector3(0.0F, -2.0F, 0.0F); public Vector3 ObjektSchwungMitgeben = Vector3.zero;
    public bool SpieleAngehaengteSounddatei = true; // die Alternative: man setzt die Sounddatei am Objekt auf "play on awake", also "spiele beim (Objekt-)Erzeugen
[Header("Einstellungen zur Drehung:")]
    public bool ZufaelligeDrehung = false; public bool ZufaelligeDrehungNurYAchse = false; public bool DrehungVonElternObjekt = false; public Vector3 RotationDesObjekts;
	private GameObject abwurfPunkt;    


    void Start () {
        if (wiederholtAutomatischAbwerfenAlleXSekunden != 0){ StartCoroutine(AbwurfAutomatischWiederholt()); }
    }
	
    void Update () {
        // falls Tastenreaktion eingeschaltet und Leertaste gedrückt wurde:
        if (aufLeertasteReagieren==true && Input.GetKeyDown(KeyCode.Space)){ BewegtenGegenstandErzeugen(); }
    }

    IEnumerator AbwurfAutomatischWiederholt(){
        while (true){
            BewegtenGegenstandErzeugen();
            yield return new WaitForSeconds(wiederholtAutomatischAbwerfenAlleXSekunden); // kein Abbruchkriterium, Endlosschleife
        }
    }

    void BewegtenGegenstandErzeugen(){
        Vector3 abwurfPosition = transform.position; abwurfPosition += ObjektVersetztErzeugen;
        // Drehung festlegen (je nach gewähltem Häkchen)
        Quaternion spawnRotation; spawnRotation = Quaternion.identity; // Standardwert, "Nulldrehung"
        if (ZufaelligeDrehung==true) { spawnRotation = Random.rotation; }
        if (DrehungVonElternObjekt==true) { spawnRotation = transform.rotation; } // das Objekt an dem dieses Skript hängt
        if (ZufaelligeDrehungNurYAchse==true) { 
            Vector3 rotationVector = new Vector3(0, Random.Range(-90.0F, 90.0F), 0); spawnRotation = Quaternion.Euler(rotationVector); }
        if (RotationDesObjekts!=Vector3.zero) { spawnRotation = Quaternion.Euler(RotationDesObjekts); }
        // Objekt erzeugen
        GameObject flug = Instantiate(welchesObjekt, abwurfPosition, spawnRotation); 
        // falls "SchwungMitgeben" ungleich Null UND WICHTIG: Rigidbody angehängt, dann bewegen
        if (ObjektSchwungMitgeben!=Vector3.zero && flug.GetComponent<Rigidbody>() != null){ flug.GetComponent<Rigidbody>().AddRelativeForce(ObjektSchwungMitgeben, ForceMode.VelocityChange); }
        AudioSource sndObjekt = flug.GetComponent<AudioSource>();
        if (sndObjekt != null && SpieleAngehaengteSounddatei==true) { sndObjekt.Play(); }
    }
}
