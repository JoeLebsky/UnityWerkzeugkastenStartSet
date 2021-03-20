/* Version 2021-03-06
hängt man an ein Objekt in einer Szene (Szenen-Name sollte mit "Start" oder "_Start" beginnen)
Erzeugt Schaltflächen/Buttons mit denen man einzelne Szenen ansteuern kann.
Die Beschriftung gibt der User rechts im Inspektor vor, oder es wird durchnummiert ("Namen der Szenen verwenden" fehlt noch als Option)


Baustellen: oje. Jede Menge.
    - simpel die Anzahl (und Namen) der Szenen ermitteln, irgendwie List<EditorBuildSettingsScene> Scenes = EditorBuildSettings.scenes;
    - sicher ist auch eine Variante möglich dass man nicht alle Szenen in Build einfügen muss sondern es dynamisch geht, mit EditorSceneManager.LoadSceneAsyncInPlayMode oder so 
    - Startszene selbst muss natürlich ausgenommen werden
    - Design-Möglichkeiten fehlen (Button-Größe, -Anordnung, -Schriftart, ...)
    - ggf. noch ein Feature: wenn unter "Texturen" welche mit "Levelbild1" etc sind, diese automatisch dem Button zuweisen (oder ein Quad dahinter)
    - später auch was cooles mit drehendem Rad aus lauter Screenshots/Buttons
 */

using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEditor; using System.IO;

public class Start_Level_Szenen_Auflisten : MonoBehaviour{

    // Hier kann der User Namen festlegen (rechts im Inspector)
    public string[] SzenenNamen;
    private string ButtonText;
 
    void OnGUI(){
        try{
            // eigentlich Start bei 0, aber die Startszene (sollte natürlich an erster Stelle stehen) ausnehmen, daher Start erst bei 1
            for (int i = 1; i < 10; i++){
                // Beschriftung: falls der User etwas festgelegt hat, das verwenden
                ButtonText = "Szene/Level" + i;
                if (SzenenNamen.Length>i) { ButtonText = SzenenNamen[i]; } 
                
                
                if (GUI.Button(new Rect(10, i*30+10, 250, 30), ButtonText))
                    try { SceneManager.LoadScene(i, LoadSceneMode.Single); } catch {}
            }
        }         catch { }
   }        
}
