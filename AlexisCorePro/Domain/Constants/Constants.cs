﻿namespace AlexisCorePro.Domain
{
    public class Default
    {
        public static readonly string DateFormat = "dd.MM.yyyy";

        public static readonly int TextFieldLength = 100;
        public static readonly int TextAreaLength = 500;

        public const string Language = "en";

        public const int PageSize = 10;
    }

    public static class CultureTwoLetterISONames
    {
        public const string English = "en";

        public const string German = "de";
    }

    public static class Roles
    {
        public const string Admin = "admin";
        public const string User = "user";
    }
}
