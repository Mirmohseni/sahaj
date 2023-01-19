using System.Text.RegularExpressions;

namespace Offer
{
    public class Record
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PNR { get; set; }
        public char FareClass { get; set; }
        public DateTime TravelDate { get; set; }
        public int Pax { get; set; }
        public DateTime TicketingDate { get; set; }
        public string? Email { get; set; }
        public string? MobilePhone { get; set; }
        public string? BookedCabin { get; set; }
        public List<string>? ValidationErrors { get; set; }

        public Record()
        {
            this.ValidationErrors = new List<string>();
        }

        public static Record ToRecord(String[] st)
        {
            var record = new Record()
            {
                FirstName = st[0].Replace(" ", ""),
                LastName = st[1].Replace(" ", ""),
                PNR = st[2].Replace(" ", ""),
                Email = st[7].Replace(" ", ""),
                MobilePhone = st[8].Replace(" ", ""),
                BookedCabin = st[9].Replace(" ", "")
            };

            // FareClass
            try
            {
                record.FareClass = char.Parse(st[3].Replace(" ", ""));
            }
            catch
            {
                record.ValidationErrors?.Add("FareClass is not valid.");
            }

            // TravelDate
            try
            {
                record.TravelDate = DateTime.Parse(st[4].Replace(" ", ""));
            }
            catch
            {
                record.ValidationErrors?.Add("TravelDate is not valid.");
            }

            // Pax
            try
            {
                record.Pax = Int32.Parse(st[5].Replace(" ", ""));
            }
            catch
            {
                record.ValidationErrors?.Add("Pax is not valid.");
            }

            // TicketingDate
            try
            {
                record.TicketingDate = DateTime.Parse(st[6].Replace(" ", ""));
            }
            catch
            {
                record.ValidationErrors?.Add("TicketingDate is not valid.");
            }

            return record;
        }

        public void ValidateRecord()
        {
            // Email
            if (this.Email == null)
            {
                this.ValidationErrors?.Add("Email is null.");
            }
            else if (this.IsEmailValid() == false)
            {
                this.ValidationErrors?.Add("Email ID is not valid.");
            }

            // Mobile phone
            if (this.MobilePhone == null)
            {
                this.ValidationErrors?.Add("Mobile phone is null.");
            }
            else if (IsMobilePhoneValid(this.MobilePhone) == false)
            {
                this.ValidationErrors?.Add("Mobile phone is not valid.");
            }

            // Ticketing date
            if (IsTicketingDateValid(this.TicketingDate, this.TravelDate) == false)
            {
                this.ValidationErrors?.Add("Ticketing date is not before travel date.");
            }

            // PNR
            if (this.PNR == null)
            {
                this.ValidationErrors?.Add("PNR is null.");
            }
            else if (IsPNRValid(this.PNR) == false)
            {
                this.ValidationErrors?.Add("PNR is not 6 characters or is not alphanumeric.");
            }

            //Booked Cabin
            if (this.BookedCabin == null)
            {
                this.ValidationErrors?.Add("Booked cabin is null.");
            }
            else if (IsBookedCabinValid(this.BookedCabin) == false)
            {
                this.ValidationErrors?.Add("Booked cabin is not valid.");
            }
        }

        private bool IsEmailValid()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(this.Email ??= "");
            return match.Success;
        }

        private bool IsMobilePhoneValid(string mobilePhone)
        {
            return Regex.Match(mobilePhone, @"^\+?[0-9]{1,14}$").Success;
        }

        private bool IsTicketingDateValid(DateTime ticketingDate, DateTime travelDate)
        {
            return ticketingDate.CompareTo(travelDate) < 0;
        }

        private bool IsPNRValid(string pnr)
        {
            return Regex.Match(pnr, @"^[a-zA-Z0-9]{6}$").Success;
        }

        private bool IsBookedCabinValid(string bookedCabin)
        {
            return Enum.IsDefined(typeof(BookedCabin), bookedCabin.Replace(" ", ""));
        }

        public void WriteFile(string filename)
        {
            var line = this.RecordToString();

            string discountCode = GetDiscountCode();

            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine($"{line}, {discountCode}");
            }
        }

        public string GetDiscountCode()
        {
            var discountCode = "";
            if (this.FareClass <= 'E')
            {
                discountCode = "OFFER_20";
            }
            else if (this.FareClass <= 'K')
            {
                discountCode = "OFFER_30";
            }
            else if (this.FareClass <= 'R')
            {
                discountCode = "OFFER_25";
            }

            return discountCode;
        }

        public void WritefailingRecordsFile(string filename)
        {
            var line = this.RecordToString();
            using (StreamWriter sw = File.AppendText(filename))
            {
                var errors = string.Join("|", this.ValidationErrors);
                sw.WriteLine($"{line}, {errors}");
            }
        }

        public static void WritefailingRecordsFile(string filename, string line)
        {
            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine(line);
            }
        }

        private string RecordToString()
        {
            return $"{this.FirstName}, {this.LastName}, {this.PNR}, {this.FareClass}, {this.TravelDate}, {this.Pax}, {this.TicketingDate}, {this.Email}, {this.MobilePhone}, {this.BookedCabin}";
        }
    }

    public enum BookedCabin
    {
        Economy,
        PremiumEconomy,
        Business,
        First
    }
}
