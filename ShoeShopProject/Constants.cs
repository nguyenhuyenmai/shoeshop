namespace ShoeShopProject
{
    /// <summary>
    /// Lưu trữ thông tin biến toàn cục, biến môi trường của hệ thống
    /// </summary>
    public class Constants
    {
        public const string DEFAULT_IMG_USER = "/assets/img/illustrations/profiles/profile-2.png";

        /// <summary>
        /// Convert currency
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ConvertCurrency(decimal amount)
        {
            return amount.ToString("#,##0đ");
        }
    }
}
