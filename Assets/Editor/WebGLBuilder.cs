using UnityEditor;

class WebGLBuilder
{
    static void build()
    {
        string[] scenes = {"Assets/Scenes/MainScene.unity"};

        string pathToDeploy = "builds/WebGL/";

        BuildPipeline.BuildPlayer(scenes, pathToDeploy, BuildTarget.WebGL, BuildOptions.None);
    }
}
