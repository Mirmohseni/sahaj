namespace Offer
{
    public class ProcessOffer
    {
        public static void ProcessFile(string inputFilename, string outputFilename, string failureFilename)
        {
            using (var reader = new StreamReader(inputFilename))
            {
                reader.ReadLine();

                Record record;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var st = line?.Split(',');

                    if (st != null && st.Length == 10)
                    {
                        record = Record.ToRecord(st);
                        record.ValidateRecord();
                        if (record.ValidationErrors == null || !record.ValidationErrors.Any())
                        {
                            record.WriteFile(outputFilename);
                        }
                        else
                        {
                            record.WritefailingRecordsFile(failureFilename);
                        }
                    }
                    else
                    {
                        Record.WritefailingRecordsFile(failureFilename, $"{line}, this line's data is not valid");
                    }
                }
            }
        }
    }
}