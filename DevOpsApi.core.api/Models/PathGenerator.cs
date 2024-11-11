namespace DevOpsApi.core.api.Models
{
    public static class PathGenerator
    {
        public static string GeneratePathByNumber(string number)
        {
            try
            {
                long result;
                if (long.TryParse(number, out result) == false) return number;

                int lenght = number.Length;
                if (lenght <= 3)
                {
                    return "1" + @"\" + number;
                }

                List<String> folderNames = CreatePathMethod(number);
                if (folderNames != null)
                    return folderNames.Aggregate((s, b) => s + @"\" + b);
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string CreatePathByLetterAndNumber(string documentNo)
        {
            string[] splittedArray = documentNo.Split('-');
            if (splittedArray.Count() < 4) return null;

            List<String> result = CreatePathMethod(splittedArray[3]);
            if (result == null && result.Count == 0) return null;

            List<String> withHeader = new List<string>();
            result.ForEach(d => withHeader.Add(splittedArray.First() + d));
            return withHeader.Aggregate((s, b) => s + @"\" + b);

        }
        private static List<String> CreatePathMethod(string number)
        {
            try
            {
                List<String> folderNames = new List<String>();
                for (int i = 1; i <= number.Length - 3; i++)
                {
                    string sub = number.Substring(0, i);
                    sub = sub.PadRight(number.Length, '0');
                    folderNames.Add(sub);
                }
                if (!folderNames.Any()) folderNames.Add("0".PadRight(number.Length, '0'));
                folderNames.Add(number);
                return folderNames;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GeneratedFilePath(string doumentNo, DocumentType docType)
        {
            if (String.IsNullOrEmpty(doumentNo)) return string.Empty;

            if (docType == DocumentType.NumberAndLetter)
                return PathGenerator.CreatePathByLetterAndNumber(doumentNo);
            else if (docType == DocumentType.Number)
                return PathGenerator.GeneratePathByNumber(doumentNo);
            return string.Empty;
        }
    }
}
