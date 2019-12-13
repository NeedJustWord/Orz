namespace Orz.Autofac.Interfaces
{
    public interface IPhone : IId
    {
        IHeadphone Headphone { get; set; }
        IMicrophone Microphone { get; set; }
        IPower Power { get; set; }
        void Print();
        void PrintId();
    }
}
