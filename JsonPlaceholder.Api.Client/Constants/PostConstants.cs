namespace JsonPlaceholder.Api.Client.Constants
{
    public class PostConstants
    {
        public const int TitleMaxChars = 50; //NOTE: Real API does not have such limitation. It was created only for test example purposes

        public static string TitleMoreThanMaxValidationMessage = $"Posts title cannot be more than {TitleMaxChars} chars";

        public static string RecordWasNotFoundMessage(int? recordId) => $"Post with id {recordId} was not found";
    }
}
