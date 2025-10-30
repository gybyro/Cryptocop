namespace Cryptocop.Software.API.Repositories.Helpers;

public class PaymentCardHelper
{
    public static string MaskPaymentCard(string paymentCardNumber)
    {
        var mask = new string('*', paymentCardNumber.Length - 4);
        var pan = paymentCardNumber.Substring(paymentCardNumber.Length - 4, paymentCardNumber.Length);

        return $"{mask}{pan}";
    }
}