/* versucht alle Bewegungskräfte eines Objekts (welches einen rigidbody hat) auf Tastendruck zurückzusetzen
Version 2021-04-01

Baustellen: 
 - ggf. weitere Angaben erlauben (welche Rotation, welche Taste)... 
 - und manchmal muss man für einen Sekundenbruchteil auf "Objekt ist kinematisch" gehen? (s. Unity-Forumsbeitrag 39998)
 - und Check "Object has rigidbody"
 */

using UnityEngine;

public class Ruhelage_Startpunkt_Reset : MonoBehaviour {
public Vector3 NeustartPosition = Vector3.zero; // kann der User im Inspector angeben, alternativ den Startwert;
[Header("oder:")]
public bool StattdessenStartposition = true;
private Rigidbody rb; private Quaternion Startdrehung; private Vector3 Startposition;
void Start(){
    rb = GetComponent<Rigidbody>();
    Startdrehung = transform.rotation; Startposition = transform.position;
}
    
    void Update(){
        if (Input.GetKeyDown("p")){
            // Objekt "beruhigen"
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.inertiaTensorRotation = Quaternion.identity;
            // das hier auf Null setzen gibt Fehler: rb.inertiaTensor = Vector3.zero;
            // und wieder platzieren wie vom User gewünscht
            transform.rotation = Startdrehung;
            if (StattdessenStartposition==true){
                transform.position = Startposition;
            } else
            { transform.position = NeustartPosition; }
            
        }
    }
}
