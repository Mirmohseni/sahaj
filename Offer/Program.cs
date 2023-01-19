namespace Offer;
class Program
{
    static void Main(string[] args)
    {
        ProcessOffer.ProcessFile(@"test.csv",@"OfferRecords.csv",@"FailingRecords.csv");
    }
}
