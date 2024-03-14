namespace ShoeShop.Data
{
    public class EmrSession
    {
        public int userId { get; set; }
        public int Role { get; set; }
        public string userName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Image { get; set; } = "";

        public EmrSession(HttpContext httpContext)
        {
            if (httpContext != null && httpContext.Session != null)
            {
                userId = httpContext.Session.GetInt32("user_Id") ?? -1;
                Role = httpContext.Session.GetInt32("user_role") ?? -1;
                Email = string.IsNullOrEmpty(httpContext.Session.GetString("user_email")) ? "" : httpContext.Session.GetString("user_email");
                userName = string.IsNullOrEmpty(httpContext.Session.GetString("user_name")) ? "" : httpContext.Session.GetString("user_name");
                Image = string.IsNullOrEmpty(httpContext.Session.GetString("user_img")) ? "" : httpContext.Session.GetString("user_img");
            }
        }

        public void putSession(HttpContext httpContext)
        {
            httpContext.Session.SetInt32("user_Id", userId);
            httpContext.Session.SetInt32("user_role", Role);
            httpContext.Session.SetString("user_email", Email);
            httpContext.Session.SetString("user_name", userName);
            httpContext.Session.SetString("user_img", Image);
        }

        public void clearSession(HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }
    }
}
