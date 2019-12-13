namespace Orz.Unity.Interfaces
{
    public interface IPhone
    {
        IHeadphone Headphone { get; set; }
        IMicrophone Microphone { get; set; }
        IPower Power { get; set; }
        void Print();
    }
}
