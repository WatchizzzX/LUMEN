using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Capture360Editor : UnityEditor.Editor
    {
        [MenuItem("Tools/Capture 360 image of Main Camera")]
        public static void Capture()
        {
            var bytes = I360Render.Capture( 8192, true );
            if (bytes == null) return;
                
            var path = Path.Combine( Application.persistentDataPath, "360render" + ( true ? ".jpeg" : ".png" ) );
            File.WriteAllBytes( path, bytes );
            Debug.Log( "360 render saved to " + path );
        }
    }
}