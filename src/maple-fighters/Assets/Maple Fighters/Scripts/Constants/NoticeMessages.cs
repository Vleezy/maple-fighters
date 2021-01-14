﻿namespace Scripts.Constants
{
    public static class NoticeMessages
    {
        public static class AuthView
        {
            public const string EmptyEmailAddress = "Email address can not be empty.";
            public const string InvalidEmailAddress = "Email address is not valid.";
            public const string EmptyPassword = "Password can not be empty.";
            public const string ShortPassword = "Please enter a longer password.";
            public const string EmptyConfirmPassword = "Confirm password can not be empty.";
            public const string PasswordsDoNotMatch = "Passwords are not match.";
            public const string EmptyFirstName = "The first name is empty.";
            public const string EmptyLastName = "The last name is empty.";
            public const string ShortFirstName = "The first name is too short.";
            public const string ShortLastName = "The last name is too short.";
            public const string WrongPassword = "The password is incorrect.";
            public const string WrongEmailAddress = "The email address does not exist.";
            public const string RegistrationSucceed = "Registration completed successfully!";
            public const string UnknownError = "An unknown error has occurred. Please try again later.";
        }

        public static class GameServerBrowserView
        {
            public const string UnknownError = "An unknown error has occurred. Please try again later.";
        }

        public static class CharacterView
        {
            public const string CharacterCreationFailed = "The character creation failed. Please try again later.";
            public const string NameAlreadyInUse = "Your character name is already taken.";
            public const string CharacterValidationFailed = "The character validation failed. Please try again later.";
            public const string CharacterDeletionFailed = "The character deletion failed. Please try again later.";
            public const string UnknownError = "An unknown error has occurred. Please try again later.";
        }
    }
}