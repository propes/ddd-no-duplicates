namespace DddNoDuplicates.Domain
{
   public class Result
   {
      public bool IsSuccess { get; }

      public string ErrorMessage { get; }

      private Result()
      {
         IsSuccess = true;
      }

      private Result(string errorMessage)
      {
         IsSuccess = false;
         ErrorMessage = errorMessage;
      }

      public static Result Success() => new();

      public static Result Fail(string errorMessage) => new(errorMessage);
   }
}