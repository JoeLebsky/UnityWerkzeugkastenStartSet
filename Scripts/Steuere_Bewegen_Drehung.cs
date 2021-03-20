// Version 2021-02-21
// alle Steueroptionen (auch für 2 Spieler) in einem Script

/* Baustellen: 
 - alles nur mit Positionsänderung (Translate) bisher, nichts mit addforce (was ja eigentlich bei rigidbodys verwendet werden sollte)
 - im Unity-Kurs wird zwar der Android-Export (und ja auch Joysticksteuerung am Computer möglich) erwähnt, 
        aber hier bisher keine Nutzung von Input.GetAxis, .Touch oder .Acceleration statt Computer-Tasten-Abfrage ... siehe https://docs.unity3d.com/ScriptReference/Input.html
*/
using UnityEngine; 


public class Steuere_Bewegen_Drehung : MonoBehaviour{

	public float GesamtSpeed=3.0F;  public float Drehgeschwindigkeit=60.0F;
	public bool nachLoslassenWeiterfahren; public bool nachLoslassenWeiterdrehen;
	public bool PfeilRechtsLinksDreht = true;
	public bool PfeilRechtsLinksBewegt = false;
	public bool TastenADDrehen = false;
	public bool TastenADBewegen = true;
	public bool RaufRunterIstVorZurueck = true;
	public bool RaufRunterIstRaufRunter = false;
	public bool TastenWSBewegenVorZurueck = false;
	public bool TastenWSBewegenRaufRunter = true;


	private float xbeweg=0.0F; private float yvorzurueck=0.0F; private float yraufrunter = 0.0F;
	public float Absturzgrenzwert=0.0F; private Vector3 Startposition; private float xdreh=0.0F; 


    private void Start(){
        Startposition = transform.position;
        if (GesamtSpeed == 0) { GesamtSpeed = 5; }
        if (Drehgeschwindigkeit == 0) { Drehgeschwindigkeit = 50.0F; }
    }

    void Update(){
        // es wäre auch möglich mit KollidiereSpezial und "setze Restartposition" und hier dann springe zur letzten Restartpos.
        if (Absturzgrenzwert!=0 && transform.position.y<Absturzgrenzwert) { transform.position = Startposition; }
	if (PfeilRechtsLinksDreht == true){
		if (Input.GetKey(KeyCode.LeftArrow)) { xdreh = -1.0F; }
		if (Input.GetKey(KeyCode.RightArrow)) { xdreh = 1.0F; }
	}
	if (PfeilRechtsLinksBewegt == true){
		if (Input.GetKey(KeyCode.LeftArrow)) { xbeweg = -1.0F; }
		if (Input.GetKey(KeyCode.RightArrow)) { xbeweg = 1.0F; }
	}
	if (TastenADDrehen == true){
		if (Input.GetKey(KeyCode.A)) { xdreh = -1.0F; }
		if (Input.GetKey(KeyCode.D)) { xdreh = 1.0F; }
	}
	if (TastenADBewegen==true){
		if (Input.GetKey(KeyCode.A)) { xbeweg = -1.0F; }
		if (Input.GetKey(KeyCode.D)) { xbeweg = 1.0F; }
	}

	if (RaufRunterIstVorZurueck==true){
		if (Input.GetKey(KeyCode.UpArrow)) { yvorzurueck = 1.0F; }
		if (Input.GetKey(KeyCode.DownArrow)) { yvorzurueck = -1.0F; }
	}

	if (RaufRunterIstRaufRunter == true){
		if (Input.GetKey(KeyCode.UpArrow)) { yraufrunter = 1.0F; }
		if (Input.GetKey(KeyCode.DownArrow)) { yraufrunter = -1.0F; }
	}
	if (TastenWSBewegenRaufRunter  == true){
		if (Input.GetKey(KeyCode.W)) { yraufrunter = 1.0F; }
		if (Input.GetKey(KeyCode.S)) { yraufrunter = -1.0F; }
	}
	if (TastenWSBewegenVorZurueck==true){
		if (Input.GetKey(KeyCode.W)) { yvorzurueck = 1.0F; }
		if (Input.GetKey(KeyCode.S)) { yvorzurueck = -1.0F; }
	}

        // vorwärts bewegen
        transform.Translate(xbeweg * GesamtSpeed * Time.deltaTime, yraufrunter * GesamtSpeed * Time.deltaTime, yvorzurueck * GesamtSpeed * Time.deltaTime);
        //rb.AddForce(new Vector3(0, 0, GesamtSpeed * 50.0F * Time.deltaTime));   

        // und drehen
        transform.Rotate(0, xdreh * Drehgeschwindigkeit * Time.deltaTime, 0);

        // beibehalten oder nicht?
        if (nachLoslassenWeiterfahren == false) { yvorzurueck = 0.0F; yraufrunter  = 0.0F; xbeweg = 0.0F; } 
        if (nachLoslassenWeiterdrehen == false) { xdreh = 0.0F; }
    }
}
