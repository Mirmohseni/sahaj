namespace Offer.Tests;

public class RecordTest
{
    [Theory]
    [InlineData("A b h i s h e k , K u m a r , A B C 1 2 3 , Ff , 2 0 1 9 - 0 7 - 3 1 , 2 , 2 0 1 9 - 0 5 - 2 1 , abhishek@zzz.com, 9876543210, Economy")]
    public void ToRecord_InValidFareClass_ReturnTrue(string line)
    {
        var st = line.Split(',');
        var record = Record.ToRecord(st);

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("FareClass is not valid.");

        Assert.True(result, $"FareClass is not valid.");
    }

    [Theory]
    [InlineData("A b h i s h e k , K u m a r , A B C 1 2 3 , F , 2 0 1 9 - 0 7  3 1 , 2 , 2 0 1 9 - 0 5 - 2 1 , abhishek@zzz.com, 9876543210, Economy")]
    public void ToRecord_InValidTravelDate_ReturnTrue(string line)
    {
        var st = line.Split(',');
        var record = Record.ToRecord(st);

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("TravelDate is not valid.");

        Assert.True(result, $"TravelDate is not valid.");
    }

    [Theory]
    [InlineData("A b h i s h e k , K u m a r , A B C 1 2 3 , F , 2 0 1 9 - 0 7 - 3 1 , 2c , 2 0 1 9 - 0 - 2 1 , abhishek@zzz.com, 9876543210, Economy")]
    public void ToRecord_InValidPax_ReturnTrue(string line)
    {
        var st = line.Split(',');
        var record = Record.ToRecord(st);

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Pax is not valid.");

        Assert.True(result, $"Pax is not valid.");
    }

    [Theory]
    [InlineData("A b h i s h e k , K u m a r , A B C 1 2 3 , F , 2 0 1 9 - 0 7 - 3 1 , 2 , 2 0 1 9 - 0 - 2 1 , abhishek@zzz.com, 9876543210, Economy")]
    public void ToRecord_InValidTicketingDate_ReturnTrue(string line)
    {
        var st = line.Split(',');
        var record = Record.ToRecord(st);

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("TicketingDate is not valid.");

        Assert.True(result, $"TicketingDate is not valid.");
    }

    [Theory]
    [InlineData("nn")]
    [InlineData("ns@")]
    [InlineData("ns@ns")]
    [InlineData("ns@ns.")]
    public void ValidateRecord_InvalidEmail_ReturnTrue(string email)
    {
        var record = new Record()
        {
            Email = email
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Email ID is not valid.");

        Assert.True(result, $"{email} is not valid");
    }

    [Theory]
    [InlineData("n@ns.com")]
    [InlineData("n.s@ns.com")]
    [InlineData("n.s@ns.co.uk")]
    public void ValidateRecord_ValidEmail_ReturnFalse(string email)
    {
        var record = new Record()
        {
            Email = email
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Email ID is not valid.");

        Assert.False(result, $"{email} is valid");
    }

    [Theory]
    [InlineData("234s")]
    [InlineData("s076543")]
    [InlineData("++447506060606")]
    [InlineData("07506060606.")]
    public void ValidateRecord_InvalidMobilePhone_ReturnTrue(string mobilePhone)
    {
        {
            var record = new Record()
            {
                MobilePhone = mobilePhone
            };

            record.ValidateRecord();

            if (record.ValidationErrors == null)
            {
                record.ValidationErrors = new List<string>();
            }

            bool result = record.ValidationErrors.Contains("Mobile phone is not valid.");

            Assert.True(result, $"{mobilePhone} is not valid.");
        }
    }

    [Theory]
    [InlineData("123")]
    [InlineData("1")]
    [InlineData("+447506060606")]
    [InlineData("07506060606")]
    public void ValidateRecord_ValidMobilePhone_ReturnFalse(string mobilePhone)
    {
        var record = new Record()
        {
            MobilePhone = mobilePhone
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Mobile phone is not valid.");

        Assert.False(result, $"{mobilePhone} is valid.");
    }

    [Theory]
    [InlineData("2023-01-01", "2023-01-01")]
    [InlineData("2023-01-02", "2023-01-01")]
    public void ValidateRecord_InValidTicketingDate_ReturnTrue(DateTime ticketingDate, DateTime travelDate)
    {
        var record = new Record()
        {
            TicketingDate = ticketingDate,
            TravelDate = travelDate
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Ticketing date is not before travel date.");

        Assert.True(result, $"Ticketing date is not before travel date.");
    }

    [Theory]
    [InlineData("2023-01-01", "2023-10-01")]
    public void ValidateRecord_ValidTicketingDate_ReturnFalse(DateTime ticketingDate, DateTime travelDate)
    {
        var record = new Record()
        {
            TicketingDate = ticketingDate,
            TravelDate = travelDate
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Ticketing date is not before travel date.");

        Assert.False(result, $"Ticketing date is valid.");
    }

    [Theory]
    [InlineData("nssf")]
    [InlineData("nsgujgf")]
    [InlineData("ns@ns1")]
    [InlineData("ns1ns.")]
    public void ValidateRecord_InvalidPNR_ReturnTrue(string pnr)
    {
        var record = new Record()
        {
            PNR = pnr
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("PNR is not 6 characters or is not alphanumeric.");

        Assert.True(result, $"{pnr} is not valid.");
    }

    [Theory]
    [InlineData("3e5ghk")]
    [InlineData("8Kfd34")]
    [InlineData("345678")]
    [InlineData("ABCDEF")]
    [InlineData("vwxyzz")]
    public void ValidateRecord_ValidPNR_ReturnFalse(string pnr)
    {
        var record = new Record()
        {
            PNR = pnr
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("PNR is not 6 characters or is not alphanumeric.");

        Assert.False(result, $"{pnr} is valid.");
    }

    [Theory]
    [InlineData("Eco")]
    [InlineData("Premium Eco")]
    [InlineData("Busines")]
    [InlineData("1st")]
    public void ValidateRecord_InvalidBookedCabin_ReturnTrue(string bookedCabin)
    {
        var record = new Record()
        {
            BookedCabin = bookedCabin
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Booked cabin is not valid.");

        Assert.True(result, $"{bookedCabin} is not valid.");
    }

    [Theory]
    [InlineData("Economy")]
    [InlineData("PremiumEconomy")]
    [InlineData("Business")]
    [InlineData("First")]
    public void ValidateRecord_ValidBookedCabin_ReturnFalse(string bookedCabin)
    {
        var record = new Record()
        {
            BookedCabin = bookedCabin
        };

        record.ValidateRecord();

        if (record.ValidationErrors == null)
        {
            record.ValidationErrors = new List<string>();
        }

        bool result = record.ValidationErrors.Contains("Booked cabin is not valid.");

        Assert.False(result, $"{bookedCabin} is valid.");
    }

    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("E")]
    public void GetDiscountCode_FareClassAtoE(string fareClass)
    {
        var record = new Record()
        {
            FareClass = char.Parse(fareClass)
        };

        var result = record.GetDiscountCode();

        Assert.Equal("OFFER_20", result);
    }

    [Theory]
    [InlineData("F")]
    [InlineData("H")]
    [InlineData("K")]
    public void GetDiscountCode_FareClassFtoK(string fareClass)
    {
        var record = new Record()
        {
            FareClass = char.Parse(fareClass)
        };

        var result = record.GetDiscountCode();

        Assert.Equal("OFFER_30", result);
    }

    [Theory]
    [InlineData("L")]
    [InlineData("N")]
    [InlineData("R")]
    public void GetDiscountCode_FareClassLtoR(string fareClass)
    {
        var record = new Record()
        {
            FareClass = char.Parse(fareClass)
        };

        var result = record.GetDiscountCode();

        Assert.Equal("OFFER_25", result);
    }

    [Theory]
    [InlineData("S")]
    [InlineData("Y")]
    [InlineData("Z")]
    public void GetDiscountCode_FareClassStoZ(string fareClass)
    {
        var record = new Record()
        {
            FareClass = char.Parse(fareClass)
        };

        var result = record.GetDiscountCode();

        Assert.Equal("", result);
    }
}