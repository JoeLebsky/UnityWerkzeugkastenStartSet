/* Version 2021-12-21
muss an ein Objekt mit Collider gehängt werden. Wandelt diesen in Trigger um.
Schaltet bei Berührung mit Spieler entweder einen UI-Text sichtbar (wenn verknüpft) oder ein (z.B. drehender Schriftzug) GameObject (wenn verknüpft)
Baustellen:
	- Option "UI-Text dynamisch erzeugen" noch (mit wählbarem Text) bereitstellen (schon vorbereitet s.u., Code ist aus https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Text-text.html)
	- 3D-Text wäre auch noch eine Option (aber da geht ja GameObject, oder?)
	- Player bewegungslos schalten / Level weiterschalten etc.
	- Man könnte das Deaktivieren des Zieltextes/GameObjects beim Start automatisch machen... textObject.SetActive(false);
	- Player1, 2 unterscheiden (wobei der Ansatz von damals mittels Variable "Erster" nicht schlecht ist: beide Spieler Tag 'Player', bei dem der zuerst erreicht ist der Text anders :-)
	- noch eine Skriptvariante ZielErreicht_LebenPunkte, die dort den Punktestand abfragt und erst ab Minimum eine Zielerreichung zulässt
*/

using UnityEngine; using UnityEngine.UI;

public class ZielErreicht : MonoBehaviour {
	[Header("Beim Start")]
	public bool ZielobjektUnsichtbar = false;
	public bool TextLoeschenAusblenden = true;	
	[Header("Beim Erreichen")]
	public Text zielText_hier_draufziehen;
	public GameObject oder_hierher_3Dtext;
	
	private Text text; private bool Erster = true; private string vorhandenentextmerken;

    void Start(){
        if (ZielobjektUnsichtbar==true) gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (TextLoeschenAusblenden==true) {
			if (zielText_hier_draufziehen!=null){
				vorhandenentextmerken = zielText_hier_draufziehen.GetComponent<Text>().text;
				zielText_hier_draufziehen.GetComponent<Text>().text = "";
			}
			if (oder_hierher_3Dtext!=null){
				oder_hierher_3Dtext.SetActive(false);
			}
		}
        // man könnte noch abfragen ob Collider vorhanden, aber es wird ja meist ein Standardcube (der hat Collider) genommen
        GetComponent<Collider>().isTrigger = true; // den Collider-Typ auf Trigger setzen
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")) {
			if (zielText_hier_draufziehen!=null) zielText_hier_draufziehen.GetComponent<Text>().text=vorhandenentextmerken;
			if (oder_hierher_3Dtext!=null) oder_hierher_3Dtext.SetActive(true);

			/* erstmal deaktiviert: wenn Option "dynamisch erzeugen", wird wirklich alles erzeugt:
			Font zuweisen, Canvas-GameObject mit Unterkomponenten Scaler, Raycaster, und dann endlich das textGO (Text-GameObject) mit Unterkomponente "Text" und den Einstellungen...
           Font arial; arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            GameObject canvasGO = new GameObject(); canvasGO.transform.parent = other.gameObject.transform;
            canvasGO.AddComponent<Canvas>(); canvasGO.AddComponent<CanvasScaler>(); canvasGO.AddComponent<GraphicRaycaster>();
            Canvas canvas; canvas = canvasGO.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            GameObject textGO = new GameObject(); textGO.transform.parent = canvasGO.transform; textGO.AddComponent<Text>();
            text = textGO.GetComponent<Text>(); text.font = arial; text.fontSize = 48; text.alignment = TextAnchor.MiddleCenter;
            if (Erster==true) { text.text = "Gewonnen!"; } else { text.text = "Schade..."; }
            RectTransform rectTransform; rectTransform = text.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0); rectTransform.sizeDelta = new Vector2(600, 200);
			*/
        }
        Erster = false;
    }
}
