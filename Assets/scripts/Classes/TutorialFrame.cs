namespace Classes
{
    public struct TutorialFrame
    {
        public string Path;
        public float StayTime;

        public TutorialFrame(string path, float stayTime)
        {
            Path = path;
            StayTime = stayTime;
        }
    }
}