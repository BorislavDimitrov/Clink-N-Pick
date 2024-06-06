using System.Text.RegularExpressions;

namespace ClickNPick.Application.Helpers;

public static class EmailContentHelper
{
    private const string UsernamePlaceholder = @"\[Username\]";
    private const string ConfirmationLinkPlaceholder = @"\[ConfirmationLink\]";
    private const string ResetPasswordLinkPlaceholder = @"\[ResetPasswordLink\]";
    private const string PromotionDetailsPlaceholder = @"\[PromotionDetails\]";

    private static readonly Regex UsernameRegex = new Regex(UsernamePlaceholder);
    private static readonly Regex ConfirmationLinkRegex = new Regex(ConfirmationLinkPlaceholder);
    private static readonly Regex ResetPasswordLinkRegex = new Regex(ResetPasswordLinkPlaceholder);
    private static readonly Regex PromotionDetailsRegex = new Regex(PromotionDetailsPlaceholder);

    private const string ConfirmEmailTextTemplate = @"
<body style='font-family: Arial, sans-serif; margin: 100px; padding: 100px; background-color: #f4f4f4;'>
  <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
    <h1 style='color: #333;'>Welcome to Our Platform!</h1>
    <p style='color: #666;'>Dear [Username],</p>
    <p style='color: #666;'>Thank you for registering with us! We're excited to have you on board.</p>
    <p style='color: #666;'>To get started, please confirm your email address by clicking the button below:</p>
    <p style='color: #666;'><a href='[ConfirmationLink]' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px;'>Confirm Email</a></p>
    <p style='color: #666;'>If you didn't register on our platform, you can ignore this email.</p>
    <p style='color: #666;'>Best regards,<br>Click N`Pick team</p>
  </div>
</body>";

    private const string ResetPasswordEmailTextTemplate = @"
<body style='font-family: Arial, sans-serif; margin: 0; padding: 0;'>
  <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; border-radius: 10px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);'>
    <h1 style='color: #333; font-size: 24px; font-weight: bold; text-align: center;'>Forgot Your Password?</h1>
    <p style='color: #666; font-size: 16px; text-align: center;'>Dear [Username],</p>
    <p style='color: #666; font-size: 16px; text-align: center;'>You have requested to reset your password.</p>
    <div style='text-align: center; margin-top: 20px;'>
      <a href='[ResetPasswordLink]' style='display: inline-block; padding: 12px 24px; background-color: #007bff; color: #fff; font-size: 16px; text-decoration: none; border-radius: 5px;'>Reset Password</a>
    </div>
    <p style='color: #666; font-size: 16px; text-align: center; margin-top: 20px;'>If you didn't request a password reset, you can ignore this email.</p>
    <p style='color: #666; font-size: 16px; text-align: center; margin-top: 20px;'>Best regards,<br />Click N`Pick team</p>
  </div>
</body>";

    private const string PromotionSuccessEmailTextTemplate = @"
<body style='font-family: Arial, sans-serif; margin: 0; padding: 0;'>
  <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; border-radius: 10px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);'>
    <h1 style='color: #333; font-size: 24px; font-weight: bold; text-align: center;'>Congratulations! Your Ad is Successfully Promoted</h1>
    <p style='color: #666; font-size: 16px; text-align: center;'>Dear [Username],</p>
    <p style='color: #666; font-size: 16px; text-align: center;'>Your product promotion ad has been successfully promoted. Payment for the promotion has been processed successfully.</p>
    <div style='text-align: center; margin-top: 20px;'>
      <p style='color: #666; font-size: 16px; text-align: center;'>Promotion Details:</p>
      <p style='color: #666; font-size: 16px; text-align: center;'>[PromotionDetails]</p>
    </div>
    <p style='color: #666; font-size: 16px; text-align: center; margin-top: 20px;'>Thank you for choosing our platform for promoting your product.</p>
    <p style='color: #666; font-size: 16px; text-align: center; margin-top: 20px;'>Best regards,<br />Click N`Pick team</p>
  </div>
</body>";

    public static string FormatConfirmEmailText(string username, string confirmationLink)
    {
        string formattedEmail = ConfirmEmailTextTemplate;

        formattedEmail = UsernameRegex.Replace(formattedEmail, username);
        formattedEmail = ConfirmationLinkRegex.Replace(formattedEmail, confirmationLink);

        return formattedEmail;
    }

    public static string FormatResetPasswordEmailText(string username, string resetPasswordLink)
    {
        string formattedEmail = ResetPasswordEmailTextTemplate;

        formattedEmail = UsernameRegex.Replace(formattedEmail, username);
        formattedEmail = ResetPasswordLinkRegex.Replace(formattedEmail, resetPasswordLink);

        return formattedEmail;
    }

    public static string FormatPromotionSuccessEmailText(string username, string promotionDetails)
    {
        string formattedEmail = PromotionSuccessEmailTextTemplate;

        formattedEmail = UsernameRegex.Replace(formattedEmail, username);
        formattedEmail = PromotionDetailsRegex.Replace(formattedEmail, promotionDetails);

        return formattedEmail;
    }
}
