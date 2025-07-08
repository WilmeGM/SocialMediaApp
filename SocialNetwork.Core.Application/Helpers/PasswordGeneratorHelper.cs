namespace SocialNetwork.Core.Application.Helpers
{
    public static class PasswordGeneratorHelper
    {
        public static string GenerateRandomPassword(int length = 6, int requiredUniqueChars = 1)
        {
            string upperCase = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            string lowerCase = "abcdefghijkmnopqrstuvwxyz";
            string digits = "0123456789";
            string specialChars = "!@#$%^&*()_+-=[]{}|;:'\",.<>?";

            var rand = new Random();
            var passwordChars = new List<char>
            {
                upperCase[rand.Next(upperCase.Length)],
                lowerCase[rand.Next(lowerCase.Length)],
                digits[rand.Next(digits.Length)],
                specialChars[rand.Next(specialChars.Length)]
            };

            string allChars = upperCase + lowerCase + digits + specialChars;
            while (passwordChars.Count < length)
            {
                passwordChars.Add(allChars[rand.Next(allChars.Length)]);
            }

            return new string(passwordChars.Distinct().Take(requiredUniqueChars).Concat(passwordChars).OrderBy(_ => rand.Next()).ToArray());
        }
    }
}
