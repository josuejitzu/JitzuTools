using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JitzuTools
{
    public class CrearFolders : EditorWindow
    {
        List<string> rutas = new List<string>() { "3rd-Party", "Animaciones", "Audio", "Materiales", "Modelos", "Plugins", "Prefabs", "Resources", "Texturas", "Scenes", "Scripts", "Shaders", "UIX"};

        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;





        // Add menu item named "My Window" to the Window menu
        [MenuItem("JitzuTools/Crear Estructura")]
        public static void ShowWindow()
        {
           
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(CrearFolders));
        }

        void OnGUI()
        {
            GUILayout.Label("Estructura Folders", EditorStyles.boldLabel);
            GUILayout.Space(10);
            // myString = EditorGUILayout.TextField("Text Field", myString);

            List<string> rutasEditable = rutas;
            for (int i = 0; i < rutas.Count; i++)
            {
                rutasEditable[i] = EditorGUILayout.TextField("Ruta" + i, rutasEditable[i]);
            }


            //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            //myBool = EditorGUILayout.Toggle("Toggle", myBool);
            //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            //EditorGUILayout.EndToggleGroup();

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                rutas.Add("");
            }
            if(GUILayout.Button("-"))
            {
                rutas.RemoveAt(rutas.Count-1);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Crear"))
            {
                if (rutasEditable.Count > 0)
                    CrearEstructura(rutasEditable);
            }
        }
         void CrearEstructura(List<string> rutasFinales)
        {
            for (int i = 0; i < rutasFinales.Count; i++)
            {
                if(string.IsNullOrEmpty( rutasFinales[i]) )
                {
                    Debug.Log($"No se especifico nombre en Ruta{i}, ignorando...");
                }else if (AssetDatabase.IsValidFolder("Assets/" + rutasFinales[i]))
                {
                    Debug.Log($"La carpeta {rutasFinales[i]} ya existe, saltando...");
                }
                else 
                {
                    string guid = AssetDatabase.CreateFolder("Assets", rutas[i]);
                    //string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
                }
            }
            rutas = rutasFinales;

        }
    }
}
