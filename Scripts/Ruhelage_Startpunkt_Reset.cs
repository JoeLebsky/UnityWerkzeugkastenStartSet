/* versucht alle Bewegungskräfte eines Objekts (welches einen rigidbody hat) auf Tastendruck zurückzusetzen
Version 2021-03-31

Baustellen: 
 - ggf. weitere Angaben erlauben (Rotation, welche Taste)... 
 - und manchmal muss man für einen Sekundenbruchteil auf "Objekt ist kinematisch" gehen? (s. Unity-Forumsbeitrag 39998)
 - und Check "Object has rigidbody"
 */

using UnityEngine;

public class Ruhelage_Startpunkt_Reset : MonoBehaviour {
public Vector3 NeustartPosition = Vector3.zero; // kann der User im Inspector angeben
private Rigidbody rb;
void Start(){
    rb = GetComponent<Rigidbody>();
}
    
    void Update(){
        if (Input.GetKeyDown("p")){
            // Objekt "beruhigen"
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.inertiaTensorRotation = Quaternion.identity;
            // das hier auf Null setzen gibt Fehler: rb.inertiaTensor = Vector3.zero;
            // und wieder platzieren wie vom User gewünscht
            rb.transform.position = NeustartPosition;
            
        }
    }
}
