using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;


namespace JitzuTools
{
    [EditorTool("PhysicsDrop Tool", typeof(Rigidbody))]
    public class PhysicsTool : EditorTool
    {
        GUIContent cachedIcon;

        // NOTE: as were caching this, unity will serialize it between compiles! so if we want to test out new looks,
        // just return the new GUIContent and bypass the cache until were happy with the icon...
        public override GUIContent toolbarIcon
        {
            get
            {
                if (cachedIcon == null)
                    cachedIcon = EditorGUIUtility.IconContent("PhysicMaterial Icon", "|PhysicsDrop Tool");
                return cachedIcon;
            }
        }

        private List<RigidbodyCache> bodyCache = new List<RigidbodyCache>();
        public override void OnToolGUI(EditorWindow window)
        {

            // make sure we have objects to work with...
            if (Selection.activeTransform == null)
                return;

            // lets convert the selected objects postion into a screen position...
            Vector3 pos = Selection.activeTransform.position;
            Vector3 screenPoint = SceneView.lastActiveSceneView.camera.WorldToScreenPoint(pos);

            // ... if its visible, lets draw some GUI over it
            if (screenPoint.z > 0)
            {
                // Make sure we tell Unity were drawing some UI
                Handles.BeginGUI();

                // lets call into PixelsToPoints, this takes editor GUI scale/high DPI screens into account to make sure the 
                // UI aligns correctly
                screenPoint = EditorGUIUtility.PixelsToPoints(SceneView.lastActiveSceneView.camera.WorldToScreenPoint(pos));

                Vector2 buttonSize = new Vector2(100, 20);
                float screenHeight = SceneView.currentDrawingSceneView.position.size.y;
                Vector2 buttonPos = new Vector2(screenPoint.x, screenHeight - (screenPoint.y));

                // Actually draw the button
                if (GUI.Button(new Rect(buttonPos, buttonSize), "Simulate"))
                {
                    // first, lets grab all the rigidbodies were working on...
                    var selectedBodies = new List<Rigidbody>();
                    foreach (var gameObject in Selection.gameObjects)
                    {
                        var body = gameObject.GetComponent<Rigidbody>();
                        if (body != null)
                        {
                            selectedBodies.Add(body);
                        }
                    }

                    // then lets grab all the rigidbodies in the scene and cache their positions
                    var bodies = FindObjectsOfType<Rigidbody>();
                    bodyCache.Clear();
                    foreach (var body in bodies)
                    {
                        bodyCache.Add(new RigidbodyCache(body));
                    }

                    // Tell the undo system were about to modify our selected objects.
                    Undo.RecordObjects(Selection.transforms, "Physics Simulation...");

                    // Annnd finally lets tick the physics engine!!
                    Physics.autoSimulation = false;
                    for (int i = 0; i < 600; i++)
                    {
                        Physics.Simulate(Time.fixedDeltaTime);
                    }
                    Physics.autoSimulation = true;

                    // Since Physics.Simulate effected everything in the scene, lets loop over the cached copies
                    // of all the rigid bodies and reset the positions and rotations of everything that wasnt selected!
                    // We should also make sure to clear accumulated velocities, so they dont carry over
                    foreach (var body in bodyCache)
                    {
                        if (!selectedBodies.Contains(body.Rigidbody))
                        {
                            var transform = body.Rigidbody.transform;
                            transform.position = body.Position;
                            transform.rotation = body.Rotation;
                        }

                        body.Rigidbody.velocity = Vector3.zero;
                        body.Rigidbody.angularVelocity = Vector3.zero;
                    }
                }

                // Finally lets tell Untiy we finished our UI
                Handles.EndGUI();
            }
        }
    }

    public struct RigidbodyCache
    {
        public Rigidbody Rigidbody;
        public Vector3 Position;
        public Quaternion Rotation;

        public RigidbodyCache(Rigidbody rigidbody)
        {
            this.Rigidbody = rigidbody;
            this.Position = rigidbody.position;
            this.Rotation = rigidbody.rotation;
        }
    }
}