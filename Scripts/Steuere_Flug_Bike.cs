// Version 2020-06-13
using UnityEngine; // using System.Collections; using System.Collections.Generic;


public class Steuere_Flug_Bike : MonoBehaviour{
     
    public bool stuerztBeiNullAb = false; public bool kannRueckwarts = false; // das hier war gedacht -90 bis 90 für Motorrad: public bool BewegungEinschraenken = false;
public float Drehgeschwindigkeit=50.0F; public float Steiggeschwindigkeit = 25.0F; public float BeschleunigungMitTasteBUndV=2.0F; public float Startgeschwindigkeit=5.0F;
   // die internen Variablen
   private float rollen; private float aufab; private float Fluggeschwindigkeit;

void Start(){
    Fluggeschwindigkeit = Startgeschwindigkeit;
}
    void Update(){
        if (Input.GetKey(KeyCode.LeftArrow)) { rollen = 1.0F; }
        if (Input.GetKey(KeyCode.RightArrow)) { rollen = -1.0F; }
        if (Input.GetKey(KeyCode.UpArrow)) { aufab = -1.0F; }
        if (Input.GetKey(KeyCode.DownArrow)) { aufab = 1.0F; }
        if (Input.GetKey("b")) { Fluggeschwindigkeit+=BeschleunigungMitTasteBUndV; }
        if (Input.GetKey("v")) { Fluggeschwindigkeit-=BeschleunigungMitTasteBUndV; }
        if (kannRueckwarts==false && Fluggeschwindigkeit<0) { Fluggeschwindigkeit = 0; }

        // bewegen (besser wäre Rigidbody.AddForce)
        transform.Translate(0.0F, 0.0F, Fluggeschwindigkeit * Time.deltaTime);

        // drehen
        transform.Rotate(aufab * Steiggeschwindigkeit * Time.deltaTime, 0.0F, rollen * Drehgeschwindigkeit * Time.deltaTime);

        // kein Weiterrollen, aber Weitersteigen? hm.
        rollen=0.0F; aufab=0.0F;
        // if (stuerztBeiNullAb==true && Fluggeschwindigkeit == 0) { macheirgendwas } ist noch nicht implementiert :-)
    }
}
