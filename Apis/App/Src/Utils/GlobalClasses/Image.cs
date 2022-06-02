namespace InteliSystem.Utils.GlobalClasses
{
    public class Image
    {
        public Image() { }

        public Image(string id, byte[] file, string extension)
        {
            Id = id;
            File = file;
            Extension = extension;
        }

        public string Id { get; set; }
        public byte[] File { get; set; }
        public string Extension { get; set; }
    }
}