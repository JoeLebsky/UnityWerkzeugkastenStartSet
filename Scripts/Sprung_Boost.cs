// Version 2020-11-23b

/* Baustellen:
  -  ggf. noch "erst nach Kontakt mit Boden/Objekt mit Tag'Boden' o.ä. wieder Sprung erlaubt"
  - weitere Sondertasten... das unten ist nicht übermäßig elegant gelöst :-)
  - und die Partikelanimation ggf. separater Sekundenwert... aktuell: so lange wie nicht neu gesprungen werden darf... halbwegs ok :-)
*/

using UnityEngine; 

public class Sprung_Boost : MonoBehaviour{
    
    public Vector3 Kraftrichtung=new Vector3(0, 500, 200);
    public bool AktivierePartikelsystem = true;
    public bool TabTaste = false; public bool LeerTaste = false; public bool ReturnTaste = false; 
    public string Buchstabentaste = "x"; 
    public float SekundenVorNeusprung = 2.0F;
    private float Zeitstempel, VergangeneZeit; private ParticleSystem ps;
    void Start(){
        if (AktivierePartikelsystem==true) { ps = GetComponent<ParticleSystem>(); } // play on awake muss aus sein. das ps.enableEmission etc. klappte nicht so gut.
        VergangeneZeit=SekundenVorNeusprung; // damit man sofort losspringen kann
        
    }
    void Update(){
        
        VergangeneZeit += Time.deltaTime; // Debug.Log(VergangeneZeit.ToString()); 
        if (SekundenVorNeusprung!=0 && VergangeneZeit < SekundenVorNeusprung) { return; }
        // wenn Taste gedrückt, Sprung auslösen und den Zeitzähler zurücksetzen und die Routine verlassen
        if (AktivierePartikelsystem==true) { ps.Stop(); }
        if (Input.GetKeyDown(KeyCode.Space)&& LeerTaste==true){ 
            GetComponent<Rigidbody>().AddRelativeForce(Kraftrichtung); // AddForce wäre immer die gleiche Richtung egal wie der Spieler gedreht ist :-)
            VergangeneZeit=0.0F; 
            if (AktivierePartikelsystem==true) { ps.Play(); }
            return; 
            }
        if (Input.GetKeyDown(KeyCode.Tab)&& TabTaste==true){ 
            GetComponent<Rigidbody>().AddRelativeForce(Kraftrichtung); 
            VergangeneZeit=0.0F; 
            if (AktivierePartikelsystem==true) { ps.Play(); }
            return; }
        if (Input.GetKeyDown(KeyCode.Return)&& ReturnTaste==true){ 
            GetComponent<Rigidbody>().AddRelativeForce(Kraftrichtung); 
            VergangeneZeit=0.0F; 
            if (AktivierePartikelsystem==true) { ps.Play(); }
            return; }
        if (Buchstabentaste!="") { 
            if (Input.GetKeyDown(Buchstabentaste)){ 
                GetComponent<Rigidbody>().AddRelativeForce(Kraftrichtung); 
                VergangeneZeit=0.0F; 
                if (AktivierePartikelsystem==true) { ps.Play(); }
            
            } 
        }
        
    }
}
