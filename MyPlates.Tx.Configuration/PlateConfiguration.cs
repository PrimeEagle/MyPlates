using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using MyPlates.Tx.Carts;
using MyPlates.Tx.Data;
using MyPlates.Tx.Helper;
using TxDot.Web.Services;

namespace MyPlates.Tx.Configuration
{
    public class PlateConfiguration
    {
        public static void LoadPlateData(LicensePlate plate)
        {
            plate.ReservedForMyPlates = MyPlatesData.GetReservedPlateStatus(plate.Text);
            plate.CategoryID = MyPlatesData.GetPlateCategoryId(plate.PlateCode);
            plate.CategoryName = MyPlatesData.GetPlateCategoryName(plate.PlateCode);
            plate.TypeName = MyPlatesData.GetPlateTypeName(plate.PlateCode);
            plate.Class = MyPlatesData.GetClassForPlateCode(plate.PlateCode);
            plate.RegEx = MyPlatesData.GetRegExForPlateCode(plate.PlateCode);
            plate.RegExISA = MyPlatesData.GetRegExISAForPlateCode(plate.PlateCode);

            using (SqlDataReader reader = (SqlDataReader)MyPlatesData.GetPricingInformation(plate.PlateCode, plate.RenewalPeriod))
            {
                while (reader.Read())
                {
                    plate.TotalCost = reader.GetDecimal(reader.GetOrdinal("TotalFee"));
                    plate.TxDotYearlyPlateFee = reader.GetDecimal(reader.GetOrdinal("YearlyPlateFee"));
                    plate.TxDotGeneralRevenuePercentage = reader.GetDecimal(reader.GetOrdinal("GeneralRevenuePercent"));
                }
            }

            using (SqlDataReader reader = (SqlDataReader)MyPlatesData.GetPlateSuggestionInformation(plate.PlateCode))
            {
                while (reader.Read())
                {
                    plate.SuggestionsAlgorithm = reader.GetString(reader.GetOrdinal("SuggestionAlgorithm"));
                    plate.NumSuggestions = reader.GetInt32(reader.GetOrdinal("NumSuggestions"));
                    plate.ValidateSuggestions = reader.GetBoolean(reader.GetOrdinal("SuggestionsValidated"));
                }
            }

            using (SqlDataReader reader = (SqlDataReader)MyPlatesData.GetPlateImageSize(plate.PlateCode))
            {
                while (reader.Read())
                {
                    plate.ImageSizeX = reader.GetInt32(reader.GetOrdinal("ImageSizeX"));
                    plate.ImageSizeY = reader.GetInt32(reader.GetOrdinal("ImageSizeY"));
                }
            }

            if (plate.OwnerInfo.County != null && plate.OwnerInfo.County.Length > 0)
            {
                CountyInfo cInfo = TxDotWebServices.GetSpecificCountyInfo(Context.SessionID, plate.OwnerInfo.County.ToUpper());

                string zip = cInfo.PhysicalAddress.ZIP;
                if (cInfo.PhysicalAddress.ZIP4.Length > 0)
                {
                    zip = zip + "-" + cInfo.PhysicalAddress.ZIP4;
                }

                plate.OwnerInfo.CountyNumber = cInfo.Number;
                plate.CountyStreet1 = cInfo.PhysicalAddress.Street1;
                plate.CountyStreet2 = cInfo.PhysicalAddress.Street2;
                plate.CountyCity = cInfo.PhysicalAddress.City;
                plate.CountyState = cInfo.PhysicalAddress.State;
                plate.CountyZIP = zip;
                plate.CountyPhone = cInfo.Phone;
                plate.CountyEmail = cInfo.Email;
            }
        }

        public static bool CheckPlateAvailability(string plateText, string plateCode)
        {
            LicensePlate plate = new LicensePlate();
            plate.MfgText = plateText;
            plate.PlateCode = plateCode;

            PlateConfiguration.LoadPlateData(plate);

            bool status = false;

            if (plate.ValidPattern)
            {
                status = false;
                bool availableLocal = true;

                if(plate.ReservedForMyPlates) 
                {
                    availableLocal = MyPlatesData.GetReservedPlateAvailability(plate.Text);
                }

                if(availableLocal) 
                {
                    TxDotResponse response = TxDotWebServices.CheckPlateAvailability(Context.SessionID, Context.Username, plate.Text, plate.MfgText, plate.PlateCode, plate.ReservedForMyPlates, plate.ISA);

                    if (response.Success)
                    {
                        status = true;
                    }
                }

                MyPlatesData.RecordPlateAvailabilityCheck(Context.UserHostAddress, Context.Username, plate.Text, status);
            }

            return status;
        }

        public static bool HoldPlate(string plateText, string plateCode)
        {
            TxDotResponse response = new TxDotResponse();
            LicensePlate plate = new LicensePlate();
            plate.MfgText = plateText;
            plate.PlateCode = plateCode;
            PlateConfiguration.LoadPlateData(plate);

            if (plate.ReservedForMyPlates)
            {
                response = TxDotWebServices.HoldReservedPlate(Context.SessionID, Context.Username, plate.Text, plate.MfgText, plate.PlateCode, plate.ISA);
            }
            else
            {
                response = TxDotWebServices.HoldPlate(Context.SessionID, Context.Username, plate.Text, plate.MfgText, plate.PlateCode, plate.ISA);
            }

            return response.Success;
        }

        public static bool RenewHoldPlate(string plateText, string plateCode)
        {
            bool reserved = MyPlatesData.GetReservedPlateStatus(plateText);

            LicensePlate plate = new LicensePlate();
            plate.MfgText = plateText;
            plate.PlateCode = plateCode;
            PlateConfiguration.LoadPlateData(plate);

            TxDotResponse response = TxDotWebServices.RenewHold(Context.SessionID, Context.Username, plate.Text, plate.MfgText, plate.PlateCode, plate.ReservedForMyPlates, plate.ISA);

            return response.Success;
        }

        public static bool CancelPlateHold(string plateText, string plateCode)
        {
            LicensePlate plate = new LicensePlate();
            plate.MfgText = plateText;
            plate.PlateCode = plateCode;
            PlateConfiguration.LoadPlateData(plate);

            TxDotResponse response = TxDotWebServices.CancelHold(Context.SessionID, Context.Username, plate.Text, plate.MfgText, plate.PlateCode, plate.ReservedForMyPlates, plate.ISA);

            return response.Success;
        }

        public static List<string> FindSuggestions(string plateText, string plateCode)
        {
            List<string> suggestions = new List<string>();
            string mask = string.Empty;
            string numberStr = string.Empty;
            string numPaddingStr = string.Empty;
            int number = 0;

            LicensePlate tempPlate = new LicensePlate();
            tempPlate.MfgText = plateText;
            tempPlate.PlateCode = plateCode;
            PlateConfiguration.LoadPlateData(tempPlate);

            switch (tempPlate.SuggestionsAlgorithm)
            {
                case "NONE":
                    suggestions.Clear();
                    break;

                case "NUMSEQ":
                    for (int i = 0; i < plateText.Length; i++)
                    {
                        if (char.IsNumber(plateText[i]))
                        {
                            mask += "#";
                            numberStr += plateText[i];
                        }
                        else
                        {
                            mask += "A";
                        }
                    }

                    number = Convert.ToInt32(numberStr);
                    for (int j = 0; j < numberStr.Length; j++)
                    {
                        numPaddingStr += "0";
                    }

                    for (int n = 0; n < tempPlate.NumSuggestions; n++)
                    {
                        string s = string.Empty;
                        bool numReplaced = false;

                        for (int pos = 0; pos < mask.Length; pos++)
                        {
                            if (mask[pos] == 'A')
                            {
                                s += plateText[pos];
                            } else if(mask[pos] == '#' && !numReplaced) 
                            {
                                s += string.Format("{0:" + numPaddingStr + "}", ++number);
                                numReplaced = true;
                            }
                        }
                        suggestions.Add(s);
                    }

                    if (tempPlate.ValidateSuggestions)
                    {
                        suggestions = RemoveInvalidSuggestions(suggestions, plateCode);
                    }
                    break;

                case "NUMSEQVALID":
                    string alpha = string.Empty;

                    for (int i = 0; i < plateText.Length; i++)
                    {
                        if (char.IsNumber(plateText[i]))
                        {
                            numberStr += plateText[i];
                        }
                        else
                        {
                            alpha += plateText[i];
                        }
                    }

                    number = Convert.ToInt32(numberStr);

                    DataSet ds = MyPlatesData.GetReservedSeriesSuggestions(alpha, number, tempPlate.NumSuggestions);

                    foreach(DataRow dr in ds.Tables[0].Rows) 
                    {
                        string pt = dr["PlateText"].ToString();
                        if (PlateConfiguration.CheckPlateAvailability(pt, plateCode)) 
                        {
                            suggestions.Add(pt);
                        }
                    }
                    break;

                case "SUBST":
                    suggestions = PlateConfiguration.GetSuggestedSubstitutes(plateText, tempPlate.NumSuggestions);
                    
                    if (tempPlate.ValidateSuggestions)
                    {
                        suggestions = RemoveInvalidSuggestions(suggestions, plateCode);
                    }
                    break;

                default:
                    break;
            }

            return suggestions;
        }

        private static List<string> RemoveInvalidSuggestions(List<string> suggestions, string plateCode)
        {
            List<string> tempSuggestions = new List<string>();
            foreach (string s in suggestions)
            {
                if (PlateConfiguration.CheckPlateAvailability(s, plateCode))
                {
                    tempSuggestions.Add(s);
                }
            }

            return tempSuggestions;
        }

        private static List<string> GetSuggestedSubstitutes(string plateText, int numNeeded)
        {
            int numFound = 0;
            List<int> positions;
            List<Suggestion> suggestions = new List<Suggestion>();
            List<string> suggestionsText = new List<string>();

            for (int i = 0; i < Convert.ToInt32(Math.Pow((double)2, (double)plateText.Length)); i++)
            {
                string binary = Convert.ToString(i, 2).PadLeft(plateText.Length, '0');

                positions = new List<int>();
                string revBinary = ReverseString(binary);
                for (int j = 0; j < revBinary.Length; j++)
                {
                    if (revBinary[j].ToString().Equals("1"))
                    {
                        positions.Add(j);
                    }
                }

                string newText = plateText;
                foreach(int pos in positions)
                {
                    string replacement = MyPlatesData.GetSuggestedSubstitute(newText[pos].ToString());
                    if (replacement == string.Empty)
                    {
                        replacement = plateText[pos].ToString();
                    }
                    newText = newText.Substring(0, pos) + replacement + newText.Substring(pos + 1);
                }

                if (!newText.Equals(plateText))
                {
                    // check to see if this one already exists before adding
                    bool exists = false;
                    foreach (Suggestion s in suggestions)
                    {
                        if (s.SuggestedText == newText)
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        suggestions.Add(new Suggestion(plateText, newText));
                        numFound++;
                        if (numFound == numNeeded)
                        {
                            break;
                        }
                    }
                }

            }

            suggestions.Sort();
            foreach (Suggestion s in suggestions)
            {
                suggestionsText.Add(s.SuggestedText);
            }

            return suggestionsText;
        }

        private static string ReverseString(string x)
        {
            char[] charArray = new char[x.Length];
            int len = x.Length - 1;
            for (int i = 0; i <= len; i++)
                charArray[i] = x[len - i];
            return new string(charArray);
        }

        public static string FormatPhoneNumber(string phoneNum)
        {
            string formattedNum = string.Empty;

            if (phoneNum.Length == 10)
            {
                formattedNum = "(" + phoneNum.Substring(0, 3) + ") " + phoneNum.Substring(3, 3) + "-" + phoneNum.Substring(6);
            }

            if (phoneNum.Length == 7)
            {
                formattedNum = phoneNum.Substring(0, 3) + "-" + phoneNum.Substring(3);
            }

            return formattedNum;
        }

        public static string ToTitleCase(string text)
        {
            return new System.Globalization.CultureInfo("en").TextInfo.ToTitleCase(text.ToLower());
        }

        public static string CleanNumber(string phoneNum)
        {
            string newPhoneNum = "";

            for (int i = 0; i < phoneNum.Length; i++)
            {
                if (char.IsNumber(phoneNum[i]))
                {
                    newPhoneNum += phoneNum[i];
                }
            }

            return newPhoneNum;
        }

        public static string FormatZIPCode(string zip, string zip4)
        {
            if (zip4 == string.Empty)
            {
                return zip;
            }
            else
            {
                return zip + "-" + zip4;
            }
        }
    }
}
