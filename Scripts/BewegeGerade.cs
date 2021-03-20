// Version 2021-02-06
// weitere Veränderungen:
// erst mal keine geplant, Skript ist einfach aber macht was es soll :-)
using UnityEngine; 

public class BewegeGerade : MonoBehaviour{
    [Header("Entweder Objekt wird direkt bewegt...")]
    public Vector3 Geschwindigkeit = new Vector3(1.5F, 0.0F, 0.0F);
    public bool RelativZurEigenenDrehung = true;
    
    [Header("... oder rigidbody-Geschwindigkeit")] // setzt natürlich ein add component, Rigidbody voraus
    public Vector3 KoerperGeschwindigkeit = Vector3.zero; 

    [Header("Noch ein paar Sonder-Optionen:")]
    public float DreheUmNach=0.0F; public float StoppeBewegungNach=0.0F; public float BeendeProgrammNach=0.0F;

    private float zeitpunkt;
    void Start(){
        zeitpunkt = Time.time; // Startzeitpunkt brauchen wir für die diversen "nach X Sekunden das und das machen"
    }

    // Update is called once per frame
    void Update(){
        if (StoppeBewegungNach!=0 && (Time.time-zeitpunkt>StoppeBewegungNach)) { Geschwindigkeit=Vector3.zero; }
        if (BeendeProgrammNach!=0 && (Time.time-zeitpunkt>BeendeProgrammNach)) { Application.Quit(); }
        if (DreheUmNach!=0 && (Time.time-zeitpunkt>DreheUmNach)) { zeitpunkt=Time.time; Geschwindigkeit= (-1) * Geschwindigkeit; }
        // Bewege das Objekt hier mittels "Translate"-Methode. 
        // Vorsicht: umgeht die Physics-Engine ... das ist wie fortlaufendes Teleportieren, kann die Performance runterziehen weil Unity dauernd Abstände neu berechnen muss
        if (Geschwindigkeit != Vector3.zero && KoerperGeschwindigkeit== Vector3.zero){ 
            if (RelativZurEigenenDrehung==true)
            { transform.Translate(Geschwindigkeit*Time.deltaTime, Space.Self); }
            else
            { transform.Translate(Geschwindigkeit*Time.deltaTime, Space.World); }
        } 
    }
    
    void FixedUpdate(){ 
        // FixedUpdate soll man statt Update für Physics-Operationen wie ridigbody verwenden, siehe Unity-Handbuch
        if (Geschwindigkeit == Vector3.zero && KoerperGeschwindigkeit!= Vector3.zero){ 
            GetComponent<Rigidbody>().velocity = KoerperGeschwindigkeit;
        }
        // Kraft per addforce auszuüben würde fortlaufend beschleunigen: GetComponent<Rigidbody>().AddForce(KoerperGeschwindigkeit);  
    }
}
