/* Version 2020-06-06
wichtig: in den meisten Fällen wird man das Skript auf die Kamera am Spieler ziehen wollen, nicht den Spieler selbst (dort dann Bewege-Skript)
ggf. weitere Features später:
    - Limitierung (public float MaxDrehung=45.0F; if transform.rotation der Kamera mehr als diesen Wert vom Parent-Objekt abweicht...)
    - weitere public-Variablen um mehr Varianten beim Bewegen zu haben
*/
using UnityEngine; 

public class Mausbewegung : MonoBehaviour{

    public float DrehgeschwindigkeitRechtsLinks = 200.0F; public float DrehgeschwindigkeitRaufRunter = -200.0F;
    public Vector3 Bewegungsgeschwindigkeit = Vector3.zero; 

    
    void Update(){
        // sobald eine Bewegung erfolgt: es in Drehung umsetzen (kann man auch auf 0 setzen im Inspector) und/oder Bewegen
        if (Input.GetAxis("Mouse X")!=0 || Input.GetAxis("Mouse Y")!=0){ 
            Vector3 Drehung = new Vector3(Input.GetAxisRaw("Mouse Y")*DrehgeschwindigkeitRaufRunter, Input.GetAxisRaw("Mouse X")*DrehgeschwindigkeitRechtsLinks, 0.0F);
            transform.Rotate(Drehung*Time.deltaTime, Space.Self); 
            float BewegungX= Input.GetAxisRaw("Mouse X")*Bewegungsgeschwindigkeit.x*Time.deltaTime;
            float BewegungZ = Input.GetAxisRaw("Mouse Y")*Bewegungsgeschwindigkeit.z*Time.deltaTime; 
            transform.Translate(new Vector3(BewegungX, 0.0F, BewegungZ));
        }
    }
}
