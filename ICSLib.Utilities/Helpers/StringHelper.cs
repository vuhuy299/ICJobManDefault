/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System;

namespace ICSLib.Utilities.Helpers
{
    public class StringHelper
    {
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡö",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữü",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static bool isNumeric(string s)
		{
			try
			{
				Convert.ToInt32(s);
				return true;
			}
			catch
			{
				return false;
			}
		}

        public static string RemoveSignature(string input)
        {
            try
            {
                if (input == null)
                {
                    return "";
                }
                input = input.Replace("\"", "");
                for (int i = 1; i < VietnameseSigns.Length; i++)
                {
                    for (int j = 0; j < VietnameseSigns[i].Length; j++)
                        input = input.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return input;
        }

        public static string removeSpecialSignatures(string inputString)
        {
            string retVal = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(inputString))
                {
                    for (int i = 0; i < inputString.Length; i++)
                    {
                        char c = inputString[i];
                        if ((c >= 'a' && c <= 'z')
                            || (c >= 'A' && c <= 'Z')
                            || (c >= '0' && c <= '9')
                            || (c == ' '))
                        {
                            retVal += c.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return retVal;
        }

        public static string RemoveSignatureForURL(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }
                input = RemoveSignature(input);
                input = input.ToLower().Trim();
                input = removeSpecialSignatures(input);
                while (input.Contains("  "))
                {
                    input = input.Replace("  ", " ");
                }
                input = input.Replace(" ", "-");
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return input;
        }
    }
}
